using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "geoLocation")]
    [XmlRoot("geoLocation")]
    public class GeoLocationResource
    {
        [DataMember(Name = "latitude")]
        [XmlElement("latitude")]
        public string Latitude { get; set; }

        [DataMember(Name = "longitude")]
        [XmlElement("longitude")]
        public string Longitude { get; set; }
    }
}
