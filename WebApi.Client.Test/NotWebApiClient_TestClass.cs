using F2C.MAP.NOT.Context.Common;
using F2C.MAP.NOT.PetaPoco;
using F2C.MAP.NOT.WebApi.Contract.Requests;
using F2C.MAP.NOT.WebApi.Contract.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;

namespace F2C.MAP.NOT.WebApi.Client.Test
{
    [TestClass]
    public class NotWebApiClient_TestClass
    {
        string BaseUrl { get { return ConfigurationManager.AppSettings["NotWebApiBaseUri"]; } }

        [TestMethod]
        [TestCategory("Integration")]
        public void NotWebApiClient_GetEventSubscription()
        {
            long subscriptionId = 0;

            var db = new Database("NotDatabase");
            try
            {
                var subscription = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
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

                subscriptionId = (long)db.Insert("Subscription", "Id", subscription);

                INotWebApiClient client = new NotWebApiClient(BaseUrl);

                var result = client.GetEventSubscription(subscriptionId);

                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status should be OK");

                var actual = result.Resource as Subscription;

                Assert.AreEqual(subscription.Subscription_Date.ToString("s") + "Z", actual.DateSubscribedUtc, "Date subscribed values are not equal");
                Assert.AreEqual(subscription.Merchant_ID, actual.MerchantId, "Merchant ID values are not equal");
                Assert.AreEqual(subscription.Delivery_Address, actual.Delivery.Address, "Delivery Address values are not equal");
                Assert.AreEqual(((DeliveryMethod)subscription.Delivery_Method_ID).ToString(), actual.Delivery.Method, "Delivery method values are not equal");
                Assert.AreEqual(subscription.Description, actual.Description, "Description values are not equal");
                Assert.AreEqual(subscription.Event_Type_ID, actual.EventType, "Event type values are not equal");
                Assert.AreEqual(subscription.Subscribed_By, actual.Subscriber, "Subscriber values are not equal");

            }
            finally
            {
                db.Delete("Subscription", "ID", null, subscriptionId);
            }

        }

        [TestMethod]
        [TestCategory("Integration")]
        public void NotWebApiClient_CreatEventSubscription()
        {
            INotWebApiClient client = new NotWebApiClient(BaseUrl);

            CreateEventSubscriptionRequest request = new CreateEventSubscriptionRequest()
            {
                Description = "Description",
                Event = 1,
                MerchantId = 20001,
                Subscriber = "abc@yahoo.com",
                Delivery = new Contract.Requests.Delivery()
                {
                    Address = "abc@yahoo.com",
                    Method = "Email"
                }
            };

            var result = client.CreatEventSubscription(request);

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

            Assert.IsInstanceOfType(result.Resource, typeof(Subscription));

            var actual = result.Resource as Subscription;
         
            Assert.AreEqual(request.MerchantId, actual.MerchantId, "Merchant ID values are not equal");
            Assert.AreEqual(request.Delivery.Address, actual.Delivery.Address, "Delivery Address values are not equal");
            Assert.AreEqual(request.Delivery.Method, actual.Delivery.Method, "Delivery method values are not equal");
            Assert.AreEqual(request.Description, actual.Description, "Description values are not equal");
            Assert.AreEqual(request.Event, actual.EventType, "Event type values are not equal");
            Assert.AreEqual(request.Subscriber, actual.Subscriber, "Subscriber values are not equal");

        }

        [TestMethod]
        [TestCategory("Integration")]
        public void NotWebApiClient_RemoveEventSubscription()
        {
            long subscriptionId = 0;

            var db = new Database("NotDatabase");
            try
            {
                var subscription = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
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

                subscriptionId = (long)db.Insert("Subscription", "Id", subscription);

                INotWebApiClient client = new NotWebApiClient(BaseUrl);

                var result = client.RemoveEventSubscription(subscriptionId);

                Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode, "Status should be No content");               

            }
            finally
            {
                db.Delete("Subscription", "ID", null, subscriptionId);
            }

        }

        [TestMethod]
        [TestCategory("Integration")]
        public void NotWebApiClient_GetMerchantSubscriptions()
        {
            long subscriptionId = 0;
            int merchantId = 20001;

            var db = new Database("NotDatabase");
            try
            {
                var subscription = new SubscriptionPetaPoco()
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

                subscriptionId = (long)db.Insert("Subscription", "Id", subscription);

                INotWebApiClient client = new NotWebApiClient(BaseUrl);

                var result = client.GetMerchantSubscriptions(merchantId);

                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status should be OK");              

            }
            finally
            {
                db.Delete("Subscription", "ID", null, subscriptionId);
            }

        }

        [TestMethod]
        [TestCategory("Integration")]
        public void NotWebApiClient_GetEvent()
        {
            long eventId = 0;

            var db = new Database("NotDatabase");
            try
            {
                var eventModel = new EventPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 1,
                    Merchant_ID = 20001,
                    Event_Content = "{a:\"test\",b:\"data\" }",
                    Event_Received = DateTime.UtcNow
                };

                eventId = (long)db.Insert("Event", "Id", eventModel);

                INotWebApiClient client = new NotWebApiClient(BaseUrl);

                var result = client.GetEvent(eventId);

                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Status should be OK");

                var actual = result.Resource as Event;

                Assert.AreEqual(eventModel.Merchant_ID, actual.MerchantId, "Merchant ID values are not equal");
                Assert.AreEqual(eventModel.Event_Content, actual.Content, "Content values are not equal");             
                Assert.AreEqual(eventModel.Event_Type_ID, actual.Type, "Event type are not equal");
                Assert.AreEqual(eventModel.Event_Received.ToString("s") + "Z", actual.Received, "Event received values are not equal");

            }
            finally
            {
                db.Delete("Event", "ID", null, eventId);
            }

        }

    }
}
