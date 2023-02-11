using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Logging;
using Example.Notific.Context.Contract.CQRS.Dtos;
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
    public class RetrieveMerchantSubscriptionsQueryHandler : IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]>
    {
        #region Properties and local variables

        private static ILog _logger = LogManager.GetLogger(typeof(RetrieveMerchantSubscriptionsQueryHandler));

        readonly IUnitOfWorkStorage _storage;

        internal ISubscriptionRepository _subscriptionRepository;

        #endregion

        #region Ctors

        public RetrieveMerchantSubscriptionsQueryHandler(IUnitOfWorkStorage storage, ISubscriptionRepository subscriptionRepository)
        {
            if (storage == null) throw new ArgumentNullException("storage", "UnitOfWorkStorage cannot be passed null");

            if (subscriptionRepository == null) throw new ArgumentNullException("Subscription repository cannot be passed null");

            _storage = storage;

            _subscriptionRepository = subscriptionRepository;
        }

        #endregion

        public SubscriptionDto[] Handle(RetrieveMerchantSubscriptionsQuery query)
        {
            if (query == null)
                throw new ArgumentNullException("Query cannot be passed null");

            using (_logger.Push())
            {
                _logger.Info("Handling query : RetrieveMerchantSubscriptionsQuery", query);

                try
                {
                    using (var uow = _storage.NewUnitOfWork())
                    {
                        using (var txn = uow.BeginTransaction())
                        {
                            IList<Subscription> subscription = _subscriptionRepository.GetByMerchant(query.MerchantId,query.IncludeTerminated);

                            _logger.Debug("Merchant subscriptions details", subscription);

                            var subscriptionDto = DomainSubscriptionMapper.ToSubscriptionDtoArray(subscription);

                            _logger.Info("Query executed: RetrieveMerchantSubscriptionsQuery", subscriptionDto);

                            return subscriptionDto;
                        }
                    }

                }
                catch (SqlException ex)
                {
                    _logger.Warn("Exception occured: RetrieveMerchantSubscriptionsQuery", ex);
                    // rethrow exception from context for timeout or deadlock
                    if (ex.Number == -2 || ex.Number == 1205)
                        throw new QueryTimeoutException(String.Format("Failed to retreive merchant subscription. Id: {0}", query.MerchantId));
                    else
                        throw;
                }
            }
        }
    }
}
