using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Logging;
using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.Context.CQRS.Queries.DataMapper;
using Example.Notific.Context.Domain.Infrastructure.Interfaces;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Queries.Presentation
{
    public class RetrieveNotificationQueryHandler : IQueryHandler<RetrieveNotificationQuery, object>
    {
        #region Properties and local variables

        private static ILog _logger = LogManager.GetLogger(typeof(RetrieveNotificationQueryHandler));

        internal IUnitOfWorkStorage _storage;

        internal IEventRepository _eventRepository;

        internal INotificationJsonGenerator _notificationJsonGenerator;

        internal IMerchantConfigRepository _merchantConfigRepository;

        #endregion

        #region Ctors

        public RetrieveNotificationQueryHandler(IUnitOfWorkStorage storage, IEventRepository eventRepository, INotificationJsonGenerator notificationJsonGenerator, IMerchantConfigRepository merchantConfigRepository)
        {
            if (storage == null) throw new ArgumentNullException("storage", "UnitOfWorkStorage cannot be passed null");

            if (eventRepository == null) throw new ArgumentNullException("Event repository cannot be passed null");

            if (notificationJsonGenerator == null) throw new ArgumentNullException("Notification json generator service cannot be passed null");

            if (merchantConfigRepository == null) throw new ArgumentNullException("Merchant config repository cannot be passed null");

            _storage = storage;

            _eventRepository = eventRepository;

            _notificationJsonGenerator = notificationJsonGenerator;

            _merchantConfigRepository = merchantConfigRepository;
        }

        #endregion

        public object Handle(RetrieveNotificationQuery query)
        {
            if (query == null)
                throw new ArgumentNullException("Query cannot be passed null");

            using (_logger.Push())
            {
                _logger.Info("Handling query : RetrieveNotificationQuery", query);

                try
                {
                    using (var uow = _storage.NewUnitOfWork())
                    {
                        using (var txn = uow.BeginTransaction())
                        {
                            Event eventDetails = _eventRepository.Get(query.EventId);

                            _logger.Debug("Event details", eventDetails);

                            CheckEventExistence(eventDetails, query.EventId);

                            CheckEventBelongToMerchant(eventDetails, query.MerchantId);

                            _logger.Info("Checking merchant configuration. MerchantId" + query.MerchantId);

                            VerificationMethod verificationMethod = VerificationMethod.SecretOnly;

                            verificationMethod = CheckMerchantVerificationMethod(query.MerchantId);

                            var eventDto = DomainEventMapper.ToNotificationDto(eventDetails, verificationMethod);

                            _logger.Info("Query executed: RetrieveNotificationQuery", eventDto);

                            return DomainEventMapper.ToNotificationDto(eventDetails, verificationMethod);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _logger.Warn("Exception occured: RetrieveNotificationQuery", ex);
                    // rethrow exception from context for timeout or deadlock
                    if (ex.Number == -2 || ex.Number == 1205)
                        throw new QueryTimeoutException(String.Format("Failed to retreive event details. Id: {0}", query.EventId));
                    else
                        throw;
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

        private void CheckEventBelongToMerchant(Event eventDetails, int merchantId)
        {
            using (_logger.Push())
            {
                if (merchantId > 0)
                {
                    if (merchantId != eventDetails.MerchantId)
                    {
                        _logger.Warn("Event not belong to merchant. Id: " + eventDetails.Id);
                        throw new EventNotBelongException(eventDetails.Id);
                    }
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
