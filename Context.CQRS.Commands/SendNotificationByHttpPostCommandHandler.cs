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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Example.Notific.Context.CQRS.Commands
{
    public class SendNotificationByHttpPostCommandHandler : ICommandHandler<SendNotificationByHttpPostCommand>
    {
        #region Properties and local variables

        ILog _logger = LogManager.GetLogger(typeof(SendNotificationByHttpPostCommandHandler));

        internal IUnitOfWorkStorage _storage;

        internal ISubscriptionRepository _subscriptionRepository;
        internal IEventRepository _eventRepository;
        internal INotificationRepository _notificationRepository;
        internal IHttpPoster _httpPoster;
        internal INotificationJsonGenerator _notificationJsonGenerator;
        internal IMerchantConfigRepository _merchantConfigRepository;
        internal INotificationSigGenerator _notificationSigGenerator;
        #endregion

        #region Ctors

        public SendNotificationByHttpPostCommandHandler(
            IUnitOfWorkStorage storage,
            ISubscriptionRepository subscriptionRepository, IEventRepository eventRepository, INotificationRepository notificationRepository, IHttpPoster httpPoster, INotificationJsonGenerator notificationJsonGenerator,
            IMerchantConfigRepository merchantConfigRepository, INotificationSigGenerator notificationSigGenerator)
        {
            #region check for nulls

            if (storage == null) throw new ArgumentNullException("Unit of work storage cannot be passed null");

            if (subscriptionRepository == null) throw new ArgumentNullException("Subscription repository cannot be passed null");

            if (eventRepository == null) throw new ArgumentNullException("Event repository cannot be passed null");

            if (notificationRepository == null) throw new ArgumentNullException("Notification repository cannot be passed null");

            if (httpPoster == null) throw new ArgumentNullException("Httppost service cannot be passed null");

            if (notificationJsonGenerator == null) throw new ArgumentNullException("Notification json generator service cannot be passed null");

            if (merchantConfigRepository == null) throw new ArgumentNullException("Merchant respository cannot be passed null");

            if (notificationJsonGenerator == null) throw new ArgumentNullException("Notification sig generator cannot be passed null");
            #endregion

            _storage = storage;

            _subscriptionRepository = subscriptionRepository;

            _eventRepository = eventRepository;

            _notificationRepository = notificationRepository;

            _httpPoster = httpPoster;

            _notificationJsonGenerator = notificationJsonGenerator;

            _merchantConfigRepository = merchantConfigRepository;

            _notificationSigGenerator = notificationSigGenerator;
        }

        #endregion

        public void Handle(SendNotificationByHttpPostCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("SendNotificationByHttpPostCommand command cannot be passed null");

            using (_logger.Push())
            {
                _logger.Info("Handling command: SendNotificationByHttpPostCommand", command);

                using (var uow = _storage.NewUnitOfWork())
                {
                    Event eventModel = _eventRepository.Get(command.EventId);

                    _logger.Debug("Checking event existence. Id: " + command.EventId);
                    CheckEventExistence(eventModel, command.EventId);

                    Subscription subscription = _subscriptionRepository.Get(command.SubscriptionId);

                    _logger.Debug("Checking subscription existence. Id: " + command.SubscriptionId);
                    CheckSubscriptionExistence(subscription, command.SubscriptionId);

                    _logger.Debug("Checking subscription active. Id: " + command.SubscriptionId);
                    CheckSubscriptionActive(subscription, command.SubscriptionId);

                    _logger.Info("Checking merchant configuration. MerchantId" + eventModel.MerchantId);
                    VerificationMethod verificationMethod = CheckMerchantVerificationMethod(eventModel.MerchantId);



                    var notificationModel = new Notification(eventModel, subscription, DeliveryMethod.HttpPost, subscription.DeliveryAddress, DateTime.UtcNow, NotificationStatus.NotDelivered, "", "SendNotificationByHttpPostCommandHandler");

                    using (var tran = uow.BeginTransaction())
                    {
                        _notificationRepository.Save(notificationModel);

                        tran.Commit();
                    }

                    _logger.Debug("Persisting notification", notificationModel);

                    string jsonString = _notificationJsonGenerator.Generate(eventModel, verificationMethod);

                    HttpPostResult result = null;

                    try
                    {
                        if (verificationMethod == VerificationMethod.SecretOnly)
                            result = _httpPoster.Post(subscription.DeliveryAddress, jsonString);
                        else
                        {
                            _logger.Info("Generating notification signature header HMAC-SHA256");

                            string notSig = _notificationSigGenerator.ComputeHMACSHA256(eventModel.Secret, jsonString);

                            _logger.Info("HMAC has been generated x-ExampleCompany-notsig: " + notSig);

                            result = _httpPoster.Post(subscription.DeliveryAddress, jsonString, notSig);
                        }
                    }
                    catch (ArgumentNullException ex)
                    {
                        result = new HttpPostResult();
                        result.HttpStatusCode = -1;
                        result.Message = "Url or data passed NULL or blank";
                        _logger.Warn("NULL exception in post data. Id: ", new { Url = subscription.DeliveryAddress, Data = jsonString });
                    }

                    _logger.Info("Http post result", result);

                    var attemptModel = new DeliveryAttempt(notificationModel.Id, DateTime.UtcNow, result.Message, "SendNotificationByHttpPostCommandHandler", result.HttpStatusCode);

                    notificationModel.RegisterDeliveryAttempt(attemptModel);

                    using (var tran = uow.BeginTransaction())
                    {
                        _notificationRepository.Update(notificationModel);

                        tran.Commit();
                    }
                }

                _logger.Info("Command execution complete : SendNotificationByHttpPostCommand", command);
            }
        }

        private void CheckSubscriptionExistence(Subscription subscription, long subscriptionId)
        {
            using (_logger.Push())
            {
                if (subscription == null)
                {
                    _logger.Warn("Subscription not found. Id: " + subscriptionId);
                    throw new SubscriptionNotFoundException(subscriptionId);
                }
            }
        }

        private void CheckSubscriptionActive(Subscription subscription, long subscriptionId)
        {
            using (_logger.Push())
            {
                if (subscription.SubscriptionTerminated == true)
                {
                    _logger.Warn("Subscription not active. Id: " + subscriptionId);
                    throw new SubscriptionNotActiveException(subscriptionId);
                }
            }
        }

        private void CheckEventExistence(Event eventDetails, Guid eventId)
        {
            using (_logger.Push())
            {
                if (eventDetails == null)
                {
                    _logger.Warn("Event not found. Id: " + eventId);
                    throw new EventNotFoundException(eventId);
                }
            }
        }
        private VerificationMethod CheckMerchantVerificationMethod(int merchantId)
        {
            using (_logger.Push())
            {
                var merchantConfiguration = _merchantConfigRepository.GetByMerchant(merchantId);
                if (merchantConfiguration == null)
                {
                    _logger.Warn("Merchant configuration not found. MerchantId: " + merchantId);
                    throw new ConfigurationNotFoundException(merchantId);
                }
                else
                {
                    _logger.Info("Verification method found. Verfification_Method: " + merchantConfiguration.VerificationMethod);
                    return (VerificationMethod)merchantConfiguration.VerificationMethod;
                }
            }
        }
    }
}
