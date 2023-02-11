using Example.Common.WebAPI.Contract.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Example.Notific.WebApi.Contract.Resources
{
    public class MerchantConfiguration : BaseResource
    {
        [DataMember(Name = "id", Order = 1)]
        [XmlElement("id", Order = 1)]
        public long Id { get; set; }

        [DataMember(Name = "merchantId", Order = 2)]
        [XmlElement("merchantId", Order = 2)]
        public int MerchantId { get; set; }

        [DataMember(Name = "secret", Order = 3)]
        [XmlElement("secret", Order = 3)]
        public string Secret { get; set; }
    }

    public class Configuration
    {
        [DataMember(Name = "id", Order = 1)]
        [XmlElement("id", Order = 1)]
        public long Id { get; set; }

        [DataMember(Name = "merchantId", Order = 2)]
        [XmlElement("merchantId", Order = 2)]
        public int MerchantId { get; set; }

        [DataMember(Name = "secret", Order = 3)]
        [XmlElement("secret", Order = 3)]
        public string Secret { get; set; }
    }
}
