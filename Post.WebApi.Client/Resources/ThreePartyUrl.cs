using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "threePartyUrl")]
    [XmlRoot("threePartyUrl")]
    public class ThreePartyUrl
    {
        [DataMember(Name = "url")]
        [XmlElement("url", IsNullable = false)]
        public string Url { get; set; }
    }
}
