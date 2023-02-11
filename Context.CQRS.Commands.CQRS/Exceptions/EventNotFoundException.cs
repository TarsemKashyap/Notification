using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Exceptions
{
    public class EventNotFoundException : Exception
    {
        #region Ctors

        public EventNotFoundException(Guid eventId)
            : base(string.Format("Event not found. Id: {0}", eventId.ToString()))
        {
        }

        #endregion
    }
}
