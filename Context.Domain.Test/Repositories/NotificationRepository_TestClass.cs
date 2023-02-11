using Example.Common.Context.DDD.UnitOfWork;
using Example.Notific.Context.Common;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.SimpleInjector;
using Example.Notific.PetaPoco;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetaPoco;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Notific.Context.Domain.Test.Repositories
{
    [TestClass]
    public class NotificationRepository_TestClass
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void NotificationRepository_GetNotificationsToRetry_Integration()
        {
            var db = new Database("NotDataBase");

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            long subscriptionId = 0;
            Guid eventId = Guid.NewGuid();
            long notificationId = 0;
            long attemptId = 0;

            try
            {

                var subscription = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 1,
                    Merchant_ID = 20001,
                    Delivery_Address = "test",
                    Delivery_Method_ID = 1,
                    Description = "Description",
                    Subscribed_By = "Subscribed_By",
                    Subscription_Date = DateTime.Now,
                    Subscription_Terminated = false,
                    Termination_Date = DateTime.Now
                };

                subscriptionId = (long)db.Insert("Subscription", "Id", subscription);

                var eventModel = new EventPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 1,
                    Merchant_ID = 20001,
                    Event_Content = "{a:\"test\",b:\"data\" }",
                    Event_Received = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    Content_Type_ID = 1,
                    Event_Secret = Guid.NewGuid().ToString()
                };

                eventId = (Guid)db.Insert("Event", "Id", false, eventModel);

                var notification = new NotificationPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.UtcNow,
                    Delivery_Address = "Test",
                    Delivery_Method_ID = 1,
                    Event_ID = eventId,
                    Notification_Sent = DateTime.UtcNow,
                    Notification_Status_ID = (int)NotificationStatus.NotDelivered,
                    Subscription_ID = subscriptionId
                };

                notificationId = (long)db.Insert("Notification", "Id", notification);

                var deliveryAttempt = new DeliveryAttemptPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.UtcNow,
                    Delivery_Timestamp = DateTime.UtcNow.AddHours(-(CommonConsts.NotificationExpireLimit - 1)),
                    Delivery_Failure_Reason = "Test",
                    Delivery_Http_Response_Code = 404,
                    Notification_ID = notificationId,
                    Delivery_Successful = false
                };

                attemptId = (long)db.Insert("Notification_Delivery_Attempt", "Id", deliveryAttempt);

                var storage = container.GetInstance<IUnitOfWorkStorage>();

                var repo = container.GetInstance<INotificationRepository>();

                using (var uow = storage.NewUnitOfWork())
                {
                    var actual = repo.GetNotificationsToRetry();

                    Assert.AreNotEqual(actual.Count(), 0, "Attempt count should be equal to 1");

                    Assert.AreEqual(notification.Created_By, actual[0].CreatedBy, "Create by values are not equal");
                    Assert.AreEqual(notification.Creation_Date.ToString(), actual[0].CreatedDate.ToString(), "Creation date values are not equal");
                    Assert.AreEqual(notification.Delivery_Address, actual[0].DeliveryAddress, "Delivery address values are not equal");
                    Assert.AreEqual(notification.Delivery_Method_ID, (int)actual[0].DeliveryMethod, "Delivery method values are not equal");
                    Assert.AreEqual(notification.Event_ID, actual[0].Event.Id, "Event ID values are not equal");
                    Assert.AreEqual(notification.Notification_Sent.ToString(), actual[0].Sent.ToString(), "Notification sent values are not equal");
                    Assert.AreEqual(notification.Notification_Status_ID, (int)actual[0].Status, "Notification status values are not equal");
                    Assert.AreEqual(notification.Subscription_ID, actual[0].GeneratedBy.Id, "Subscription ID values are not equal");
                }
            }
            finally
            {
                db.Execute("delete from Notification_Delivery_Attempt where Notification_ID='" + notificationId + "'");
                db.Execute("delete from Notification where id='" + notificationId + "'");
                db.Execute("delete from Subscription where id='" + subscriptionId + "'");
                db.Execute("delete from Event where id='" + eventId + "'");
            }
        }
    }
}
