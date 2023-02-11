using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Context.Infrastructure.NHibernate.Repositories;
using Example.Notific.Context.Common;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Repositories
{
    public class SubscriptionRepository : NHRepositoryWithTypedId<Subscription, long>, ISubscriptionRepository
    {
        #region Ctors

        public SubscriptionRepository(IUnitOfWorkStorage storage)
            : base(storage)
        {
        }


        #endregion

        public IList<Subscription> GetByMerchant(int merchantId, bool includeTerminated)
        {
            if (!_storage.IsStarted)
            {
                using (var uow = _storage.NewUnitOfWork())
                {
                    return GetMerchantSubscriptions(merchantId, includeTerminated);
                }
            }
            else
            {
                return GetMerchantSubscriptions(merchantId, includeTerminated);
            }
        }

        public IList<Subscription> GetActiveForMerchantAndEventType(int merchantId, EventType eventType)
        {
            if (!_storage.IsStarted)
            {
                using (var uow = _storage.NewUnitOfWork())
                {
                    return GetMerchantActiveSubscriptionsEvents(merchantId, eventType);
                }
            }
            else
            {
                return GetMerchantActiveSubscriptionsEvents(merchantId, eventType);
            }
        }

        private IList<Subscription> GetMerchantSubscriptions(int merchantId, bool includeTerminated)
        {
            using (var tran = _storage.Current.BeginTransaction())
            {
                ISession nhSession = (ISession)_storage.Current.GetDBSession();

                var eqMerchant = Restrictions.Eq("MerchantId", merchantId);
                var eqTerminated = Restrictions.Eq("SubscriptionTerminated", false);

                IList<Subscription> result = null;

                if (includeTerminated)
                    result = nhSession.CreateCriteria<Subscription>().Add(eqMerchant).List<Subscription>();
                else
                    result = nhSession.CreateCriteria<Subscription>().Add(eqMerchant).Add(eqTerminated).List<Subscription>();
                return result;
            }
        }

        private IList<Subscription> GetMerchantActiveSubscriptionsEvents(int merchantId, EventType eventType)
        {
            using (var tran = _storage.Current.BeginTransaction())
            {
                ISession nhSession = (ISession)_storage.Current.GetDBSession();

                var eqMerchant = Restrictions.Eq("MerchantId", merchantId);
                var eqEvent = Restrictions.Eq("EventType", eventType);
                var eqTerminated = Restrictions.Eq("SubscriptionTerminated", false);

                IList<Subscription> result = null;

                result = nhSession.CreateCriteria<Subscription>().Add(eqMerchant).Add(eqEvent).Add(eqTerminated).List<Subscription>();

                return result;
            }
        }
    }
}
