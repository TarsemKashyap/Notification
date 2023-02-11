using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Moq;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Common;
using Example.Notific.Context.Domain.Model;
using PetaPoco;
using SimpleInjector;
using Example.Notific.Context.SimpleInjector;
using Example.Notific.PetaPoco;
using Example.Common.Context.CQRS;

namespace Example.Notific.Context.CQRS.Commands.Test
{
    [TestClass]
   public class SubscribeToEventCommandHandler_TestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SubscribeToEventCommandHandler_Stroage_Null()
        {          
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            SubscribeToEventCommandHandler handler = new SubscribeToEventCommandHandler(null,subscriptionRepo.Object);          
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SubscribeToEventCommandHandler_SubscriptionRepository_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();          

            SubscribeToEventCommandHandler handler = new SubscribeToEventCommandHandler(storage.Object,null);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SubscribeToEventCommandHandler_Command_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            SubscribeToEventCommandHandler handler = new SubscribeToEventCommandHandler(storage.Object,subscriptionRepo.Object);           

            handler.Handle(null);
        }


        [TestMethod]
        public void SubscribeToEventCommandHandler_Handle()
        {

            #region mocking

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<Subscription> subscriptionModel = new Mock<Subscription>();

            subscriptionRepo
                .Setup(t => t.Save(It.IsAny<Subscription>()))
                .Returns(subscriptionModel.Object);

            subscriptionModel.SetupGet(s => s.DeliveryAddress).Returns("Address");
            subscriptionModel.SetupGet(s => s.MerchantId).Returns(20001);
            subscriptionModel.SetupGet(s => s.DeliveryMethod).Returns(DeliveryMethod.Email);
            subscriptionModel.SetupGet(s => s.Description).Returns("Description");
            subscriptionModel.SetupGet(s => s.SubscribedBy).Returns("abc@yahoo.com");
            subscriptionModel.SetupGet(s => s.CreatedDate).Returns(DateTime.UtcNow);
            subscriptionModel.SetupGet(s => s.SubscriptionDate).Returns(DateTime.UtcNow);
            subscriptionModel.SetupGet(s => s.EventType).Returns(EventType.DdPaymentDishonoured);

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            #endregion

            SubscribeToEventCommandHandler handler = new SubscribeToEventCommandHandler(storage.Object,
             subscriptionRepo.Object);

            SubscribeToEventCommand command = new SubscribeToEventCommand()
            {
                DeliveryAddress = "Address",
                DeliveryMethod = DeliveryMethod.Email,
                Description = "Description",
                EventType = EventType.DdPaymentDishonoured,
                MerchantId = 20001,
                Subscriber = "abc@yahoo.com"
            };

            handler.Handle(command);

            subscriptionRepo.Verify(t => t.Save(It.IsAny<Subscription>()), Times.Once());

        }

        [TestMethod]
        [TestCategory("Integration")]
        public void SubscribeToEventCommandHandler_Handle_Integration()
        {
            var db = new Database("NotDataBase");

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<ICommandHandler<SubscribeToEventCommand>>();

            long id = 0;

            try
            {               

                SubscribeToEventCommand command = new SubscribeToEventCommand()
                {
                    DeliveryAddress = "abc@yahoo.com",
                    DeliveryMethod = DeliveryMethod.Email,
                    Description = "Description",
                    EventType = EventType.DdPaymentDishonoured,
                    MerchantId = 20001,
                    Subscriber = "abc@yahoo.com"                    
                };

                handler.Handle(command);

                id = command.SubscriptionId;

                Assert.IsTrue(id > 0L, "should persist subscription record");

                var repo = container.GetInstance<ISubscriptionRepository>();

                var storag = container.GetInstance<IUnitOfWorkStorage>();

                using (var uow = storag.NewUnitOfWork())
                {
                    var actual = repo.Get(id);

                    Assert.IsNotNull(actual.CreatedBy, "Create by should not be Null");
                    Assert.IsNotNull(actual.CreatedDate.ToString(), "Creation date should not be Null");
                    Assert.AreEqual(command.DeliveryAddress, actual.DeliveryAddress, "Delivery address values are not equal");
                    Assert.AreEqual(command.DeliveryMethod, actual.DeliveryMethod, "Delivery method values are not equal");
                    Assert.AreEqual(command.Description, actual.Description, "Description values are not equal");
                    Assert.AreEqual(command.EventType, actual.EventType, "Event type values are not equal");
                    Assert.AreEqual(command.MerchantId, actual.MerchantId, "Merchant ID values are not equal");
                    Assert.AreEqual(command.Subscriber, actual.SubscribedBy, "Subscribed by values are not equal");
                    Assert.IsNotNull(actual.SubscriptionDate.ToString(), "Subscription date should not be Null");
                    Assert.IsFalse(actual.SubscriptionTerminated, "Subscription terminated values should not be true");                   
                }

            }
            finally
            {
                db.Execute("delete from Subscription where id='" + id + "'");
            }
        }
    }
}
