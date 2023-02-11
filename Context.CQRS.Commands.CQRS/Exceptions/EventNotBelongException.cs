using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Exceptions
{
    public class EventNotBelongException : Exception
    {
        #region Ctors

        public EventNotBelongException(Guid eventId)
            : base(string.Format("Event not belong to merchant. Id: {0}", eventId.ToString()))
        {
        }

        #endregion
    }
}
