using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Common.Context.DDD.Persistence;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Common;

namespace Example.Notific.Context.Domain.Repositories.Interfaces
{
    public interface ISubscriptionRepository : IRepositoryWithTypedId<Subscription, long>
    {
        IList<Subscription> GetByMerchant(int merchantId, bool includeTerminated);

        IList<Subscription> GetActiveForMerchantAndEventType(int merchantId, EventType eventType);
    }

}
