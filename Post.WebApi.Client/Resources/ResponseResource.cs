using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "payment")]
    [XmlRoot("payment")]
    public class ResponseResource
    {
        [DataMember(Name = "code")]
        [XmlElement("code")]
        public string Code { get; set; }

        [DataMember(Name = "message")]
        [XmlElement("message")]
        public string Message { get; set; }
    }
}
