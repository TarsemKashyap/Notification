using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Logging;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.Context.CQRS.Queries.DataMapper;
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
    public class RetrieveSubscriptionQueryHandler : IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto>
    {
        #region Properties and local variables

        private static ILog _logger = LogManager.GetLogger(typeof(RetrieveSubscriptionQueryHandler));

        internal IUnitOfWorkStorage _storage;

        internal ISubscriptionRepository _subscriptionRepository;

        #endregion

        #region Ctors

        public RetrieveSubscriptionQueryHandler(IUnitOfWorkStorage storage, ISubscriptionRepository subscriptionRepository)
        {
            if (storage == null) throw new ArgumentNullException("storage", "UnitOfWorkStorage cannot be passed null");

            if (subscriptionRepository == null) throw new ArgumentNullException("Subscription repository cannot be passed null");

            _storage = storage;

            _subscriptionRepository = subscriptionRepository;
        }

        #endregion

        public SubscriptionDto Handle(RetrieveSubscriptionQuery query)
        {
            if (query == null)
                throw new ArgumentNullException("Query cannot be passed null");

            using (_logger.Push())
            {
                _logger.Info("Handling query : RetrieveSubscriptionQuery", query);

                try
                {
                    using (var uow = _storage.NewUnitOfWork())
                    {
                        using (var txn = uow.BeginTransaction())
                        {
                            Subscription subscription = _subscriptionRepository.Get(query.SubscriptionId);                           

                            CheckSubscriptionExistence(subscription, query.SubscriptionId);

                            _logger.Debug("Event subscription details", subscription);

                            var subscriptionDto = DomainSubscriptionMapper.ToSubscriptionDto(subscription);

                            _logger.Info("Query executed: RetrieveSubscriptionQuery", subscriptionDto);

                            return subscriptionDto;
                        }
                    }

                }
                catch (SqlException ex)
                {
                    _logger.Warn("Exception occured: RetrieveSubscriptionQuery", ex);
                    // rethrow exception from context for timeout or deadlock
                    if (ex.Number == -2 || ex.Number == 1205)
                        throw new QueryTimeoutException(String.Format("Failed to retreive event subscription. Id: {0}", query.SubscriptionId));
                    else
                        throw;
                }
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
    }
}
