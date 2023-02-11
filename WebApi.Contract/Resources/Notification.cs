using Example.Common.WebAPI.Contract.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Example.Notific.WebApi.Contract.Resources
{
    [DataContract(Name = "notification", Namespace = "")]
    [XmlRoot("notification", Namespace = "")]
    public class Notification : BaseResource
    {
        [DataMember(Name = "id", Order = 1)]
        [XmlElement("id", Order = 1)]
        public int Id { get; set; }

        [DataMember(Name = "status", Order = 2)]
        [XmlElement("status", Order = 2)]
        public int MerchantId { get; set; }

        [DataMember(Name = "event", Order = 3)]
        [XmlElement("event", Order = 3)]
        public Event Event { get; set; }

        [DataMember(Name = "delivery", Order = 4)]
        [XmlElement("delivery", Order = 4)]
        public DeliveryResource delivery { get; set; }

        [DataMember(Name = "deliveryAttempts", Order = 5)]
        [XmlElement("deliveryAttempts", Order = 5)]
        public List<Attempt> DeliveryAttempts { get; set; }
    }

    [DataContract(Name = "attempt", Namespace = "")]
    [XmlRoot("attempt", Namespace = "")]
    public class Attempt
    {
        [DataMember(Name = "timestamp", Order = 1)]
        [XmlElement("timestamp", Order = 1)]
        public DateTime Timestamp { get; set; }

        [DataMember(Name = "successful", Order = 2)]
        [XmlElement("successful", Order = 2)]
        public bool Successful { get; set; }

        [DataMember(Name = "failureReason", Order = 3)]
        [XmlElement("failureReason", Order = 3)]
        public string FailureReason { get; set; }
    }
}
