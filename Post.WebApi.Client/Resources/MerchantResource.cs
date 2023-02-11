using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "merchant")]
    [XmlRoot("merchant")]
    public class MerchantResource
    {
        [DataMember(Name = "id")]
        [XmlElement("id")]
        public int? Id { get; set; }

        [DataMember(Name = "subAccount")]
        [XmlElement("subAccount")]
        public int? SubAccount { get; set; }
    }
}
