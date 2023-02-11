using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Notific.Context.Domain.Model;
using FluentNHibernate.Mapping;
using Example.Notific.Context.Common;

namespace Example.Notific.Context.Domain.Mapping
{
    public class NotificationMap : ClassMap<Notification>
    {
        public NotificationMap()
        {
            Table("Notification");

            Id(x => x.Id).GeneratedBy.Identity();

            References(x => x.Event).Column("Event_ID").Cascade.All().Not.LazyLoad();

            References(x => x.GeneratedBy).Column("Subscription_ID").Cascade.All().Not.LazyLoad();

            Map(x => x.DeliveryMethod).Column("Delivery_Method_ID").CustomType(typeof(DeliveryMethod)).Not.Nullable();

            Map(x => x.DeliveryAddress).Column("Delivery_Address").Length(4000).Not.Nullable();

            Map(x => x.Sent).Column("Notification_Sent").Not.Nullable();

            Map(x => x.Status).Column("Notification_Status_ID").CustomType(typeof(NotificationStatus)).Not.Nullable();

            Map(x => x.CommsTrackingId).Column("Comms_Tracking_ID").Length(50);

            HasMany<DeliveryAttempt>(x => x.DeliveryAttempts)
              .KeyColumn("Notification_ID")
              .Inverse()
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
