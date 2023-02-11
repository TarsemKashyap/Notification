using Example.Common.Context.DDD.UnitOfWork;
using Example.Notific.Context.Common;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.SimpleInjector;
using Example.Notific.PetaPoco;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetaPoco;
using SimpleInjector;
using System;
using System.Linq;

namespace Example.Notific.Context.Domain.Test.Repositories
{
    [TestClass]
    public class SubscriptionRepository_TestClass
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void GetMerchantSubscription_Data()
        {
            var idOne = 0L;
            var idTwo = 0L;

            int merchantId = 20001;

            var db = new Database("NotDatabase");
            try
            {
                var subscriptionOne = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 1,
                    Merchant_ID = merchantId,
                    Delivery_Address = "Address",
                    Delivery_Method_ID = 1,
                    Description = "Description",
                    Subscribed_By = "Subscribed_By",
                    Subscription_Date = DateTime.Now,
                    Subscription_Terminated = false,
                    Termination_Date = DateTime.Now
                };

                idOne = (long)db.Insert("Subscription", "Id", subscriptionOne);

                var subscriptionTwo = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 2,
                    Merchant_ID = merchantId,
                    Delivery_Address = "Address",
                    Delivery_Method_ID = 1,
                    Description = "Description",
                    Subscribed_By = "Subscribed_By",
                    Subscription_Date = DateTime.Now,
                    Subscription_Terminated = false,
                    Termination_Date = DateTime.Now
                };

                idTwo = (long)db.Insert("Subscription", "Id", subscriptionTwo);

                Container container = new Container();

                NotBootstrapper.Bootstrap(container, null, true);

                var storage = container.GetInstance<IUnitOfWorkStorage>();

                var subscriptionRepo = container.GetInstance<ISubscriptionRepository>();

                Assert.IsTrue(idOne > 0L, "should persist subscription record");
                Assert.IsTrue(idTwo > 0L, "should persist subscription record");

                using (var uow = storage.NewUnitOfWork())
                {
                    var actual = subscriptionRepo.GetByMerchant(merchantId, false);

                    Assert.AreEqual(2, actual.Count(), "Should be 2 as two subscriptions of same merchant inserted");

                    Assert.AreEqual(subscriptionOne.Created_By, actual[0].CreatedBy, "Create by values are not equal");
                    Assert.AreEqual(subscriptionOne.Creation_Date.ToString(), actual[0].CreatedDate.ToString(), "Creation date values are not equal");
                    Assert.AreEqual(subscriptionOne.Delivery_Address, actual[0].DeliveryAddress, "Delivery address values are not equal");
                    Assert.AreEqual(subscriptionOne.Delivery_Method_ID, (int)actual[0].DeliveryMethod, "Delivery method values are not equal");
                    Assert.AreEqual(subscriptionOne.Description, actual[0].Description, "Description values are not equal");
                    Assert.AreEqual(subscriptionOne.Event_Type_ID, (int)actual[0].EventType, "Event type values are not equal");
                    Assert.AreEqual(subscriptionOne.Merchant_ID, actual[0].MerchantId, "Merchant ID values are not equal");
                    Assert.AreEqual(subscriptionOne.Subscribed_By, actual[0].SubscribedBy, "Subscribed by values are not equal");
                    Assert.AreEqual(subscriptionOne.Subscription_Date.ToString(), actual[0].SubscriptionDate.ToString(), "Subscription date values are not equal");
                    Assert.AreEqual(subscriptionOne.Subscription_Terminated, actual[0].SubscriptionTerminated, "Subscription terminated values are not equal");

                    Assert.AreEqual(subscriptionTwo.Created_By, actual[1].CreatedBy, "Create by values are not equal");
                    Assert.AreEqual(subscriptionTwo.Creation_Date.ToString(), actual[1].CreatedDate.ToString(), "Creation date values are not equal");
                    Assert.AreEqual(subscriptionTwo.Delivery_Address, actual[1].DeliveryAddress, "Delivery address values are not equal");
                    Assert.AreEqual(subscriptionTwo.Delivery_Method_ID, (int)actual[1].DeliveryMethod, "Delivery method values are not equal");
                    Assert.AreEqual(subscriptionTwo.Description, actual[1].Description, "Description values are not equal");
                    Assert.AreEqual(subscriptionTwo.Event_Type_ID, (int)actual[1].EventType, "Event type values are not equal");
                    Assert.AreEqual(subscriptionTwo.Merchant_ID, actual[1].MerchantId, "Merchant ID values are not equal");
                    Assert.AreEqual(subscriptionTwo.Subscribed_By, actual[1].SubscribedBy, "Subscribed by values are not equal");
                    Assert.AreEqual(subscriptionTwo.Subscription_Date.ToString(), actual[1].SubscriptionDate.ToString(), "Subscription date values are not equal");
                    Assert.AreEqual(subscriptionTwo.Subscription_Terminated, actual[1].SubscriptionTerminated, "Subscription terminated values are not equal");
                }
            }
            finally
            {
                db.Delete("Subscription", "ID", null, idOne);
                db.Delete("Subscription", "ID", null, idTwo);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetMerchantSubscription_Included_Terminated_Data()
        {
            var idOne = 0L;
            var idTwo = 0L;

            int merchantId = 20001;

            var db = new Database("NotDatabase");
            try
            {
                var subscriptionOne = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 1,
                    Merchant_ID = merchantId,
                    Delivery_Address = "Address",
                    Delivery_Method_ID = 1,
                    Description = "Description",
                    Subscribed_By = "Subscribed_By",
                    Subscription_Date = DateTime.Now,
                    Subscription_Terminated = false,
                    Termination_Date = DateTime.Now
                };

                idOne = (long)db.Insert("Subscription", "Id", subscriptionOne);

                var subscriptionTwo = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 2,
                    Merchant_ID = merchantId,
                    Delivery_Address = "Address",
                    Delivery_Method_ID = 1,
                    Description = "Description",
                    Subscribed_By = "Subscribed_By",
                    Subscription_Date = DateTime.Now,
                    Subscription_Terminated = true,
                    Termination_Date = DateTime.Now
                };

                idTwo = (long)db.Insert("Subscription", "Id", subscriptionTwo);

                Container container = new Container();

                NotBootstrapper.Bootstrap(container, null, true);

                var storage = container.GetInstance<IUnitOfWorkStorage>();

                var subscriptionRepo = container.GetInstance<ISubscriptionRepository>();

                Assert.IsTrue(idOne > 0L, "should persist subscription record");
                Assert.IsTrue(idTwo > 0L, "should persist subscription record");

                using (var uow = storage.NewUnitOfWork())
                {
                    var actual = subscriptionRepo.GetByMerchant(merchantId, true);

                    Assert.AreEqual(2, actual.Count(), "Should be 2 as one subscription terminated but included terminated subscriptions in result.");

                    Assert.AreEqual(subscriptionOne.Created_By, actual[0].CreatedBy, "Create by values are not equal");
                    Assert.AreEqual(subscriptionOne.Creation_Date.ToString(), actual[0].CreatedDate.ToString(), "Creation date values are not equal");
                    Assert.AreEqual(subscriptionOne.Delivery_Address, actual[0].DeliveryAddress, "Delivery address values are not equal");
                    Assert.AreEqual(subscriptionOne.Delivery_Method_ID, (int)actual[0].DeliveryMethod, "Delivery method values are not equal");
                    Assert.AreEqual(subscriptionOne.Description, actual[0].Description, "Description values are not equal");
                    Assert.AreEqual(subscriptionOne.Event_Type_ID, (int)actual[0].EventType, "Event type values are not equal");
                    Assert.AreEqual(subscriptionOne.Merchant_ID, actual[0].MerchantId, "Merchant ID values are not equal");
                    Assert.AreEqual(subscriptionOne.Subscribed_By, actual[0].SubscribedBy, "Subscribed by values are not equal");
                    Assert.AreEqual(subscriptionOne.Subscription_Date.ToString(), actual[0].SubscriptionDate.ToString(), "Subscription date values are not equal");
                    Assert.AreEqual(subscriptionOne.Subscription_Terminated, actual[0].SubscriptionTerminated, "Subscription terminated values are not equal");

                    Assert.AreEqual(subscriptionTwo.Created_By, actual[1].CreatedBy, "Create by values are not equal");
                    Assert.AreEqual(subscriptionTwo.Creation_Date.ToString(), actual[1].CreatedDate.ToString(), "Creation date values are not equal");
                    Assert.AreEqual(subscriptionTwo.Delivery_Address, actual[1].DeliveryAddress, "Delivery address values are not equal");
                    Assert.AreEqual(subscriptionTwo.Delivery_Method_ID, (int)actual[1].DeliveryMethod, "Delivery method values are not equal");
                    Assert.AreEqual(subscriptionTwo.Description, actual[1].Description, "Description values are not equal");
                    Assert.AreEqual(subscriptionTwo.Event_Type_ID, (int)actual[1].EventType, "Event type values are not equal");
                    Assert.AreEqual(subscriptionTwo.Merchant_ID, actual[1].MerchantId, "Merchant ID values are not equal");
                    Assert.AreEqual(subscriptionTwo.Subscribed_By, actual[1].SubscribedBy, "Subscribed by values are not equal");
                    Assert.AreEqual(subscriptionTwo.Subscription_Date.ToString(), actual[1].SubscriptionDate.ToString(), "Subscription date values are not equal");
                    Assert.AreEqual(subscriptionTwo.Subscription_Terminated, actual[1].SubscriptionTerminated, "Subscription terminated values are not equal");
                }
            }
            finally
            {
                db.Delete("Subscription", "ID", null, idOne);
                db.Delete("Subscription", "ID", null, idTwo);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetMerchantSubscription_Excluded_Terminated_Data()
        {
            var idOne = 0L;
            var idTwo = 0L;

            int merchantId = 20001;

            var db = new Database("NotDatabase");
            try
            {
                var subscriptionOne = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 1,
                    Merchant_ID = merchantId,
                    Delivery_Address = "Address",
                    Delivery_Method_ID = 1,
                    Description = "Description",
                    Subscribed_By = "Subscribed_By",
                    Subscription_Date = DateTime.Now,
                    Subscription_Terminated = false,
                    Termination_Date = DateTime.Now
                };

                idOne = (long)db.Insert("Subscription", "Id", subscriptionOne);

                var subscriptionTwo = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 2,
                    Merchant_ID = merchantId,
                    Delivery_Address = "Address",
                    Delivery_Method_ID = 1,
                    Description = "Description",
                    Subscribed_By = "Subscribed_By",
                    Subscription_Date = DateTime.Now,
                    Subscription_Terminated = true,
                    Termination_Date = DateTime.Now
                };

                idTwo = (long)db.Insert("Subscription", "Id", subscriptionTwo);

                Container container = new Container();

                NotBootstrapper.Bootstrap(container, null, true);

                var storage = container.GetInstance<IUnitOfWorkStorage>();

                var subscriptionRepo = container.GetInstance<ISubscriptionRepository>();

                Assert.IsTrue(idOne > 0L, "should persist subscription record");
                Assert.IsTrue(idTwo > 0L, "should persist subscription record");

                using (var uow = storage.NewUnitOfWork())
                {
                    var actual = subscriptionRepo.GetByMerchant(merchantId, false);

                    Assert.AreEqual(1, actual.Count(), "Should be 1 as one subscription terminated");

                    Assert.AreEqual(subscriptionOne.Created_By, actual[0].CreatedBy, "Create by values are not equal");
                    Assert.AreEqual(subscriptionOne.Creation_Date.ToString(), actual[0].CreatedDate.ToString(), "Creation date values are not equal");
                    Assert.AreEqual(subscriptionOne.Delivery_Address, actual[0].DeliveryAddress, "Delivery address values are not equal");
                    Assert.AreEqual(subscriptionOne.Delivery_Method_ID, (int)actual[0].DeliveryMethod, "Delivery method values are not equal");
                    Assert.AreEqual(subscriptionOne.Description, actual[0].Description, "Description values are not equal");
                    Assert.AreEqual(subscriptionOne.Event_Type_ID, (int)actual[0].EventType, "Event type values are not equal");
                    Assert.AreEqual(subscriptionOne.Merchant_ID, actual[0].MerchantId, "Merchant ID values are not equal");
                    Assert.AreEqual(subscriptionOne.Subscribed_By, actual[0].SubscribedBy, "Subscribed by values are not equal");
                    Assert.AreEqual(subscriptionOne.Subscription_Date.ToString(), actual[0].SubscriptionDate.ToString(), "Subscription date values are not equal");
                    Assert.AreEqual(subscriptionOne.Subscription_Terminated, actual[0].SubscriptionTerminated, "Subscription terminated values are not equal");
                }
            }
            finally
            {
                db.Delete("Subscription", "ID", null, idOne);
                db.Delete("Subscription", "ID", null, idTwo);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetMerchantSubscriptionAndEventType_Data()
        {
            var idOne = 0L;
            var idTwo = 0L;

            int merchantId = 20001;

            var db = new Database("NotDatabase");
            try
            {
                var subscriptionOne = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 1,
                    Merchant_ID = merchantId,
                    Delivery_Address = "Address",
                    Delivery_Method_ID = 1,
                    Description = "Description",
                    Subscribed_By = "Subscribed_By",
                    Subscription_Date = DateTime.Now,
                    Subscription_Terminated = false,
                    Termination_Date = DateTime.Now
                };

                idOne = (long)db.Insert("Subscription", "Id", subscriptionOne);

                var subscriptionTwo = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 1,
                    Merchant_ID = merchantId,
                    Delivery_Address = "Address",
                    Delivery_Method_ID = 1,
                    Description = "Description",
                    Subscribed_By = "Subscribed_By",
                    Subscription_Date = DateTime.Now,
                    Subscription_Terminated = false,
                    Termination_Date = DateTime.Now
                };

                idTwo = (long)db.Insert("Subscription", "Id", subscriptionTwo);

                Container container = new Container();

                NotBootstrapper.Bootstrap(container, null, true);

                var storage = container.GetInstance<IUnitOfWorkStorage>();

                var subscriptionRepo = container.GetInstance<ISubscriptionRepository>();

                Assert.IsTrue(idOne > 0L, "should persist subscription record");
                Assert.IsTrue(idTwo > 0L, "should persist subscription record");

                using (var uow = storage.NewUnitOfWork())
                {
                    var actual = subscriptionRepo.GetActiveForMerchantAndEventType(merchantId, (EventType)1);

                    Assert.IsNotNull(actual, "Subscription should not be null");
                    Assert.AreEqual(2, actual.Count(), "Should be 2 as two subscriptions of same merchant inserted");

                    Assert.AreEqual(subscriptionOne.Created_By, actual[0].CreatedBy, "Create by values are not equal");
                    Assert.AreEqual(subscriptionOne.Creation_Date.ToString(), actual[0].CreatedDate.ToString(), "Creation date values are not equal");
                    Assert.AreEqual(subscriptionOne.Delivery_Address, actual[0].DeliveryAddress, "Delivery address values are not equal");
                    Assert.AreEqual(subscriptionOne.Delivery_Method_ID, (int)actual[0].DeliveryMethod, "Delivery method values are not equal");
                    Assert.AreEqual(subscriptionOne.Description, actual[0].Description, "Description values are not equal");
                    Assert.AreEqual(subscriptionOne.Event_Type_ID, (int)actual[0].EventType, "Event type values are not equal");
                    Assert.AreEqual(subscriptionOne.Merchant_ID, actual[0].MerchantId, "Merchant ID values are not equal");
                    Assert.AreEqual(subscriptionOne.Subscribed_By, actual[0].SubscribedBy, "Subscribed by values are not equal");
                    Assert.AreEqual(subscriptionOne.Subscription_Date.ToString(), actual[0].SubscriptionDate.ToString(), "Subscription date values are not equal");
                    Assert.AreEqual(subscriptionOne.Subscription_Terminated, actual[0].SubscriptionTerminated, "Subscription terminated values are not equal");

                    Assert.AreEqual(subscriptionTwo.Created_By, actual[1].CreatedBy, "Create by values are not equal");
                    Assert.AreEqual(subscriptionTwo.Creation_Date.ToString(), actual[1].CreatedDate.ToString(), "Creation date values are not equal");
                    Assert.AreEqual(subscriptionTwo.Delivery_Address, actual[1].DeliveryAddress, "Delivery address values are not equal");
                    Assert.AreEqual(subscriptionTwo.Delivery_Method_ID, (int)actual[1].DeliveryMethod, "Delivery method values are not equal");
                    Assert.AreEqual(subscriptionTwo.Description, actual[1].Description, "Description values are not equal");
                    Assert.AreEqual(subscriptionTwo.Event_Type_ID, (int)actual[1].EventType, "Event type values are not equal");
                    Assert.AreEqual(subscriptionTwo.Merchant_ID, actual[1].MerchantId, "Merchant ID values are not equal");
                    Assert.AreEqual(subscriptionTwo.Subscribed_By, actual[1].SubscribedBy, "Subscribed by values are not equal");
                    Assert.AreEqual(subscriptionTwo.Subscription_Date.ToString(), actual[1].SubscriptionDate.ToString(), "Subscription date values are not equal");
                    Assert.AreEqual(subscriptionTwo.Subscription_Terminated, actual[1].SubscriptionTerminated, "Subscription terminated values are not equal");
                }
            }
            finally
            {
                db.Delete("Subscription", "ID", null, idOne);
                db.Delete("Subscription", "ID", null, idTwo);
            }
        }
    }
}
