using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Exceptions
{
    public class SubscriptionNotFoundException : Exception
    {
        #region Ctors

        public SubscriptionNotFoundException(long subscriptionId)
            : base(string.Format("Subscription not found. Id: {0}", subscriptionId))
        {
        }

        #endregion
    }
}
