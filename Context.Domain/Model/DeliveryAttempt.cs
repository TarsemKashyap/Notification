using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Common.Context.DDD.Domain;
using Example.Notific.Context.Common;

namespace Example.Notific.Context.Domain.Model
{
    public class DeliveryAttempt : EntityWithTypedId<long>
    {
        #region Properties 

        public virtual long NotificationId { get; protected set; }

        public virtual DateTime Timestamp { get; protected set; }

        public virtual bool Successful { get; protected set; }

        public virtual int HttpResponseCode { get; protected set; }

        public virtual string FailureReason { get; protected set; }

        #region Audit

        public virtual string CreatedBy { get; protected set; }

        public virtual DateTime CreatedDate { get; protected set; }

        #endregion

        #endregion

        public DeliveryAttempt()
        {

        }

        public DeliveryAttempt(long notificationId, DateTime timestamp, string failureReason, string createdBy, int httpResponseCode)
        {
            NotificationId = notificationId;
            Timestamp = timestamp;
            Successful = (httpResponseCode >= 200 && httpResponseCode < 300) ? true : false;
            FailureReason = failureReason;
            CreatedBy = createdBy;
            CreatedDate = DateTime.UtcNow;
            HttpResponseCode = httpResponseCode;
        }
    }
}
