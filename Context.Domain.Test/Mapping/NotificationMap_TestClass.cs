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
    public class NotificationMap_TestClass
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void NotificationMap_()
        {
            var db = new Database("NotDatabase");

            long subscriptionId = 0;
            Guid eventId = Guid.NewGuid();
            long id = 0;

            try
            {

                var eventModel = new Event(EventType.DdPaymentDishonoured, DateTime.UtcNow, 20001, "content", "EventMap_", ContentType.Payment, Guid.NewGuid().ToString());

                var subscriptionModel = new Subscription(20001, EventType.DdPaymentDishonoured, DeliveryMethod.Email, "Address", "Description", "Subscribed_By", "SubscriptionMap_");

                Container container = new Container();

                NotBootstrapper.Bootstrap(container, null, true);

                var repo = container.GetInstance<INotificationRepository>();

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

                    var notificationModel = new Notification(eventModel,subscriptionModel,DeliveryMethod.Email,"Address",DateTime.UtcNow,NotificationStatus.Delivered,"ID", "NotificationMap_");

                    using (var tran = uow.BeginTransaction())
                    {
                        repo.Save(notificationModel);

                        tran.Commit();

                        id = notificationModel.Id;
                    }

                    var actual = repo.Get(id);

                    Assert.AreEqual(notificationModel.CreatedBy, actual.CreatedBy, "Create by values are not equal");
                    Assert.AreEqual(notificationModel.CreatedDate.ToString(), actual.CreatedDate.ToString(), "Creation date values are not equal");
                    Assert.AreEqual(notificationModel.DeliveryAddress, actual.DeliveryAddress, "Delivery address values are not equal");
                    Assert.AreEqual(notificationModel.DeliveryMethod, actual.DeliveryMethod, "Delivery method values are not equal");
                    Assert.AreEqual(notificationModel.CommsTrackingId, actual.CommsTrackingId, "Comms tracking id values are not equal");
                    Assert.AreEqual(notificationModel.Event.Id, actual.Event.Id, "Event ID values are not equal");
                    Assert.AreEqual(notificationModel.Sent.ToString(), actual.Sent.ToString(), "Notification sent values are not equal");
                    Assert.AreEqual(notificationModel.Status, actual.Status, "Notification status values are not equal");
                    Assert.AreEqual(notificationModel.GeneratedBy.Id, actual.GeneratedBy.Id, "Subscription ID values are not equal");

                }
            }
            finally
            {
                db.Execute("delete from Notification where id='" + id + "'");
                db.Execute("delete from Subscription where id='" + subscriptionId + "'");
                db.Execute("delete from Event where id='" + eventId + "'");
            }
        }
    }
}
