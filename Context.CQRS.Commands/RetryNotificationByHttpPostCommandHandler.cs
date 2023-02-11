using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Logging;
using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Domain.Infrastructure;
using Example.Notific.Context.Domain.Infrastructure.Interfaces;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Example.Notific.Context.CQRS.Commands
{
    public class RetryNotificationByHttpPostCommandHandler : ICommandHandler<RetryNotificationByHttpPostCommand>
    {

        #region Properties and local variables

        ILog _logger = LogManager.GetLogger(typeof(RetryNotificationByHttpPostCommandHandler));

        internal IUnitOfWorkStorage _storage;
        internal INotificationRepository _notificationRepository;
        internal IHttpPoster _httpPoster;
        internal INotificationJsonGenerator _notificationJsonGenerator;
        #endregion

        #region Ctors

        public RetryNotificationByHttpPostCommandHandler(
            IUnitOfWorkStorage storage,
           INotificationRepository notificationRepository, IHttpPoster httpPoster, INotificationJsonGenerator notificationJsonGenerator)
        {
            #region check for nulls

            if (storage == null) throw new ArgumentNullException("Unit of work storage cannot be passed null");

            if (notificationRepository == null) throw new ArgumentNullException("Notification repository cannot be passed null");

            if (httpPoster == null) throw new ArgumentNullException("Httppost service cannot be passed null");

            if (notificationJsonGenerator == null) throw new ArgumentNullException("Notification json generator service cannot be passed null");

            #endregion

            _storage = storage;

            _notificationRepository = notificationRepository;

            _httpPoster = httpPoster;

            _notificationJsonGenerator = notificationJsonGenerator;
        }

        #endregion

        public void Handle(RetryNotificationByHttpPostCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("RetryNotificationByHttpPostCommand command cannot be passed null");

            using (_logger.Push())
            {
                _logger.Info("Handling command: RetryNotificationByHttpPostCommand", command);

                using (var uow = _storage.NewUnitOfWork())
                {
                    Notification notification = _notificationRepository.Get(command.NotificationId);

                    _logger.Debug("Checking notification existence. Id: " + command.NotificationId);
                    CheckNotificationExistence(notification, command.NotificationId);

                    _logger.Debug("Checking notification status. Id: " + command.NotificationId);
                    CheckNotificationStatus(notification);

                    HttpPostResult result = null;

                    string jsonString = _notificationJsonGenerator.Generate(notification.Event);

                    try
                    {
                        result = _httpPoster.Post(notification.DeliveryAddress, jsonString);
                    }
                    catch (ArgumentNullException ex)
                    {
                        result = new HttpPostResult();
                        result.HttpStatusCode = -1;
                        result.Message = "Url or data passed NULL or blank";
                        _logger.Warn("NULL exception in post data. Id: ", new { Url = notification.DeliveryAddress, Data = jsonString });
                    }

                    _logger.Info("Http post result", result);

                    var attemptModel = new DeliveryAttempt(notification.Id, DateTime.UtcNow, result.Message, "RetryNotificationByHttpPostCommandHandler", result.HttpStatusCode);

                    notification.RegisterDeliveryAttempt(attemptModel);

                    using (var tran = uow.BeginTransaction())
                    {
                        _notificationRepository.Update(notification);

                        tran.Commit();
                    }
                }

                _logger.Info("Command execution complete : RetryNotificationByHttpPostCommand", command);
            }
        }

        private void CheckNotificationExistence(Notification notification, long notificationId)
        {
            using (_logger.Push())
            {
                if (notification == null)
                {
                    _logger.Warn("Notification not found. Id: " + notificationId);
                    throw new NotificationNotFoundException(notificationId);
                }
            }
        }

        private void CheckNotificationStatus(Notification notification)
        {
            using (_logger.Push())
            {
                if (notification.Status != NotificationStatus.NotDelivered)
                {
                    _logger.Warn("Notification not in correct status to be retried. Status: " + notification.Status.ToString());
                    throw new InvalidOperationException("Notification not in correct status to be retried. Status: " + notification.Status.ToString());
                }
            }
        }
    }
}
