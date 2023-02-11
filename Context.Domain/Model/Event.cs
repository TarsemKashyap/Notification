using Example.Common.Context.DDD.Domain;
using Example.Notific.Context.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Model
{
    public class Event : EntityWithTypedId<Guid>
    {
        #region Properties        
              

        public virtual EventType Type { get; protected set; }      

        public virtual DateTime Received { get; protected set; }

        public virtual int MerchantId { get; protected set; }

        public virtual string Secret { get; protected set; }

        public virtual ContentType ContentType { get; protected set; }

        public virtual string Content { get; protected set; }

        #region Audit

        public virtual string CreatedBy { get; protected set; }

        public virtual DateTime CreatedDate { get; protected set; }

        #endregion

        #endregion

        public Event()
        {

        }

        public Event(EventType eventType, DateTime received, int merchantId, string content, string createdBy, ContentType contentType,string secret)
        {
            MerchantId = merchantId;
            Type = eventType;
            CreatedBy = createdBy;
            CreatedDate = DateTime.UtcNow;
            Received = received;
            Content = content;
            ContentType = contentType;
            Secret = secret;           
        }
    }
}
