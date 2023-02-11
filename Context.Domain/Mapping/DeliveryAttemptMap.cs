using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Notific.Context.Domain.Model;
using FluentNHibernate.Mapping;

namespace Example.Notific.Context.Domain.Mapping
{
    public class DeliveryAttemptMap : ClassMap<DeliveryAttempt>
    {
        public DeliveryAttemptMap()
        {
            Table("Notification_Delivery_Attempt");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.NotificationId).Column("Notification_ID").Not.Nullable();

            Map(x => x.Timestamp).Column("Delivery_Timestamp").Not.Nullable();

            Map(x => x.Successful).Column("Delivery_Successful").Not.Nullable();

            Map(x => x.HttpResponseCode).Column("Delivery_Http_Response_Code").Not.Nullable();

            Map(x => x.FailureReason).Column("Delivery_Failure_Reason");

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
