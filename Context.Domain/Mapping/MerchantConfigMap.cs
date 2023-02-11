using Example.Notific.Context.Domain.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Mapping
{
    public class MerchantConfigMap : ClassMap<MerchantConfig>
    {
        public MerchantConfigMap()
        {
            Table("Merchant_Configuration");

            Id(x => x.Id).GeneratedBy.Identity();
        
            Map(x => x.MerchantId).Column("Merchant_ID").Not.Nullable();

            Map(x => x.Secret).Column("Secret").Not.Nullable();

            Map(x => x.VerificationMethod).Column("Verification_Method").Not.Nullable();

            HasMany<MerchantConfigHistory>(x => x.History)
              .KeyColumn("Merchant_Configuration_ID")             
              .Not.LazyLoad()
              .Cascade.All();

            #region Audit

            Map(t => t.CreatedBy)
                .Column("Created_By")
                .Not.Nullable();

            Map(t => t.CreatedDate)
                .Column("Creation_Date")
                .Not.Nullable();

            Map(t => t.LastModifiedBy)
                .Column("Last_Modified_By");

            Map(t => t.LastModifiedDate)
                .Column("Last_Modified_Date");

            #endregion
        }
    }
}
