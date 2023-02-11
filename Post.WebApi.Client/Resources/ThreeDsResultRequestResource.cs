using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "threeDsResultRequestResource")]
    [XmlRoot("threeDsResultRequestResource")]
    public class ThreeDsResultRequestResource
    {
        [DataMember(Name = "ipAddress")]
        [XmlElement("ipAddress")]
        public string IpAddress { get; set; }

        [DataMember(Name = "callerReference")]
        [XmlElement("callerReference")]
        public string CallerReference { get; set; }

        [DataMember(Name = "pares")]
        [XmlElement("pares")]
        public string Pares { get; set; }

        //[DataMember(Name = "merchant")]
        //[XmlElement("merchant")]
        //public MerchantResource Merchant { get; set; }

    }
}
