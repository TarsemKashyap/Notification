using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Notific.Context.Domain.Model;
using FluentNHibernate.Mapping;
using Example.Notific.Context.Common;

namespace Example.Notific.Context.Domain.Mapping
{
    public class SubscriptionMap : ClassMap<Subscription>
    {
        public SubscriptionMap()
        {
            Table("Subscription");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.MerchantId).Column("Merchant_ID").Not.Nullable();

            Map(x => x.EventType).Column("Event_Type_ID").CustomType(typeof(EventType)).Not.Nullable();

            Map(x => x.DeliveryMethod).Column("Delivery_Method_ID").CustomType(typeof(DeliveryMethod)).Not.Nullable();

            Map(x => x.DeliveryAddress).Column("Delivery_Address").Length(4000).Not.Nullable();

            Map(x => x.Description).Column("Description").Length(255).Not.Nullable();

            Map(x => x.SubscribedBy).Column("Subscribed_By").Length(255).Not.Nullable();

            Map(x => x.SubscriptionDate).Column("Subscription_Date").Not.Nullable();

            Map(x => x.SubscriptionTerminated).Column("Subscription_Terminated").Not.Nullable();

            Map(x => x.TerminationDate).Column("Termination_Date");

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
