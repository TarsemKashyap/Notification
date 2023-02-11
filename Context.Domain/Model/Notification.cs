using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Common.Context.DDD.Domain;
using Example.Notific.Context.Common;

namespace Example.Notific.Context.Domain.Model
{
    public class Notification : EntityWithTypedId<long>
    {
        #region Properties       

        public virtual Event Event { get; protected set; }

        public virtual Subscription GeneratedBy { get; protected set; }

        public virtual DeliveryMethod DeliveryMethod { get; protected set; }

        public virtual string DeliveryAddress { get; protected set; }

        public virtual DateTime Sent { get; protected set; }

        public virtual NotificationStatus Status { get; protected set; }

        public virtual string CommsTrackingId { get; protected set; }

        public virtual IList<DeliveryAttempt> DeliveryAttempts { get; protected set; }

        #region Audit

        public virtual string CreatedBy { get; protected set; }

        public virtual DateTime CreatedDate { get; protected set; }

        public virtual string LastModifiedBy { get; protected set; }

        public virtual DateTime? LastModifiedDate { get; protected set; }

        #endregion

        #endregion

        public Notification(Event eventId, Subscription subsriptionId, DeliveryMethod deliveryMethod, string deliveryAddress, DateTime sent, NotificationStatus status, string commsTrackingId, string createdBy)
        {
            Event = eventId;
            GeneratedBy = subsriptionId;
            DeliveryMethod = deliveryMethod;
            DeliveryAddress = deliveryAddress;
            Sent = sent;
            Status = status;
            CommsTrackingId = commsTrackingId;
            CreatedBy = createdBy;
            CreatedDate = DateTime.UtcNow;
        }

        public Notification()
        {

        }

        /// <summary>
        /// Insert the delivery attempts
        /// </summary>
        /// <param name="attempt">Delivery attempt model</param>
        public virtual void RegisterDeliveryAttempt(DeliveryAttempt attempt)
        {
            if (attempt.Successful)
            {
                Sent = attempt.Timestamp;
                Status = NotificationStatus.Delivered;
            }
            else
            {
                if (!CheckNotificationExpired() || CheckStatusCodeNotificationFailure(attempt.HttpResponseCode))
                {
                    Status = NotificationStatus.Failed;
                }
            }

            if (DeliveryAttempts != null)
            {
                DeliveryAttempts.Add(attempt);
            }
            else
            {
                DeliveryAttempts = new List<DeliveryAttempt> { attempt };
            }

        }

        private static bool CheckStatusCodeNotificationFailure(int httpResponseCode)
        {
            return httpResponseCode == -1;
        }

        private bool CheckNotificationExpired()
        {
            var status = true;

            if (DeliveryAttempts != null)
            {
                var firstDeliveryAttempt = DeliveryAttempts.OrderBy(i=>i.Timestamp).FirstOrDefault();

                if (firstDeliveryAttempt != null)
                {
                    if (firstDeliveryAttempt.Timestamp.AddHours(CommonConsts.NotificationExpireLimit) <= DateTime.UtcNow)
                    {
                        status = false;
                    }
                }
            }

            return status;
        }          
    }
}

