using Example.Common.WebAPI.Contract.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Example.Notific.WebApi.Contract.Resources
{
    [DataContract(Name = "event", Namespace = "")]
    [XmlRoot("event", Namespace = "")]
    public class Event : BaseResource
    {
        [DataMember(Name = "id", Order = 1)]
        [XmlElement("id", Order = 1)]
        public string Id { get; set; }       

        [DataMember(Name = "merchantId", Order = 2)]
        [XmlElement("merchantId", Order = 2)]
        public int MerchantId { get; set; }

        [DataMember(Name = "received", Order = 3)]
        [XmlElement("received", Order = 3)]
        public string Received { get; set; }

        [DataMember(Name = "type", Order = 4)]
        [XmlElement("type", Order = 4)]
        public int Type { get; set; }

        [DataMember(Name = "content", Order = 5)]
        [XmlElement("content", Order = 5)]
        public string Content { get; set; }
    }
}
