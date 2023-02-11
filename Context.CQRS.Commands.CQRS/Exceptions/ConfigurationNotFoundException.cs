using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Exceptions
{
    public class ConfigurationNotFoundException : Exception
    {
        #region Ctors

        public ConfigurationNotFoundException(int merchantId)
            : base(string.Format("Merchant configuration not found. Id: {0}", merchantId))
        {
        }

        #endregion
    }
}
