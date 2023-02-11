using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Exceptions
{
    public class NotificationNotFoundException : Exception
    {
        #region Ctors

        public NotificationNotFoundException(long notificationId)
            : base(string.Format("Notification not found. Id: {0}", notificationId))
        {
        }

        #endregion
    }
}
