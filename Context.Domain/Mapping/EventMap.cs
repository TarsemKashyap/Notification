using Example.Notific.Context.Common;
using Example.Notific.Context.Domain.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Mapping
{
    public class EventMap : ClassMap<Event>
    {
        public EventMap()
        {
            Table("Event");          

            Id(x => x.Id).Column("Id").GeneratedBy.GuidComb();

            Map(x => x.Type).Column("Event_Type_ID").CustomType(typeof(EventType)).Not.Nullable();

            Map(x => x.ContentType).Column("Content_Type_ID").CustomType(typeof(ContentType)).Not.Nullable();

            Map(x => x.Secret).Column("Event_Secret").Not.Nullable();

            Map(x => x.Received).Column("Event_Received").Not.Nullable();

            Map(x => x.MerchantId).Column("Merchant_ID").Not.Nullable();

            Map(x => x.Content).Column("Event_Content").Not.Nullable().CustomType("StringClob").CustomSqlType("nvarchar(max)"); ;

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
