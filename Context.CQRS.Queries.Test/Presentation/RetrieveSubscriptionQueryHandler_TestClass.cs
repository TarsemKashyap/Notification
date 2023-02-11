using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example.Common.Context.DDD.UnitOfWork;
using Moq;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.CQRS.Queries.Presentation;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Common;
using PetaPoco;
using SimpleInjector;
using Example.Notific.Context.SimpleInjector;
using Example.Common.Context.CQRS;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.PetaPoco;
using Example.Notific.Context.Common.Helpers;

namespace Example.Notific.Context.CQRS.Queries.Test.Presentation
{
    [TestClass]
    public class RetrieveSubscriptionQueryHandler_TestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetrieveSubscriptionQueryHandler_Storage_Null()
        {
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            RetrieveSubscriptionQueryHandler handler = new RetrieveSubscriptionQueryHandler(null,
                subscriptionRepo.Object);           
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetrieveSubscriptionQueryHandler_SubscriptionRepository_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();

            RetrieveSubscriptionQueryHandler handler = new RetrieveSubscriptionQueryHandler(storage.Object,
                null);
        }

        [TestMethod]
        [ExpectedException(typeof(SubscriptionNotFoundException))]
        public void RetrieveSubscriptionQueryHandler_SubscriptionNotFound()
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

            RetrieveSubscriptionQueryHandler handler = new RetrieveSubscriptionQueryHandler(storage.Object,
             subscriptionRepo.Object);

            RetrieveSubscriptionQuery query = new RetrieveSubscriptionQuery() { SubscriptionId = 20001 };

            handler.Handle(query);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetrieveSubscriptionQueryHandler_Query_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            RetrieveSubscriptionQueryHandler handler = new RetrieveSubscriptionQueryHandler(storage.Object,
             subscriptionRepo.Object);

            handler.Handle(null);
        }

        [TestMethod]
        public void RetrieveSubscriptionQueryHandler_Handle()
        {

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

            RetrieveSubscriptionQueryHandler handler = new RetrieveSubscriptionQueryHandler(storage.Object,
              subscriptionRepo.Object);

            RetrieveSubscriptionQuery query = new RetrieveSubscriptionQuery() { SubscriptionId = 20001 };

            var actual = handler.Handle(query);

            Assert.AreEqual(subscriptionModel.Object.SubscriptionDate.ToString(), actual.DateSubscribed.ToString(), "Subscription date values are not equal");
            Assert.AreEqual(subscriptionModel.Object.MerchantId, actual.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(subscriptionModel.Object.DeliveryAddress, actual.DeliveryAddress, "Delivery address values are not equal");
            Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModel.Object.DeliveryMethod), actual.DeliveryMethod, "Delivery method values are not equal");
            Assert.AreEqual(subscriptionModel.Object.Description, actual.Description, "Description values are not equal");
            Assert.AreEqual((int)subscriptionModel.Object.EventType, actual.EventType, "Event type values are not equal");
            Assert.AreEqual(subscriptionModel.Object.SubscribedBy, actual.Subscriber, "Subscribed by values are not equal");
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void RetrieveSubscriptionQueryHandler_Handle_Integration()
        {
            var db = new Database("NotDataBase");

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>>();

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
                    Subscription_Terminated = true,
                    Termination_Date = DateTime.Now
                };

                id = (long)db.Insert("Subscription", "Id", subscriptionModel);

                var query = new RetrieveSubscriptionQuery() { SubscriptionId = id };

                var actual = handler.Handle(query);

                Assert.AreEqual(subscriptionModel.Subscription_Date.ToString(), actual.DateSubscribed.ToString(), "Subscription date values are not equal");
                Assert.AreEqual(subscriptionModel.Merchant_ID, actual.MerchantId, "Merchant ID values are not equal");
                Assert.AreEqual(subscriptionModel.Delivery_Address, actual.DeliveryAddress, "Delivery address values are not equal");
                Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModel.Delivery_Method_ID), actual.DeliveryMethod, "Delivery method values are not equal");
                Assert.AreEqual(subscriptionModel.Description, actual.Description, "Description values are not equal");
                Assert.AreEqual(subscriptionModel.Event_Type_ID, actual.EventType, "Event type values are not equal");
                Assert.AreEqual(subscriptionModel.Subscribed_By, actual.Subscriber, "Subscribed by values are not equal");

            }
            finally
            {
                db.Execute("delete from Subscription where id='" + id + "'");
            }
        }
    }
}
