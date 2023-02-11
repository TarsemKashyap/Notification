using Example.Common.Context.DDD.Persistence;
using Example.Notific.Context.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Repositories.Interfaces
{
    public interface IMerchantConfigRepository : IRepositoryWithTypedId<MerchantConfig, long>
    {
        /// <summary>
        /// Get merchant configuration details
        /// </summary>
        /// <param name="merchantId">Merchant Id</param>
        /// <returns>Merchant config model</returns>
        MerchantConfig GetByMerchant(int merchantId);
    }
}
