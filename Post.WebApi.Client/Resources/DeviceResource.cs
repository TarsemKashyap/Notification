using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "device")]
    [XmlRoot("device")]
    public class DeviceResource
    {
        [DataMember(Name = "id")]
        [XmlElement("id")]
        public string Id { get; set; }

        [DataMember(Name = "description")]
        [XmlElement("description")]
        public string Description { get; set; }
    }
}
