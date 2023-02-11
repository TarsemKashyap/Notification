using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example.Common.Context.DDD.UnitOfWork;
using Moq;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Common;
using PetaPoco;
using SimpleInjector;
using Example.Notific.Context.SimpleInjector;
using Example.Notific.PetaPoco;
using Example.Common.Context.CQRS;

namespace Example.Notific.Context.CQRS.Commands.Test
{
    [TestClass]
    public class TerminateSubscriptionCommandHandler_TestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TerminateSubscriptionCommandHandle_Stroage_Null()
        {           
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            TerminateSubscriptionCommandHandler handler = new TerminateSubscriptionCommandHandler(null,subscriptionRepo.Object);         
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TerminateSubscriptionCommandHandle_SubscriptionRepository_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();           

            TerminateSubscriptionCommandHandler handler = new TerminateSubscriptionCommandHandler(storage.Object,null);           
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TerminateSubscriptionCommandHandle_Command_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            TerminateSubscriptionCommandHandler handler = new TerminateSubscriptionCommandHandler(storage.Object, subscriptionRepo.Object);           

            handler.Handle(null);
        }

        [TestMethod]
        [ExpectedException(typeof(SubscriptionNotFoundException))]
        public void TerminateSubscriptionCommandHandle_SubscriptionNotFound()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            subscriptionRepo
                .Setup(t => t.Get(It.IsAny<long>()))
                .Returns((Subscription)null);

            TerminateSubscriptionCommandHandler handler = new TerminateSubscriptionCommandHandler(storage.Object, subscriptionRepo.Object);

            TerminateSubscriptionCommand command = new TerminateSubscriptionCommand() { SubscriptionId = 2000 };

            handler.Handle(command);
        }

        [TestMethod]
        public void TerminateSubscriptionCommandHandle_Handle()
        {          

            #region mocking

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<Subscription> subscriptionModel = new Mock<Subscription>();

            subscriptionRepo
                .Setup(t => t.Get(It.IsAny<long>()))
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

            TerminateSubscriptionCommandHandler handler = new TerminateSubscriptionCommandHandler(storage.Object, subscriptionRepo.Object);

            TerminateSubscriptionCommand command = new TerminateSubscriptionCommand() { SubscriptionId = 2000 };

            handler.Handle(command);

            subscriptionRepo.Verify(t => t.Update(It.IsAny<Subscription>()), Times.Once());

        }

        [TestMethod]
        [TestCategory("Integration")]
        public void TerminateSubscriptionCommandHandle_Handle_Integration()
        {
            var db = new Database("NotDataBase");

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<ICommandHandler<TerminateSubscriptionCommand>>();

            long id = 0;

            try
            {
                string createdBy = "SubscriptionMap_";

                var subscriptionModel = new SubscriptionPetaPoco()
                {
                    Created_By = createdBy,
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 1,
                    Merchant_ID = 20001,
                    Delivery_Address = "Address",
                    Delivery_Method_ID = 1,
                    Description = "Description",
                    Subscribed_By = "Subscribed_By",
                    Subscription_Date = DateTime.Now,
                    Subscription_Terminated = false,
                    Termination_Date = DateTime.Now
                };

                id = (long)db.Insert("Subscription", "Id", subscriptionModel);

                TerminateSubscriptionCommand command = new TerminateSubscriptionCommand() { SubscriptionId = id };

                handler.Handle(command);

                var repo = container.GetInstance<ISubscriptionRepository>();

                var storag = container.GetInstance<IUnitOfWorkStorage>();

                using (var uow = storag.NewUnitOfWork())
                {
                    var actual = repo.Get(id);

                    Assert.AreEqual(true, actual.SubscriptionTerminated, "This should not be false");

                    Assert.IsNotNull(actual.TerminationDate, "This should not be Null");
                }

            }
            finally
            {
                db.Execute("delete from Subscription where id='" + id + "'");
            }
        }
    }
}
