using Example.Common.Context.Bus;
using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Logging;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Commands
{
    public class SendNotificationRetiresScheduledTaskCommandHandler : ICommandHandler<SendNotificationRetiresScheduledTaskCommand>
    {
        private static ILog _logger = LogManager.GetLogger(typeof(SendNotificationRetiresScheduledTaskCommandHandler));

        private readonly IUnitOfWorkStorage _storage;

        private readonly INotificationRepository _notificationRepository;

        internal IBusAdaptor _adapter;

        public SendNotificationRetiresScheduledTaskCommandHandler(IUnitOfWorkStorage storage, INotificationRepository notificationRepository, IBusAdaptor busAdaptor)
        {
            #region check for nulls

            if (storage == null) throw new ArgumentNullException("Unit of work storage cannot be passed null");

            if (notificationRepository == null) throw new ArgumentNullException("Notification repository cannot be passed null");

            if (busAdaptor == null) throw new ArgumentNullException("Bus adapter cannot be passed null");

            #endregion

            _storage = storage;

            _notificationRepository = notificationRepository;

            _adapter = busAdaptor;

        }

        public void Handle(SendNotificationRetiresScheduledTaskCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("SendNotificationRetiresScheduledTaskCommand command cannot be passed null");

            using (_logger.Push())
            {
                _logger.Info("Handling command: SendNotificationRetiresScheduledTaskCommand", command);

                int notificationCount = 0, errors = 0;

                using (var uow = _storage.NewUnitOfWork())
                {
                    var notifications = _notificationRepository.GetNotificationsToRetry();

                    notificationCount = notifications.Count();

                    foreach (var item in notifications)
                    {
                        if(item.DeliveryAttempts !=null && item.DeliveryAttempts.Count() > 0)
                        {
                            RetryNotificationByHttpPostCommand retryNotification = new RetryNotificationByHttpPostCommand() { NotificationId = item.Id };

                            try
                            {
                                _adapter.Send(retryNotification);
                            }
                            catch (Exception ex)
                            {
                                errors++;
                                _logger.Error("Failed to send RetryNotificationByHttpPostCommand command for notification id : " + item.Id + "", command, ex);
                            }
                        }
                    }
                }
            }
        }
    }
}
