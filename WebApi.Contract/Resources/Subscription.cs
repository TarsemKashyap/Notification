using Example.Common.WebAPI.Contract.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Example.Notific.WebApi.Contract.Resources
{
    [DataContract(Name = "subscription", Namespace = "")]
    [XmlRoot("subscription", Namespace = "")]
    public class Subscription : BaseResource
    {
        [DataMember(Name = "merchantId", Order = 1)]
        [XmlElement("merchantId", Order = 1)]
        public int MerchantId { get; set; }

        [DataMember(Name = "eventType", Order = 2)]
        [XmlElement("eventType", Order = 2)]
        public int EventType { get; set; }

        [DataMember(Name = "delivery", Order = 3)]
        [XmlElement("delivery", Order = 3)]
        public DeliveryResource Delivery { get; set; }

        [DataMember(Name = "description", Order = 4)]
        [XmlElement("description", Order = 4)]
        public string Description { get; set; }

        [DataMember(Name = "dateSubscribedUtc", Order = 5)]
        [XmlElement("dateSubscribedUtc", Order = 5)]
        public string DateSubscribedUtc { get; set; }

        [DataMember(Name = "subscriber", Order = 6)]
        [XmlElement("subscriber", Order = 6)]
        public string Subscriber { get; set; }
       
    }

    [DataContract(Name = "delivery", Namespace = "")]
    [XmlRoot("delivery", Namespace = "")]
    public class DeliveryResource
    {
        [DataMember(Name = "method", Order = 1)]
        [XmlElement("method", Order = 1)]
        public string Method { get; set; }

        [DataMember(Name = "address", Order = 2)]
        [XmlElement("address", Order = 2)]
        public string Address { get; set; }
    }
}
