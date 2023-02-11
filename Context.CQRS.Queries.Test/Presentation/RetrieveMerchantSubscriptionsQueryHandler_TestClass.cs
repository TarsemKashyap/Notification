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
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Common;
using PetaPoco;
using SimpleInjector;
using Example.Notific.Context.SimpleInjector;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Common.Context.CQRS;
using Example.Notific.PetaPoco;
using Example.Notific.Context.Common.Helpers;

namespace Example.Notific.Context.CQRS.Queries.Test.Presentation
{
    [TestClass]
    public class RetrieveMerchantSubscriptionsQueryHandler_TestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetrieveMerchantSubscriptionsQueryHandler_Storage_Null()
        {
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            RetrieveMerchantSubscriptionsQueryHandler handler = new RetrieveMerchantSubscriptionsQueryHandler(null,
                subscriptionRepo.Object);          
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetrieveMerchantSubscriptionsQueryHandler_SubscriptionRepository_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();

            RetrieveMerchantSubscriptionsQueryHandler handler = new RetrieveMerchantSubscriptionsQueryHandler(storage.Object,
                null);           

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetrieveMerchantSubscriptionsQueryHandler_Query_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();

            RetrieveMerchantSubscriptionsQueryHandler handler = new RetrieveMerchantSubscriptionsQueryHandler(storage.Object,
             subscriptionRepo.Object);

            handler.Handle(null);
        }

        [TestMethod]
        public void RetrieveMerchantSubscriptionsQueryHandler_Handle()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<Subscription> subscriptionModelOne = new Mock<Subscription>();
            Mock<Subscription> subscriptionModelTwo = new Mock<Subscription>();
            List<Mock<Subscription>> subscriptionModel = new List<Mock<Subscription>>();

            subscriptionModelOne.SetupGet(s => s.DeliveryAddress).Returns("Address");
            subscriptionModelOne.SetupGet(s => s.MerchantId).Returns(20001);
            subscriptionModelOne.SetupGet(s => s.DeliveryMethod).Returns(DeliveryMethod.Email);
            subscriptionModelOne.SetupGet(s => s.Description).Returns("Description");
            subscriptionModelOne.SetupGet(s => s.SubscribedBy).Returns("abc@yahoo.com");
            subscriptionModelOne.SetupGet(s => s.CreatedDate).Returns(DateTime.UtcNow);
            subscriptionModelOne.SetupGet(s => s.SubscriptionDate).Returns(DateTime.UtcNow);
            subscriptionModelOne.SetupGet(s => s.EventType).Returns(EventType.DdPaymentDishonoured);
            subscriptionModelOne.SetupGet(s => s.SubscriptionTerminated).Returns(false);

            subscriptionModelTwo.SetupGet(s => s.DeliveryAddress).Returns("Address");
            subscriptionModelTwo.SetupGet(s => s.MerchantId).Returns(20001);
            subscriptionModelTwo.SetupGet(s => s.DeliveryMethod).Returns(DeliveryMethod.Email);
            subscriptionModelTwo.SetupGet(s => s.Description).Returns("Description");
            subscriptionModelTwo.SetupGet(s => s.SubscribedBy).Returns("abc@yahoo.com");
            subscriptionModelTwo.SetupGet(s => s.CreatedDate).Returns(DateTime.UtcNow);
            subscriptionModelTwo.SetupGet(s => s.SubscriptionDate).Returns(DateTime.UtcNow);
            subscriptionModelTwo.SetupGet(s => s.EventType).Returns(EventType.DdPaymentDishonoured);
            subscriptionModelTwo.SetupGet(s => s.SubscriptionTerminated).Returns(false);

            subscriptionModel.Add(subscriptionModelOne);
            subscriptionModel.Add(subscriptionModelTwo);

            subscriptionRepo
               .Setup(t => t.GetByMerchant(It.IsAny<int>(), It.IsAny<bool>()))
               .Returns(subscriptionModel.Select(m => m.Object).ToList());

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            RetrieveMerchantSubscriptionsQueryHandler handler = new RetrieveMerchantSubscriptionsQueryHandler(storage.Object,
              subscriptionRepo.Object);


            RetrieveMerchantSubscriptionsQuery query = new RetrieveMerchantSubscriptionsQuery() { MerchantId = 20001, IncludeTerminated = false };

            var actual = handler.Handle(query);


            Assert.AreEqual(2, actual.Count(), "Should be 2 as two subscriptions of same merchant inserted");

            Assert.AreEqual(subscriptionModelOne.Object.SubscriptionDate.ToString(), actual[0].DateSubscribed.ToString(), "Subscription date values are not equal");
            Assert.AreEqual(subscriptionModelOne.Object.DeliveryAddress, actual[0].DeliveryAddress, "Delivery address values are not equal");
            Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModelOne.Object.DeliveryMethod), actual[0].DeliveryMethod, "Delivery method values are not equal");
            Assert.AreEqual(subscriptionModelOne.Object.Description, actual[0].Description, "Description values are not equal");
            Assert.AreEqual((int)subscriptionModelOne.Object.EventType, actual[0].EventType, "Event type values are not equal");
            Assert.AreEqual(subscriptionModelOne.Object.MerchantId, actual[0].MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(subscriptionModelOne.Object.SubscribedBy, actual[0].Subscriber, "Subscribed by values are not equal");

            Assert.AreEqual(subscriptionModelTwo.Object.SubscriptionDate.ToString(), actual[1].DateSubscribed.ToString(), "Creation date values are not equal");
            Assert.AreEqual(subscriptionModelTwo.Object.DeliveryAddress, actual[1].DeliveryAddress, "Delivery address values are not equal");
            Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModelTwo.Object.DeliveryMethod), actual[1].DeliveryMethod, "Delivery method values are not equal");
            Assert.AreEqual(subscriptionModelTwo.Object.Description, actual[1].Description, "Description values are not equal");
            Assert.AreEqual((int)subscriptionModelTwo.Object.EventType, actual[1].EventType, "Event type values are not equal");
            Assert.AreEqual(subscriptionModelTwo.Object.MerchantId, actual[1].MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(subscriptionModelTwo.Object.SubscribedBy, actual[1].Subscriber, "Subscribed by values are not equal");

        }

        [TestMethod]
        public void RetrieveMerchantSubscriptionsQueryHandler_Included_Terminated_Handle()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<Subscription> subscriptionModelOne = new Mock<Subscription>();
            Mock<Subscription> subscriptionModelTwo = new Mock<Subscription>();
            List<Mock<Subscription>> subscriptionModel = new List<Mock<Subscription>>();

            subscriptionModelOne.SetupGet(s => s.DeliveryAddress).Returns("Address");
            subscriptionModelOne.SetupGet(s => s.MerchantId).Returns(20001);
            subscriptionModelOne.SetupGet(s => s.DeliveryMethod).Returns(DeliveryMethod.Email);
            subscriptionModelOne.SetupGet(s => s.Description).Returns("Description");
            subscriptionModelOne.SetupGet(s => s.SubscribedBy).Returns("abc@yahoo.com");
            subscriptionModelOne.SetupGet(s => s.CreatedDate).Returns(DateTime.UtcNow);
            subscriptionModelOne.SetupGet(s => s.SubscriptionDate).Returns(DateTime.UtcNow);
            subscriptionModelOne.SetupGet(s => s.EventType).Returns(EventType.DdPaymentDishonoured);
            subscriptionModelOne.SetupGet(s => s.SubscriptionTerminated).Returns(false);

            subscriptionModelTwo.SetupGet(s => s.DeliveryAddress).Returns("Address");
            subscriptionModelTwo.SetupGet(s => s.MerchantId).Returns(20001);
            subscriptionModelTwo.SetupGet(s => s.DeliveryMethod).Returns(DeliveryMethod.Email);
            subscriptionModelTwo.SetupGet(s => s.Description).Returns("Description");
            subscriptionModelTwo.SetupGet(s => s.SubscribedBy).Returns("abc@yahoo.com");
            subscriptionModelTwo.SetupGet(s => s.CreatedDate).Returns(DateTime.UtcNow);
            subscriptionModelTwo.SetupGet(s => s.SubscriptionDate).Returns(DateTime.UtcNow);
            subscriptionModelTwo.SetupGet(s => s.EventType).Returns(EventType.DdPaymentDishonoured);
            subscriptionModelTwo.SetupGet(s => s.SubscriptionTerminated).Returns(true);


            subscriptionModel.Add(subscriptionModelOne);
            subscriptionModel.Add(subscriptionModelTwo);

            subscriptionRepo
               .Setup(t => t.GetByMerchant(It.IsAny<int>(), It.IsAny<bool>()))
               .Returns(subscriptionModel.Select(m => m.Object).ToList());

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            RetrieveMerchantSubscriptionsQueryHandler handler = new RetrieveMerchantSubscriptionsQueryHandler(storage.Object,
              subscriptionRepo.Object);


            RetrieveMerchantSubscriptionsQuery query = new RetrieveMerchantSubscriptionsQuery() { MerchantId = 20001, IncludeTerminated = true };

            var actual = handler.Handle(query);


            Assert.AreEqual(2, actual.Count(), "Should be 2 as two subscriptions of same merchant inserted");

            Assert.AreEqual(subscriptionModelOne.Object.SubscriptionDate.ToString(), actual[0].DateSubscribed.ToString(), "Subscription date values are not equal");
            Assert.AreEqual(subscriptionModelOne.Object.DeliveryAddress, actual[0].DeliveryAddress, "Delivery address values are not equal");
            Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModelOne.Object.DeliveryMethod), actual[0].DeliveryMethod, "Delivery method values are not equal");
            Assert.AreEqual(subscriptionModelOne.Object.Description, actual[0].Description, "Description values are not equal");
            Assert.AreEqual((int)subscriptionModelOne.Object.EventType, actual[0].EventType, "Event type values are not equal");
            Assert.AreEqual(subscriptionModelOne.Object.MerchantId, actual[0].MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(subscriptionModelOne.Object.SubscribedBy, actual[0].Subscriber, "Subscribed by values are not equal");

            Assert.AreEqual(subscriptionModelTwo.Object.SubscriptionDate.ToString(), actual[1].DateSubscribed.ToString(), "Creation date values are not equal");
            Assert.AreEqual(subscriptionModelTwo.Object.DeliveryAddress, actual[1].DeliveryAddress, "Delivery address values are not equal");
            Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModelTwo.Object.DeliveryMethod), actual[1].DeliveryMethod, "Delivery method values are not equal");
            Assert.AreEqual(subscriptionModelTwo.Object.Description, actual[1].Description, "Description values are not equal");
            Assert.AreEqual((int)subscriptionModelTwo.Object.EventType, actual[1].EventType, "Event type values are not equal");
            Assert.AreEqual(subscriptionModelTwo.Object.MerchantId, actual[1].MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(subscriptionModelTwo.Object.SubscribedBy, actual[1].Subscriber, "Subscribed by values are not equal");

        }

        [TestMethod]
        public void RetrieveMerchantSubscriptionsQueryHandler_Excluded_Terminated_Handle()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<Subscription> subscriptionModelOne = new Mock<Subscription>();
            Mock<Subscription> subscriptionModelTwo = new Mock<Subscription>();
            List<Mock<Subscription>> subscriptionModel = new List<Mock<Subscription>>();

            subscriptionModelOne.SetupGet(s => s.DeliveryAddress).Returns("Address");
            subscriptionModelOne.SetupGet(s => s.MerchantId).Returns(20001);
            subscriptionModelOne.SetupGet(s => s.DeliveryMethod).Returns(DeliveryMethod.Email);
            subscriptionModelOne.SetupGet(s => s.Description).Returns("Description");
            subscriptionModelOne.SetupGet(s => s.SubscribedBy).Returns("abc@yahoo.com");
            subscriptionModelOne.SetupGet(s => s.CreatedDate).Returns(DateTime.UtcNow);
            subscriptionModelOne.SetupGet(s => s.SubscriptionDate).Returns(DateTime.UtcNow);
            subscriptionModelOne.SetupGet(s => s.EventType).Returns(EventType.DdPaymentDishonoured);
            subscriptionModelOne.SetupGet(s => s.SubscriptionTerminated).Returns(false);

            subscriptionModelTwo.SetupGet(s => s.DeliveryAddress).Returns("Address");
            subscriptionModelTwo.SetupGet(s => s.MerchantId).Returns(20001);
            subscriptionModelTwo.SetupGet(s => s.DeliveryMethod).Returns(DeliveryMethod.Email);
            subscriptionModelTwo.SetupGet(s => s.Description).Returns("Description");
            subscriptionModelTwo.SetupGet(s => s.SubscribedBy).Returns("abc@yahoo.com");
            subscriptionModelTwo.SetupGet(s => s.CreatedDate).Returns(DateTime.UtcNow);
            subscriptionModelTwo.SetupGet(s => s.SubscriptionDate).Returns(DateTime.UtcNow);
            subscriptionModelTwo.SetupGet(s => s.EventType).Returns(EventType.DdPaymentDishonoured);
            subscriptionModelTwo.SetupGet(s => s.SubscriptionTerminated).Returns(true);

            subscriptionModel.Add(subscriptionModelOne);
            subscriptionModel.Add(subscriptionModelTwo);

            subscriptionRepo
               .Setup(t => t.GetByMerchant(It.IsAny<int>(), It.IsAny<bool>()))
               .Returns(subscriptionModel.Select(m => m.Object).Where(i => i.SubscriptionTerminated == false).ToList());

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            RetrieveMerchantSubscriptionsQueryHandler handler = new RetrieveMerchantSubscriptionsQueryHandler(storage.Object,
              subscriptionRepo.Object);


            RetrieveMerchantSubscriptionsQuery query = new RetrieveMerchantSubscriptionsQuery() { MerchantId = 20001, IncludeTerminated = false };

            var actual = handler.Handle(query);


            Assert.AreEqual(1, actual.Count(), "Should be 1 as one subscription terminated");

            Assert.AreEqual(subscriptionModelOne.Object.SubscriptionDate.ToString(), actual[0].DateSubscribed.ToString(), "Subscription date values are not equal");
            Assert.AreEqual(subscriptionModelOne.Object.DeliveryAddress, actual[0].DeliveryAddress, "Delivery address values are not equal");
            Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModelOne.Object.DeliveryMethod), actual[0].DeliveryMethod, "Delivery method values are not equal");
            Assert.AreEqual(subscriptionModelOne.Object.Description, actual[0].Description, "Description values are not equal");
            Assert.AreEqual((int)subscriptionModelOne.Object.EventType, actual[0].EventType, "Event type values are not equal");
            Assert.AreEqual(subscriptionModelOne.Object.MerchantId, actual[0].MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(subscriptionModelOne.Object.SubscribedBy, actual[0].Subscriber, "Subscribed by values are not equal");
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void RetrieveMerchantSubscriptionsQueryHandler_Handle_Integration()
        {
            var db = new Database("NotDataBase");

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            long idOne = 0;

            long idTwo = 0;

            try
            {
                string createdBy = "SubscriptionMap_";

                var subscriptionModelOne = new SubscriptionPetaPoco()
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

                var subscriptionModelTwo = new SubscriptionPetaPoco()
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

                idOne = (long)db.Insert("Subscription", "Id", subscriptionModelOne);

                idTwo = (long)db.Insert("Subscription", "Id", subscriptionModelTwo);


                RetrieveMerchantSubscriptionsQuery query = new RetrieveMerchantSubscriptionsQuery() { MerchantId = 20001, IncludeTerminated = false };

                var actual = handler.Handle(query);

                Assert.AreEqual(2, actual.Count(), "Should be 2 as two subscriptions of same merchant inserted");

                Assert.AreEqual(subscriptionModelOne.Subscription_Date.ToString(), actual[0].DateSubscribed.ToString(), "Subscription date values are not equal");
                Assert.AreEqual(subscriptionModelOne.Merchant_ID, actual[0].MerchantId, "Merchant ID values are not equal");
                Assert.AreEqual(subscriptionModelOne.Delivery_Address, actual[0].DeliveryAddress, "Delivery address values are not equal");
                Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModelOne.Delivery_Method_ID), actual[0].DeliveryMethod, "Delivery method values are not equal");
                Assert.AreEqual(subscriptionModelOne.Description, actual[0].Description, "Description values are not equal");
                Assert.AreEqual(subscriptionModelOne.Event_Type_ID, actual[0].EventType, "Event type values are not equal");
                Assert.AreEqual(subscriptionModelOne.Subscribed_By, actual[0].Subscriber, "Subscribed by values are not equal");

                Assert.AreEqual(subscriptionModelTwo.Subscription_Date.ToString(), actual[0].DateSubscribed.ToString(), "Subscription date values are not equal");
                Assert.AreEqual(subscriptionModelTwo.Merchant_ID, actual[0].MerchantId, "Merchant ID values are not equal");
                Assert.AreEqual(subscriptionModelTwo.Delivery_Address, actual[0].DeliveryAddress, "Delivery address values are not equal");
                Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModelTwo.Delivery_Method_ID), actual[0].DeliveryMethod, "Delivery method values are not equal");
                Assert.AreEqual(subscriptionModelTwo.Description, actual[0].Description, "Description values are not equal");
                Assert.AreEqual(subscriptionModelTwo.Event_Type_ID, actual[0].EventType, "Event type values are not equal");
                Assert.AreEqual(subscriptionModelTwo.Subscribed_By, actual[0].Subscriber, "Subscribed by values are not equal");

            }
            finally
            {
                db.Execute("delete from Subscription where id='" + idOne + "'");
                db.Execute("delete from Subscription where id='" + idTwo + "'");
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void RetrieveMerchantSubscriptionsQueryHandler_Handle_Included_Terminated_Integration()
        {
            var db = new Database("NotDataBase");

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            long idOne = 0;

            long idTwo = 0;

            try
            {
                string createdBy = "SubscriptionMap_";

                var subscriptionModelOne = new SubscriptionPetaPoco()
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

                var subscriptionModelTwo = new SubscriptionPetaPoco()
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

                idOne = (long)db.Insert("Subscription", "Id", subscriptionModelOne);

                idTwo = (long)db.Insert("Subscription", "Id", subscriptionModelTwo);


                RetrieveMerchantSubscriptionsQuery query = new RetrieveMerchantSubscriptionsQuery() { MerchantId = 20001, IncludeTerminated = true };

                var actual = handler.Handle(query);

                Assert.AreEqual(2, actual.Count(), "Should be 2 as one subscription terminated but included terminated subscriptions in result.");

                Assert.AreEqual(subscriptionModelOne.Subscription_Date.ToString(), actual[0].DateSubscribed.ToString(), "Subscription date values are not equal");
                Assert.AreEqual(subscriptionModelOne.Merchant_ID, actual[0].MerchantId, "Merchant ID values are not equal");
                Assert.AreEqual(subscriptionModelOne.Delivery_Address, actual[0].DeliveryAddress, "Delivery address values are not equal");
                Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModelOne.Delivery_Method_ID), actual[0].DeliveryMethod, "Delivery method values are not equal");
                Assert.AreEqual(subscriptionModelOne.Description, actual[0].Description, "Description values are not equal");
                Assert.AreEqual(subscriptionModelOne.Event_Type_ID, actual[0].EventType, "Event type values are not equal");
                Assert.AreEqual(subscriptionModelOne.Subscribed_By, actual[0].Subscriber, "Subscribed by values are not equal");

                Assert.AreEqual(subscriptionModelTwo.Subscription_Date.ToString(), actual[0].DateSubscribed.ToString(), "Subscription date values are not equal");
                Assert.AreEqual(subscriptionModelTwo.Merchant_ID, actual[0].MerchantId, "Merchant ID values are not equal");
                Assert.AreEqual(subscriptionModelTwo.Delivery_Address, actual[0].DeliveryAddress, "Delivery address values are not equal");
                Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModelTwo.Delivery_Method_ID), actual[0].DeliveryMethod, "Delivery method values are not equal");
                Assert.AreEqual(subscriptionModelTwo.Description, actual[0].Description, "Description values are not equal");
                Assert.AreEqual(subscriptionModelTwo.Event_Type_ID, actual[0].EventType, "Event type values are not equal");
                Assert.AreEqual(subscriptionModelTwo.Subscribed_By, actual[0].Subscriber, "Subscribed by values are not equal");

            }
            finally
            {
                db.Execute("delete from Subscription where id='" + idOne + "'");
                db.Execute("delete from Subscription where id='" + idTwo + "'");
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void RetrieveMerchantSubscriptionsQueryHandler_Handle_Excluded_Terminated_Integration()
        {
            var db = new Database("NotDataBase");

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>>();

            long idOne = 0;

            long idTwo = 0;

            try
            {
                string createdBy = "SubscriptionMap_";

                var subscriptionModelOne = new SubscriptionPetaPoco()
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

                var subscriptionModelTwo = new SubscriptionPetaPoco()
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

                idOne = (long)db.Insert("Subscription", "Id", subscriptionModelOne);

                idTwo = (long)db.Insert("Subscription", "Id", subscriptionModelTwo);


                RetrieveMerchantSubscriptionsQuery query = new RetrieveMerchantSubscriptionsQuery() { MerchantId = 20001, IncludeTerminated = false };

                var actual = handler.Handle(query);

                Assert.AreEqual(1, actual.Count(), "Should be 1 as one subscription terminated");

                Assert.AreEqual(subscriptionModelOne.Subscription_Date.ToString(), actual[0].DateSubscribed.ToString(), "Subscription date values are not equal");
                Assert.AreEqual(subscriptionModelOne.Merchant_ID, actual[0].MerchantId, "Merchant ID values are not equal");
                Assert.AreEqual(subscriptionModelOne.Delivery_Address, actual[0].DeliveryAddress, "Delivery address values are not equal");
                Assert.AreEqual(EnumExtensions.GetString((DeliveryMethod)subscriptionModelOne.Delivery_Method_ID), actual[0].DeliveryMethod, "Delivery method values are not equal");
                Assert.AreEqual(subscriptionModelOne.Description, actual[0].Description, "Description values are not equal");
                Assert.AreEqual(subscriptionModelOne.Event_Type_ID, actual[0].EventType, "Event type values are not equal");
                Assert.AreEqual(subscriptionModelOne.Subscribed_By, actual[0].Subscriber, "Subscribed by values are not equal");
                 
            }
            finally
            {
                db.Execute("delete from Subscription where id='" + idOne + "'");
                db.Execute("delete from Subscription where id='" + idTwo + "'");
            }
        }

        [TestMethod]
        public void RetrieveMerchantSubscriptionsQueryHandler_Handle_Zero_Subscriptions()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<Subscription> subscriptionModelOne = new Mock<Subscription>();
            Mock<Subscription> subscriptionModelTwo = new Mock<Subscription>();
            List<Mock<Subscription>> subscriptionModel = new List<Mock<Subscription>>();          

            subscriptionRepo
               .Setup(t => t.GetByMerchant(It.IsAny<int>(), It.IsAny<bool>()))
               .Returns(subscriptionModel.Select(m => m.Object).ToList());

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            RetrieveMerchantSubscriptionsQueryHandler handler = new RetrieveMerchantSubscriptionsQueryHandler(storage.Object,
              subscriptionRepo.Object);

            RetrieveMerchantSubscriptionsQuery query = new RetrieveMerchantSubscriptionsQuery() { MerchantId = 20001, IncludeTerminated = false };

            var actual = handler.Handle(query);

            Assert.AreEqual(0, actual.Count(), "Should be 0 as no subscriptions of same merchant inserted"); 
        }
    }
}
