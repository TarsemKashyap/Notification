using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using Example.Notific.Context.SimpleInjector;
using Example.Notific.PetaPoco;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetaPoco;
using SimpleInjector;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Common;

namespace Example.Notific.Context.Domain.Test.Mapping
{
    [TestClass]
    public class DeliveryAttemptMap_TestClass
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void DeliveryAttemptMap_()
        {
            var db = new Database("NotDatabase");

            long subscriptionId = 0;
            Guid eventId = Guid.NewGuid();
            long notificationId = 0;
            long id = 0;

            try
            {

                var eventModel = new Event(EventType.DdPaymentDishonoured, DateTime.UtcNow, 20001, "content", "EventMap_", ContentType.Payment, Guid.NewGuid().ToString());

                var subscriptionModel = new Subscription(20001, EventType.DdPaymentDishonoured, DeliveryMethod.Email, "Address", "Description", "Subscribed_By", "SubscriptionMap_");

                Container container = new Container();

                NotBootstrapper.Bootstrap(container, null, true);

                var notificationRepo = container.GetInstance<INotificationRepository>();

                var storage = container.GetInstance<IUnitOfWorkStorage>();

                var eventRepo = container.GetInstance<IEventRepository>();

                var subscriptionRepo = container.GetInstance<ISubscriptionRepository>();


                using (var uow = storage.NewUnitOfWork())
                {
                    using (var tran = uow.BeginTransaction())
                    {
                        eventRepo.Save(eventModel);

                        tran.Commit();

                        eventId = eventModel.Id;
                    }

                    using (var tran = uow.BeginTransaction())
                    {
                        subscriptionRepo.Save(subscriptionModel);

                        tran.Commit();

                        subscriptionId = subscriptionModel.Id;
                    }

                    var notificationModel = new Notification(eventModel, subscriptionModel, DeliveryMethod.Email, "Address", DateTime.UtcNow, NotificationStatus.Delivered, "ID", "NotificationMap_");

                    using (var tran = uow.BeginTransaction())
                    {
                        notificationRepo.Save(notificationModel);

                        tran.Commit();

                        notificationId = notificationModel.Id;
                    }

                    var attemptModel = new DeliveryAttempt(notificationId, DateTime.UtcNow, "", "attempt_map", 200);

                    notificationModel.RegisterDeliveryAttempt(attemptModel);

                    using (var tran = uow.BeginTransaction())
                    {
                        notificationRepo.Update(notificationModel);

                        tran.Commit();
                    }

                    var actual = notificationRepo.Get(notificationId).DeliveryAttempts.Where(i => i.NotificationId == notificationId).FirstOrDefault();

                    id = actual.Id;

                    Assert.AreEqual(attemptModel.CreatedBy, actual.CreatedBy, "Create by values are not equal");
                    Assert.AreEqual(attemptModel.CreatedDate.ToString(), actual.CreatedDate.ToString(), "Creation date values are not equal");
                    Assert.AreEqual(attemptModel.FailureReason, actual.FailureReason, "Delivery failure reason values are not equal");
                    Assert.AreEqual(attemptModel.Successful, actual.Successful, "Delivery successful values are not equal");
                    Assert.AreEqual(attemptModel.Timestamp.ToString(), actual.Timestamp.ToString(), "Timestamp values are not equal");
                    Assert.AreEqual(attemptModel.NotificationId, actual.NotificationId, "Notification ID values are not equal");
                    Assert.AreEqual(attemptModel.HttpResponseCode, actual.HttpResponseCode, "Http response code values are not equal");

                }
            }
            finally
            {
                db.Execute("delete from Notification_Delivery_Attempt where id='" + id + "'");
                db.Execute("delete from Notification where id='" + notificationId + "'");
                db.Execute("delete from Subscription where id='" + subscriptionId + "'");
                db.Execute("delete from Event where id='" + eventId + "'");
            }
        }
    }
}
