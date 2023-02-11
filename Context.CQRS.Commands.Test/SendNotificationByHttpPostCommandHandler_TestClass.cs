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
using Example.Notific.Context.Domain.Infrastructure.Interfaces;
using Example.Notific.Context.Domain.Infrastructure;
using System.Configuration;

namespace Example.Notific.Context.CQRS.Commands.Test
{
    [TestClass]
    public class SendNotificationByHttpPostCommandHandler_TestClass
    {

        [TestMethod]
        public void SendNotificationByHttpPostCommandHandler_Ctor()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            Mock<IMerchantConfigRepository> merchantConfigRepository = new Mock<IMerchantConfigRepository>();
            SendNotificationByHttpPostCommandHandler handler = new SendNotificationByHttpPostCommandHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object, merchantConfigRepository.Object);

            Assert.AreSame(storage.Object, handler._storage, "Storage repoistory are not same");
            Assert.AreSame(subscriptionRepo.Object, handler._subscriptionRepository, "Subscription repoistory are not same");
            Assert.AreSame(eventRepo.Object, handler._eventRepository, "Event repoistory are not same");
            Assert.AreSame(notificationRepo.Object, handler._notificationRepository, "Notification repoistory are not same");
            Assert.AreSame(httpPostService.Object, handler._httpPoster, "Http post service are not same");
            Assert.AreSame(merchantConfigRepository.Object, handler._merchantConfigRepository, "Merchant config repository are not same");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendNotificationByHttpPostCommandHandler_Stroage_Null()
        {
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            Mock<IMerchantConfigRepository> merchantConfigRepository = new Mock<IMerchantConfigRepository>();
            SendNotificationByHttpPostCommandHandler handler = new SendNotificationByHttpPostCommandHandler(null, subscriptionRepo.Object, eventRepo.Object, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object, merchantConfigRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendNotificationByHttpPostCommandHandler_Subscription_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            Mock<IMerchantConfigRepository> merchantConfigRepository = new Mock<IMerchantConfigRepository>();
            SendNotificationByHttpPostCommandHandler handler = new SendNotificationByHttpPostCommandHandler(storage.Object, null, eventRepo.Object, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object, merchantConfigRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendNotificationByHttpPostCommandHandler_Event_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            Mock<IMerchantConfigRepository> merchantConfigRepository = new Mock<IMerchantConfigRepository>();
            SendNotificationByHttpPostCommandHandler handler = new SendNotificationByHttpPostCommandHandler(storage.Object, subscriptionRepo.Object, null, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object, merchantConfigRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendNotificationByHttpPostCommandHandler_Notification_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            Mock<IMerchantConfigRepository> merchantConfigRepository = new Mock<IMerchantConfigRepository>();
            SendNotificationByHttpPostCommandHandler handler = new SendNotificationByHttpPostCommandHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, null, httpPostService.Object, notificationJsonGenerator.Object, merchantConfigRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendNotificationByHttpPostCommandHandler_HttpPost_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            Mock<IMerchantConfigRepository> merchantConfigRepository = new Mock<IMerchantConfigRepository>();
            SendNotificationByHttpPostCommandHandler handler = new SendNotificationByHttpPostCommandHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, notificationRepo.Object, null, notificationJsonGenerator.Object,merchantConfigRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendNotificationByHttpPostCommandHandler_JSONGenerator_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<IMerchantConfigRepository> merchantConfigRepository = new Mock<IMerchantConfigRepository>();
            SendNotificationByHttpPostCommandHandler handler = new SendNotificationByHttpPostCommandHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, notificationRepo.Object, httpPostService.Object, null,merchantConfigRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendNotificationByHttpPostCommandHandler_Command_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            Mock<IMerchantConfigRepository> merchantConfigRepository = new Mock<IMerchantConfigRepository>();
            SendNotificationByHttpPostCommandHandler handler = new SendNotificationByHttpPostCommandHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object,merchantConfigRepository.Object);

            handler.Handle(null);
        }

        [TestMethod]
        [ExpectedException(typeof(SubscriptionNotFoundException))]
        public void SendNotificationByHttpPostCommandHandler_SubscriptionNotFound()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            Mock<IMerchantConfigRepository> merchantConfigRepository = new Mock<IMerchantConfigRepository>();
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

            eventRepo
               .Setup(t => t.Get(It.IsAny<Guid>()))
               .Returns(new Event());

            SendNotificationByHttpPostCommandHandler handler = new SendNotificationByHttpPostCommandHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object,merchantConfigRepository.Object);

            SendNotificationByHttpPostCommand command = new SendNotificationByHttpPostCommand() { SubscriptionId = 1, EventId = Guid.NewGuid() };

            handler.Handle(command);
        }

        [TestMethod]
        [ExpectedException(typeof(SubscriptionNotActiveException))]
        public void SendNotificationByHttpPostCommandHandler_SubscriptionNotActiveException()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<Subscription> subscriptionModel = new Mock<Subscription>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            Mock<IMerchantConfigRepository> merchantConfigRepository = new Mock<IMerchantConfigRepository>();
            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            subscriptionRepo
                .Setup(t => t.Get(It.IsAny<long>()))
                .Returns(subscriptionModel.Object);

            eventRepo
               .Setup(t => t.Get(It.IsAny<Guid>()))
               .Returns(new Event());

            subscriptionModel.SetupGet(s => s.SubscriptionTerminated).Returns(true);

            SendNotificationByHttpPostCommandHandler handler = new SendNotificationByHttpPostCommandHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object,merchantConfigRepository.Object);

            SendNotificationByHttpPostCommand command = new SendNotificationByHttpPostCommand() { SubscriptionId = 1, EventId = Guid.NewGuid() };

            handler.Handle(command);
        }

        [TestMethod]
        [ExpectedException(typeof(EventNotFoundException))]
        public void SendNotificationByHttpPostCommandHandler_EventNotFoundException()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            Mock<IMerchantConfigRepository> merchantConfigRepository = new Mock<IMerchantConfigRepository>();
            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            eventRepo
               .Setup(t => t.Get(It.IsAny<Guid>()))
               .Returns((Event)null);

            SendNotificationByHttpPostCommandHandler handler = new SendNotificationByHttpPostCommandHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object,merchantConfigRepository.Object);

            SendNotificationByHttpPostCommand command = new SendNotificationByHttpPostCommand() { SubscriptionId = 1, EventId = Guid.NewGuid() };

            handler.Handle(command);
        }

        [TestMethod]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Ignore]
        public void SendNotificationByHttpPostCommandHandler_Handle()
        {
            long subscriptionId = 1;
            Guid eventId = Guid.NewGuid();

            string endPoint = ConfigurationManager.AppSettings["ReqApiUrl"].ToString() + "OKRequest";

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<ISubscriptionRepository> subscriptionRepo = new Mock<ISubscriptionRepository>();
            Mock<IEventRepository> eventRepo = new Mock<IEventRepository>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            Mock<Subscription> subscriptionModel = new Mock<Subscription>();
            Mock<Event> eventModel = new Mock<Event>();
            Mock<IMerchantConfigRepository> merchantConfigRepository = new Mock<IMerchantConfigRepository>();

            subscriptionModel.SetupGet(s => s.DeliveryAddress).Returns(endPoint);
            subscriptionModel.SetupGet(s => s.SubscriptionTerminated).Returns(false);
            subscriptionModel.SetupGet(s => s.Id).Returns(subscriptionId);

            eventModel.SetupGet(s => s.Content).Returns("{a:\"test\",b:\"data\" }");
            eventModel.SetupGet(s => s.Id).Returns(eventId);

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            eventRepo
               .Setup(t => t.Get(It.IsAny<Guid>()))
               .Returns(eventModel.Object);

            subscriptionRepo
              .Setup(t => t.Get(It.IsAny<long>()))
              .Returns(subscriptionModel.Object);

            httpPostService
             .Setup(t => t.Post(It.IsAny<string>(), It.IsAny<string>()))
             .Returns(new HttpPostResult() { HttpStatusCode = 400, Message = "error" });

            SendNotificationByHttpPostCommandHandler handler = new SendNotificationByHttpPostCommandHandler(storage.Object, subscriptionRepo.Object, eventRepo.Object, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object,merchantConfigRepository.Object);

            SendNotificationByHttpPostCommand command = new SendNotificationByHttpPostCommand() { SubscriptionId = subscriptionId, EventId = eventId };

            handler.Handle(command);

            notificationRepo.Verify(t => t.Save(It.IsAny<Notification>()), Times.Once());
            notificationRepo.Verify(t => t.Update(It.IsAny<Notification>()), Times.Once());

        }

        [TestMethod]
        [TestCategory("Integration")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Ignore]
        public void SendNotificationByHttpPostCommandHandler_Handle_SuccessDelivery()
        {
            var db = new Database("NotDataBase");

            string endPoint = ConfigurationManager.AppSettings["ReqApiUrl"].ToString() + "OKRequest";

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<ICommandHandler<SendNotificationByHttpPostCommand>>();

            long subscriptionId = 0;
            Guid eventId = Guid.NewGuid();
            long notificationId = 0;

            try
            {

                var subscription = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 1,
                    Merchant_ID = 20001,
                    Delivery_Address = endPoint,
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
                    Content_Type_ID = 1
                };

                eventId = (Guid)db.Insert("Event", "Id", false, eventModel);

                SendNotificationByHttpPostCommand command = new SendNotificationByHttpPostCommand() { SubscriptionId = subscriptionId, EventId = eventId };

                handler.Handle(command);

                var actual = db.SingleOrDefault<NotificationPetaPoco>("SELECT * FROM Notification where Event_ID=@0 and Subscription_ID=@1", eventId, subscriptionId);

                Assert.IsNotNull(actual, "There should be record exist for notification");

                Assert.AreEqual(actual.Notification_Status_ID, (int)NotificationStatus.Delivered, "Status should be delivered");

                notificationId = actual.Id;

                var attempt = db.SingleOrDefault<DeliveryAttemptPetaPoco>("SELECT * FROM Notification_Delivery_Attempt WHERE Notification_ID=@0", notificationId);

                Assert.IsNotNull(attempt, "There should be record exist for delivery attempt");

                Assert.IsTrue(attempt.Delivery_Successful, "There should be success delivery");

                Assert.AreEqual(actual.Notification_Sent.ToString(), attempt.Delivery_Timestamp.ToString(), "Sent date should be equal to timestamp");
            }
            finally
            {
                db.Execute("delete from Notification_Delivery_Attempt where Notification_ID='" + notificationId + "'");
                db.Execute("delete from Notification where id='" + notificationId + "'");
                db.Execute("delete from Subscription where id='" + subscriptionId + "'");
                db.Execute("delete from Event where id='" + eventId.ToString() + "'");
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Ignore]
        public void SendNotificationByHttpPostCommandHandler_Handle_FailureDelivery()
        {
            var db = new Database("NotDataBase");

            string endPoint = ConfigurationManager.AppSettings["ReqApiUrl"].ToString() + "NotFoundRequest";

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<ICommandHandler<SendNotificationByHttpPostCommand>>();

            long subscriptionId = 0;
            Guid eventId = Guid.NewGuid();
            long notificationId = 0;

            try
            {

                var subscription = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 1,
                    Merchant_ID = 20001,
                    Delivery_Address = endPoint,
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
                    Content_Type_ID = 1
                };

                eventId = (Guid)db.Insert("Event", "Id", false, eventModel);

                SendNotificationByHttpPostCommand command = new SendNotificationByHttpPostCommand() { SubscriptionId = subscriptionId, EventId = eventId };

                handler.Handle(command);

                var actual = db.SingleOrDefault<NotificationPetaPoco>("SELECT * FROM Notification where Event_ID=@0 and Subscription_ID=@1", eventId, subscriptionId);

                Assert.IsNotNull(actual, "There should be record exist for notification");

                Assert.AreEqual(actual.Notification_Status_ID, (int)NotificationStatus.NotDelivered, "Status should be notdelivered");

                notificationId = actual.Id;

                var attempt = db.SingleOrDefault<DeliveryAttemptPetaPoco>("SELECT * FROM Notification_Delivery_Attempt WHERE Notification_ID=@0", notificationId);

                Assert.IsNotNull(attempt, "There should be record exist for delivery attempt");

                Assert.IsFalse(attempt.Delivery_Successful, "There should be failure delivery");
            }
            finally
            {
                db.Execute("delete from Notification_Delivery_Attempt where Notification_ID='" + notificationId + "'");
                db.Execute("delete from Notification where id='" + notificationId + "'");
                db.Execute("delete from Subscription where id='" + subscriptionId + "'");
                db.Execute("delete from Event where id='" + eventId + "'");
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void SendNotificationByHttpPostCommandHandler_Handle_Url_Blank()
        {
            var db = new Database("NotDataBase");

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<ICommandHandler<SendNotificationByHttpPostCommand>>();

            long subscriptionId = 0;
            Guid eventId = Guid.NewGuid();
            long notificationId = 0;

            try
            {

                var subscription = new SubscriptionPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.Now,
                    Event_Type_ID = 1,
                    Merchant_ID = 20001,
                    Delivery_Address = "",
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
                    Content_Type_ID = 1
                };

                eventId = (Guid)db.Insert("Event", "Id", false, eventModel);

                SendNotificationByHttpPostCommand command = new SendNotificationByHttpPostCommand() { SubscriptionId = subscriptionId, EventId = eventId };

                handler.Handle(command);

                var actual = db.SingleOrDefault<NotificationPetaPoco>("SELECT * FROM Notification where Event_ID=@0 and Subscription_ID=@1", eventId.ToString(), subscriptionId);

                Assert.IsNotNull(actual, "There should be record exist for notification");

                Assert.AreEqual(actual.Notification_Status_ID, (int)NotificationStatus.Failed, "Status should be failed");

                notificationId = actual.Id;

                var attempt = db.SingleOrDefault<DeliveryAttemptPetaPoco>("SELECT * FROM Notification_Delivery_Attempt WHERE Notification_ID=@0", notificationId);

                Assert.IsNotNull(attempt, "There should be record exist for delivery attempt");

                Assert.IsFalse(attempt.Delivery_Successful, "There should be failure delivery");
            }
            finally
            {
                db.Execute("delete from Notification_Delivery_Attempt where Notification_ID='" + notificationId + "'");
                db.Execute("delete from Notification where id='" + notificationId + "'");
                db.Execute("delete from Subscription where id='" + subscriptionId + "'");
                db.Execute("delete from Event where id='" + eventId.ToString() + "'");
            }
        }
    }
}
