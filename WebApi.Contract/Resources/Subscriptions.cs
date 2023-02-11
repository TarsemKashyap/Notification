using Example.Common.WebAPI.Contract.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Example.Notific.WebApi.Contract.Resources
{
    [DataContract(Name = "subscriptions", Namespace = "")]
    [XmlRoot("subscriptions", Namespace = "")]
    public class Subscriptions
    {
        [DataMember(Name = "subscription", Order = 1)]
        [XmlElement("subscription", Order = 1)]
        public Subscription Subscription { get; set; }
    }
}
