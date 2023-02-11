using Example.Common.Context.DDD.Domain;
using Example.Notific.Context.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Model
{
    public class MerchantConfigHistory : EntityWithTypedId<long>
    {
        public virtual int MerchantId { get; protected set; }

        public virtual string Secret { get; protected set; }

        public virtual MerchantConfig MerchantConfig { get; set; }

        #region Audit

        public virtual string CreatedBy { get; protected set; }

        public virtual DateTime CreatedDate { get; protected set; }
        public virtual int VerificationMethod { get; protected set; }

        #endregion

        public MerchantConfigHistory()
        {

        }

        public MerchantConfigHistory(int merchantId, string sceret, string createdBy, int verificationMethod)
        {
            this.MerchantId = merchantId;
            this.Secret = sceret;
            this.CreatedBy = createdBy;
            this.CreatedDate = DateTime.UtcNow;
            this.VerificationMethod = verificationMethod;
        }
    }
}
