using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Context.Infrastructure.NHibernate.Repositories;
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
    public class MerchantConfigRepository : NHRepositoryWithTypedId<MerchantConfig, long>, IMerchantConfigRepository
    {
        #region Ctors

        public MerchantConfigRepository(IUnitOfWorkStorage storage)
            : base(storage)
        {
        }



        #endregion

        public MerchantConfig GetByMerchant(int merchantId)
        {
            if (!_storage.IsStarted)
            {
                using (var uow = _storage.NewUnitOfWork())
                {
                    return ConfigurationDetails(merchantId);
                }
            }
            else
            {
                return ConfigurationDetails(merchantId);
            }
        }

        private MerchantConfig ConfigurationDetails(int merchantId)
        {
            using (var tran = _storage.Current.BeginTransaction())
            {
                ISession nhSession = (ISession)_storage.Current.GetDBSession();
                var result = nhSession.CreateCriteria<MerchantConfig>().Add(Restrictions.Eq("MerchantId", merchantId)).UniqueResult<MerchantConfig>();
                return result;
            }
        }
    }
}
