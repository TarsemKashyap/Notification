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
    public class RetryNotificationByHttpPostCommandHandler_TestClass
    {
        [TestMethod]
        public void RetryNotificationByHttpPostCommandHandler_Ctor()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();

            RetryNotificationByHttpPostCommandHandler handler = new RetryNotificationByHttpPostCommandHandler(storage.Object, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object);

            Assert.AreSame(storage.Object, handler._storage, "Storage repoistory are not same");
            Assert.AreSame(notificationRepo.Object, handler._notificationRepository, "Notification repoistory are not same");
            Assert.AreSame(httpPostService.Object, handler._httpPoster, "Http post service are not same");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetryNotificationByHttpPostCommandHandler_Stroage_Null()
        {
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            RetryNotificationByHttpPostCommandHandler handler = new RetryNotificationByHttpPostCommandHandler(null, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetryNotificationByHttpPostCommandHandler_Notification_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            RetryNotificationByHttpPostCommandHandler handler = new RetryNotificationByHttpPostCommandHandler(storage.Object, null, httpPostService.Object, notificationJsonGenerator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetryNotificationByHttpPostCommandHandler_HttpPost_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            RetryNotificationByHttpPostCommandHandler handler = new RetryNotificationByHttpPostCommandHandler(storage.Object, notificationRepo.Object, null, notificationJsonGenerator.Object);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetryNotificationByHttpPostCommandHandler_NotificationJsonGenerator_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            RetryNotificationByHttpPostCommandHandler handler = new RetryNotificationByHttpPostCommandHandler(storage.Object, notificationRepo.Object, httpPostService.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RetryNotificationByHttpPostCommandHandler_Command_Null()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            RetryNotificationByHttpPostCommandHandler handler = new RetryNotificationByHttpPostCommandHandler(storage.Object, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object);

            handler.Handle(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotificationNotFoundException))]
        public void RetryNotificationByHttpPostCommandHandler_NotificationNotFoundException()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            notificationRepo
                .Setup(t => t.Get(It.IsAny<long>()))
                .Returns((Notification)null);


            RetryNotificationByHttpPostCommandHandler handler = new RetryNotificationByHttpPostCommandHandler(storage.Object, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object);

            RetryNotificationByHttpPostCommand command = new RetryNotificationByHttpPostCommand() { NotificationId = 1 };

            handler.Handle(command);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RetryNotificationByHttpPostCommandHandler_InvalidOperationException()
        {

            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<Notification> notificationModel = new Mock<Notification>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            notificationRepo
                .Setup(t => t.Get(It.IsAny<long>()))
                .Returns(notificationModel.Object);

            notificationModel.SetupGet(s => s.Status).Returns(NotificationStatus.Delivered);

            RetryNotificationByHttpPostCommandHandler handler = new RetryNotificationByHttpPostCommandHandler(storage.Object, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object);

            RetryNotificationByHttpPostCommand command = new RetryNotificationByHttpPostCommand() { NotificationId = 1 };

            handler.Handle(command);
        }

        [TestMethod]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Ignore]
        public void RetryNotificationByHttpPostCommandHandler_Handle()
        {
            Mock<IUnitOfWorkStorage> storage = new Mock<IUnitOfWorkStorage>();
            Mock<INotificationRepository> notificationRepo = new Mock<INotificationRepository>();
            Mock<IHttpPoster> httpPostService = new Mock<IHttpPoster>();
            Mock<INotificationJsonGenerator> notificationJsonGenerator = new Mock<INotificationJsonGenerator>();
            Mock<Notification> notificationModel = new Mock<Notification>();
            Mock<Event> eventModel = new Mock<Event>();

            string endPoint = ConfigurationManager.AppSettings["ReqApiUrl"].ToString() + "OKRequest";

            eventModel.SetupGet(s => s.Content).Returns("{a:\"test\",b:\"data\" }");

            notificationModel.SetupGet(s => s.Status).Returns(NotificationStatus.NotDelivered);
            notificationModel.SetupGet(s => s.DeliveryAddress).Returns(endPoint);
            notificationModel.SetupGet(s => s.Event).Returns(eventModel.Object);

            var uow = new Mock<IUnitOfWork>();

            uow
                .Setup(t => t.BeginTransaction())
                .Returns(new Mock<IGenericTransaction>().Object);

            storage
                .Setup(t => t.NewUnitOfWork())
                .Returns(uow.Object);

            notificationRepo
              .Setup(t => t.Get(It.IsAny<long>()))
              .Returns(notificationModel.Object);

            httpPostService
           .Setup(t => t.Post(It.IsAny<string>(), It.IsAny<string>()))
           .Returns(new HttpPostResult() { HttpStatusCode = 400, Message = "error" });

            RetryNotificationByHttpPostCommandHandler handler = new RetryNotificationByHttpPostCommandHandler(storage.Object, notificationRepo.Object, httpPostService.Object, notificationJsonGenerator.Object);

            RetryNotificationByHttpPostCommand command = new RetryNotificationByHttpPostCommand() { NotificationId = 1 };

            handler.Handle(command);

            notificationRepo.Verify(t => t.Update(It.IsAny<Notification>()), Times.Once());
        }

        [TestMethod]
        [TestCategory("Integration")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Ignore]
        public void RetryNotificationByHttpPostCommandHandler_Handle_SuccessDelivery()
        {
            var db = new Database("NotDataBase");

            string endPoint = ConfigurationManager.AppSettings["ReqApiUrl"].ToString() + "OKRequest";

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<ICommandHandler<RetryNotificationByHttpPostCommand>>();

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

                var notification = new NotificationPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.UtcNow,
                    Delivery_Address = endPoint,
                    Delivery_Method_ID = 1,
                    Event_ID = eventId,
                    Notification_Sent = DateTime.UtcNow,
                    Notification_Status_ID = (int)NotificationStatus.NotDelivered,
                    Subscription_ID = subscriptionId
                };

                notificationId = (long)db.Insert("Notification", "Id", notification);

                RetryNotificationByHttpPostCommand command = new RetryNotificationByHttpPostCommand() { NotificationId = notificationId };

                handler.Handle(command);

                var repo = container.GetInstance<INotificationRepository>();
                var storage = container.GetInstance<IUnitOfWorkStorage>();

                using (var uow = storage.NewUnitOfWork())
                {
                    var actual = repo.Get(notificationId);

                    Assert.AreEqual(actual.Status, NotificationStatus.Delivered, "Status should be delivered");

                    var attempt = actual.DeliveryAttempts.OrderByDescending(i => i.Id).FirstOrDefault();

                    Assert.IsNotNull(attempt, "There should be record exist for delivery attempt");

                    Assert.IsTrue(attempt.Successful, "This should be success delivery");

                    Assert.AreEqual(actual.Sent.ToString(), attempt.Timestamp.ToString(), "Sent date should be equal to timestamp");
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

        [TestMethod]
        [TestCategory("Integration")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Ignore]
        public void RetryNotificationByHttpPostCommandHandler_Handle_FaliureDelivery()
        {
            var db = new Database("NotDataBase");

            string endPoint = ConfigurationManager.AppSettings["ReqApiUrl"].ToString() + "NotFoundRequest";

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<ICommandHandler<RetryNotificationByHttpPostCommand>>();

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

                eventId = (Guid)db.Insert("Event", "Id",false, eventModel);

                var notification = new NotificationPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.UtcNow,
                    Delivery_Address = endPoint,
                    Delivery_Method_ID = 1,
                    Event_ID = eventId,
                    Notification_Sent = DateTime.UtcNow,
                    Notification_Status_ID = (int)NotificationStatus.NotDelivered,
                    Subscription_ID = subscriptionId
                };

                notificationId = (long)db.Insert("Notification", "Id", notification);

                RetryNotificationByHttpPostCommand command = new RetryNotificationByHttpPostCommand() { NotificationId = notificationId };

                handler.Handle(command);

                var repo = container.GetInstance<INotificationRepository>();
                var storage = container.GetInstance<IUnitOfWorkStorage>();

                using (var uow = storage.NewUnitOfWork())
                {
                    var actual = repo.Get(notificationId);

                    Assert.AreEqual(actual.Status, NotificationStatus.NotDelivered, "Status should be notdelivered");

                    var attempt = actual.DeliveryAttempts.OrderByDescending(i => i.Id).FirstOrDefault();

                    Assert.IsNotNull(attempt, "There should be record exist for delivery attempt");

                    Assert.IsFalse(attempt.Successful, "This should be failure delivery");
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

        [TestMethod]
        [TestCategory("Integration")]
       // [Microsoft.VisualStudio.TestTools.UnitTesting.Ignore]
        public void RetryNotificationByHttpPostCommandHandler_Handle_FaliureDelivery_72hours()
        {
            var db = new Database("NotDataBase");

            string endPoint = ConfigurationManager.AppSettings["ReqApiUrl"].ToString() + "NotFoundRequest";

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<ICommandHandler<RetryNotificationByHttpPostCommand>>();

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

                var notification = new NotificationPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.UtcNow,
                    Delivery_Address = endPoint,
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
                    Delivery_Timestamp = DateTime.UtcNow.AddHours(-(CommonConsts.NotificationExpireLimit + 1)),
                    Delivery_Failure_Reason = "Test",
                    Delivery_Http_Response_Code = 404,
                    Notification_ID = notificationId,
                    Delivery_Successful = false
                };

                attemptId = (long)db.Insert("Notification_Delivery_Attempt", "Id", deliveryAttempt);

                RetryNotificationByHttpPostCommand command = new RetryNotificationByHttpPostCommand() { NotificationId = notificationId };

                handler.Handle(command);

                var repo = container.GetInstance<INotificationRepository>();
                var storage = container.GetInstance<IUnitOfWorkStorage>();

                using (var uow = storage.NewUnitOfWork())
                {
                    var actual = repo.Get(notificationId);

                    Assert.AreEqual(actual.Status, NotificationStatus.Failed, "Status should be falied");

                    var attempt = actual.DeliveryAttempts.OrderByDescending(i => i.Id).FirstOrDefault();

                    Assert.IsNotNull(attempt, "There should be record exist for delivery attempt");

                    Assert.IsFalse(attempt.Successful, "This should be failure delivery");
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

        [TestMethod]
        [TestCategory("Integration")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Ignore]
        public void RetryNotificationByHttpPostCommandHandler_Handle_RegisterDelivery_72hours()
        {
            var db = new Database("NotDataBase");

            string endPoint = ConfigurationManager.AppSettings["ReqApiUrl"].ToString() + "NotFoundRequest";

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<ICommandHandler<RetryNotificationByHttpPostCommand>>();

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

                var notification = new NotificationPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.UtcNow,
                    Delivery_Address = endPoint,
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
                    Delivery_Timestamp = DateTime.UtcNow.AddHours(-(CommonConsts.NotificationExpireLimit + 1)),
                    Delivery_Failure_Reason = "Test",
                    Delivery_Http_Response_Code = 404,
                    Notification_ID = notificationId,
                    Delivery_Successful = false
                };

                attemptId = (long)db.Insert("Notification_Delivery_Attempt", "Id", deliveryAttempt);

                RetryNotificationByHttpPostCommand command = new RetryNotificationByHttpPostCommand() { NotificationId = notificationId };

                handler.Handle(command);

                var repo = container.GetInstance<INotificationRepository>();
                var storage = container.GetInstance<IUnitOfWorkStorage>();

                using (var uow = storage.NewUnitOfWork())
                {
                    var actual = repo.Get(notificationId);

                    Assert.AreEqual(actual.Status, NotificationStatus.NotDelivered, "Status should be notdelivered");

                    var attempt = actual.DeliveryAttempts.OrderByDescending(i => i.Id).FirstOrDefault();

                    Assert.IsNotNull(attempt, "There should be record exist for delivery attempt");

                    Assert.IsFalse(attempt.Successful, "This should be failure delivery");
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

        [TestMethod]
        [TestCategory("Integration")]
        public void RetryNotificationByHttpPostCommandHandler_Handle_Url_Blank()
        {
            var db = new Database("NotDataBase");

            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);

            var handler =
                container.GetInstance<ICommandHandler<RetryNotificationByHttpPostCommand>>();

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

                eventId = (Guid)db.Insert("Event", "Id",false, eventModel);

                var notification = new NotificationPetaPoco()
                {
                    Created_By = "CreatedBy",
                    Creation_Date = DateTime.UtcNow,
                    Delivery_Address = "",
                    Delivery_Method_ID = 1,
                    Event_ID = eventId,
                    Notification_Sent = DateTime.UtcNow,
                    Notification_Status_ID = (int)NotificationStatus.NotDelivered,
                    Subscription_ID = subscriptionId
                };

                notificationId = (long)db.Insert("Notification", "Id", notification);

                RetryNotificationByHttpPostCommand command = new RetryNotificationByHttpPostCommand() { NotificationId = notificationId };

                handler.Handle(command);

                var repo = container.GetInstance<INotificationRepository>();
                var storage = container.GetInstance<IUnitOfWorkStorage>();

                using (var uow = storage.NewUnitOfWork())
                {
                    var actual = repo.Get(notificationId);

                    Assert.AreEqual(actual.Status, NotificationStatus.Failed, "Status should be failed");

                    var attempt = actual.DeliveryAttempts.OrderByDescending(i => i.Id).FirstOrDefault();

                    Assert.IsNotNull(attempt, "There should be record exist for delivery attempt");

                    Assert.IsFalse(attempt.Successful, "This should be failure delivery");
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
