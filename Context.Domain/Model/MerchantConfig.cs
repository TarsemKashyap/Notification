using Example.Common.Context.DDD.Domain;
using Example.Notific.Context.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Model
{
    public class MerchantConfig : EntityWithTypedId<long>
    {
        public virtual int MerchantId { get; protected set; }

        public virtual string Secret { get; protected set; }

        public virtual IList<MerchantConfigHistory> History { get; protected set; }

        #region Audit

        public virtual string CreatedBy { get; protected set; }

        public virtual DateTime CreatedDate { get; protected set; }

        public virtual string LastModifiedBy { get; protected set; }

        public virtual DateTime? LastModifiedDate { get; protected set; }
        public virtual int VerificationMethod { get; protected set; }

        #endregion

        public MerchantConfig()
        {

        }

        public MerchantConfig(int merchantId, string secret, string createdBy, int verificationMethod)
        {
            this.MerchantId = merchantId;
            this.CreatedBy = createdBy;
            this.Secret = secret;
            this.CreatedDate = DateTime.UtcNow;
            this.VerificationMethod = verificationMethod;
        }

        /// <summary>
        /// Change the secret of merchant
        /// </summary>
        public virtual void ChangeSecret(string updatedBy)
        {
            if (this.History == null)
                this.History = new List<MerchantConfigHistory>();

            MerchantConfigHistory history = new MerchantConfigHistory(MerchantId, Secret, updatedBy, VerificationMethod);
            history.MerchantConfig = this;
            this.History.Add(history);

            this.Secret = Guid.NewGuid().ToString();
            this.LastModifiedBy = updatedBy;
            this.LastModifiedDate = DateTime.UtcNow;
        }
    }
}
