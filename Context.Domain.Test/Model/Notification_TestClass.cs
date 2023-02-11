using Example.Notific.Context.Common;
using Example.Notific.Context.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Notific.Context.Domain.Test.Model
{
    [TestClass]
    public class Notification_TestClass
    {
        [TestMethod]
        public void Notification_RegisterDeliveryAttempt_Delivered()
        {
            var eventModel = new Event(EventType.DdPaymentDishonoured, DateTime.UtcNow, 20001, "content", "EventMap_", ContentType.Payment, Guid.NewGuid().ToString());

            var subscriptionModel = new Subscription(20001, EventType.DdPaymentDishonoured, DeliveryMethod.Email, "Address", "Description", "Subscribed_By", "SubscriptionMap_");

            var notificationModel = new Notification(eventModel, subscriptionModel, DeliveryMethod.Email, "Address", DateTime.UtcNow, NotificationStatus.Delivered, "ID", "NotificationMap_");

            var attemptModel = new DeliveryAttempt(notificationModel.Id, DateTime.UtcNow, "", "attempt_map", 200);

            notificationModel.RegisterDeliveryAttempt(attemptModel);

            Assert.IsNotNull(notificationModel.DeliveryAttempts, "Delivery attempts should not be null");
            Assert.AreEqual(1, notificationModel.DeliveryAttempts.Count(), "Delivery attempt should be 1");
            Assert.AreEqual(notificationModel.Status, NotificationStatus.Delivered, "Notification status should be delivered");
        }

        [TestMethod]
        public void Notification_RegisterDeliveryAttempt_Failed()
        {
            var eventModel = new Event(EventType.DdPaymentDishonoured, DateTime.UtcNow, 20001, "content", "EventMap_", ContentType.Payment, Guid.NewGuid().ToString());

            var subscriptionModel = new Subscription(20001, EventType.DdPaymentDishonoured, DeliveryMethod.Email, "Address", "Description", "Subscribed_By", "SubscriptionMap_");

            var notificationModel = new Notification(eventModel, subscriptionModel, DeliveryMethod.Email, "Address", DateTime.UtcNow, NotificationStatus.NotDelivered, "ID", "NotificationMap_");

            var attemptModel = new DeliveryAttempt(notificationModel.Id, DateTime.UtcNow, "", "attempt_map", 404);

            notificationModel.RegisterDeliveryAttempt(attemptModel);

            Assert.IsNotNull(notificationModel.DeliveryAttempts, "Delivery attempts should not be null");
            Assert.AreEqual(1, notificationModel.DeliveryAttempts.Count(), "Delivery attempt should be 1");
            Assert.AreEqual(notificationModel.Status, NotificationStatus.NotDelivered, "Notification status should be delivered");
        }

        [TestMethod]
        public void Notification_RegisterDeliveryAttempt_AttemptCount()
        {
            var eventModel = new Event(EventType.DdPaymentDishonoured, DateTime.UtcNow, 20001, "content", "EventMap_", ContentType.Payment, Guid.NewGuid().ToString());

            var subscriptionModel = new Subscription(20001, EventType.DdPaymentDishonoured, DeliveryMethod.Email, "Address", "Description", "Subscribed_By", "SubscriptionMap_");

            var notificationModel = new Notification(eventModel, subscriptionModel, DeliveryMethod.Email, "Address", DateTime.UtcNow, NotificationStatus.NotDelivered, "ID", "NotificationMap_");

            var attemptModel = new DeliveryAttempt(notificationModel.Id, DateTime.UtcNow, "", "attempt_map", 404);

            notificationModel.RegisterDeliveryAttempt(attemptModel);
            notificationModel.RegisterDeliveryAttempt(attemptModel);

            Assert.IsNotNull(notificationModel.DeliveryAttempts, "Delivery attempts should not be null");
            Assert.AreEqual(2, notificationModel.DeliveryAttempts.Count(), "Delivery attempt should be 2");

        }

        [TestMethod]
        public void Notification_RegisterDeliveryAttempt_Success72Hours()
        {
            var eventModel = new Event(EventType.DdPaymentDishonoured, DateTime.UtcNow, 20001, "content", "EventMap_", ContentType.Payment, Guid.NewGuid().ToString());

            var subscriptionModel = new Subscription(20001, EventType.DdPaymentDishonoured, DeliveryMethod.Email, "Address", "Description", "Subscribed_By", "SubscriptionMap_");

            var notificationModel = new Notification(eventModel, subscriptionModel, DeliveryMethod.Email, "Address", DateTime.UtcNow, NotificationStatus.NotDelivered, "ID", "NotificationMap_");

            var attemptModel = new DeliveryAttempt(notificationModel.Id, DateTime.UtcNow.AddHours(-(CommonConsts.NotificationExpireLimit+1)), "", "attempt_map", 404);

            notificationModel.RegisterDeliveryAttempt(attemptModel);

            var nextattemptModel = new DeliveryAttempt(notificationModel.Id, DateTime.UtcNow, "", "attempt_map", 404);

            notificationModel.RegisterDeliveryAttempt(attemptModel);

            Assert.IsNotNull(notificationModel.DeliveryAttempts, "Delivery attempts should not be null");
            Assert.AreEqual(2, notificationModel.DeliveryAttempts.Count(), "Delivery attempt should be 2");
            Assert.AreNotEqual(notificationModel.Status, NotificationStatus.Failed, "Notification status should not be failed");

        }

        [TestMethod]
        public void Notification_RegisterDeliveryAttempt_Failed72Hours()
        {
            var eventModel = new Event(EventType.DdPaymentDishonoured, DateTime.UtcNow, 20001, "content", "EventMap_", ContentType.Payment, Guid.NewGuid().ToString());

            var subscriptionModel = new Subscription(20001, EventType.DdPaymentDishonoured, DeliveryMethod.Email, "Address", "Description", "Subscribed_By", "SubscriptionMap_");

            var notificationModel = new Notification(eventModel, subscriptionModel, DeliveryMethod.Email, "Address", DateTime.UtcNow, NotificationStatus.NotDelivered, "ID", "NotificationMap_");

            var attemptModel = new DeliveryAttempt(notificationModel.Id, DateTime.UtcNow.AddHours(-(CommonConsts.NotificationExpireLimit-1)), "", "attempt_map", 404);

            notificationModel.RegisterDeliveryAttempt(attemptModel);

            var nextattemptModel = new DeliveryAttempt(notificationModel.Id, DateTime.UtcNow, "", "attempt_map", 404);

            notificationModel.RegisterDeliveryAttempt(attemptModel);

            Assert.IsNotNull(notificationModel.DeliveryAttempts, "Delivery attempts should not be null");
            Assert.AreEqual(2, notificationModel.DeliveryAttempts.Count(), "Delivery attempt should be 2");
            Assert.AreEqual(notificationModel.Status, NotificationStatus.Failed, "Notification status should be failed");

        }

        [TestMethod]
        public void Notification_RegisterDeliveryAttempt_Failed_StatusCode()
        {
            var eventModel = new Event(EventType.DdPaymentDishonoured, DateTime.UtcNow, 20001, "content", "EventMap_", ContentType.Payment, Guid.NewGuid().ToString());

            var subscriptionModel = new Subscription(20001, EventType.DdPaymentDishonoured, DeliveryMethod.Email, "Address", "Description", "Subscribed_By", "SubscriptionMap_");

            var notificationModel = new Notification(eventModel, subscriptionModel, DeliveryMethod.Email, "Address", DateTime.UtcNow, NotificationStatus.NotDelivered, "ID", "NotificationMap_");

            var attemptModel = new DeliveryAttempt(notificationModel.Id, DateTime.UtcNow, "", "attempt_map", -1);

            notificationModel.RegisterDeliveryAttempt(attemptModel);

            Assert.IsNotNull(notificationModel.DeliveryAttempts, "Delivery attempts should not be null");
            Assert.AreEqual(1, notificationModel.DeliveryAttempts.Count(), "Delivery attempt should be 1");
            Assert.AreEqual(notificationModel.Status, NotificationStatus.Failed, "Notification status should be failed");
        }
    }
}
