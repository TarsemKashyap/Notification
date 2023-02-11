using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Exceptions
{
    public class SubscriptionNotActiveException : Exception
    {
        #region Ctors

        public SubscriptionNotActiveException(long subscriptionId)
            : base(string.Format("Subscription not active. Id: {0}", subscriptionId))
        {
        }

        #endregion
    }
}
