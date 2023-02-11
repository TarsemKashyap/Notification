using Example.Common.Context.DDD.Domain;
using Example.Notific.Context.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Model
{
    public class Subscription : EntityWithTypedId<long>
    {
        #region Properties       

        public virtual int MerchantId { get; protected set; }

        public virtual EventType EventType { get; protected set; }

        public virtual DeliveryMethod DeliveryMethod { get; protected set; }      

        public virtual string DeliveryAddress { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual string SubscribedBy { get; protected set; }

        public virtual DateTime SubscriptionDate { get; protected set; }

        public virtual bool SubscriptionTerminated { get; protected set; }

        public virtual DateTime? TerminationDate { get; protected set; }

        #region Audit

        public virtual string CreatedBy { get; protected set; }

        public virtual DateTime CreatedDate { get; protected set; }

        public virtual string LastModifiedBy { get; protected set; }

        public virtual DateTime? LastModifiedDate { get; protected set; }

        #endregion

        #endregion
        public Subscription()
        {

        }
        public Subscription(int merchantId, EventType eventType, DeliveryMethod deliveryMethod,string deliveryAddress,string description,string subscribedBy,string createdBy)
        {
            MerchantId = merchantId;
            EventType = eventType;
            DeliveryMethod = deliveryMethod;
            DeliveryAddress = deliveryAddress;
            Description = description;
            SubscribedBy = subscribedBy;
            SubscriptionDate = DateTime.UtcNow;
            SubscriptionTerminated = false;
            CreatedBy = createdBy;
            CreatedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Remove the event subscription
        /// </summary>
        /// <param name="terminatedBy">Terminated by</param>
        public virtual void Terminate(string terminatedBy)
        {
            TerminationDate= DateTime.UtcNow;
            SubscriptionTerminated = true;
            LastModifiedBy = terminatedBy;
            LastModifiedDate= DateTime.UtcNow;
        }

    }
}
