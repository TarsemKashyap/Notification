using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "metaDataItemResource")]
    [XmlRoot("metaDataItemResource")]
    public class MetaDataItemResource
    {
        [DataMember(Name = "key")]
        [XmlElement("key")]
        public string Key { get; set; }

        [DataMember(Name = "value")]
        [XmlElement("value")]
        public string Value { get; set; }
    }
}
