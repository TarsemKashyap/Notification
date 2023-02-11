using Example.Notific.Context.Domain.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Mapping
{
    public class MerchantConfigHistoryMap : ClassMap<MerchantConfigHistory>
    {
        public MerchantConfigHistoryMap()
        {
            Table("Merchant_Configuration_History");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.MerchantId).Column("Merchant_ID").Not.Nullable();

            Map(x => x.Secret).Column("Secret").Not.Nullable();

            References(x => x.MerchantConfig).Column("Merchant_Configuration_ID");

            #region Audit

            Map(t => t.CreatedBy)
                .Column("Created_By")
                .Not.Nullable();

            Map(t => t.CreatedDate)
                .Column("Creation_Date")
                .Not.Nullable();          

            #endregion
        }
    }
}
