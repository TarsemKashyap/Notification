using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "captureRequestResource")]
    [XmlRoot("captureRequestResource")]
    public class CaptureRequestResource
    {
        [DataMember(Name = "subType")]
        [XmlElement("subType")]
        public int Subtype { get; set; }

        [DataMember(Name = "channel")]
        [XmlElement("channel")]
        public int Channel { get; set; }

        [DataMember(Name = "service")]
        [XmlElement("service")]
        public int Service { get; set; }

        [DataMember(Name = "initiatedBy")]
        [XmlElement("initiatedBy")]
        public string InitiatedBy { get; set; }

        [DataMember(Name = "ipAddress")]
        [XmlElement("ipAddress")]
        public string IpAddress { get; set; }

        [DataMember(Name = "receiptRecipient")]
        [XmlElement("receiptRecipient")]
        public string ReceiptRecipient { get; set; }

        [DataMember(Name = "amount")]
        [XmlElement("amount")]
        public decimal Amount { get; set; }

        [DataMember(Name = "reference")]
        [XmlElement("reference")]
        public string Reference { get; set; }

        [DataMember(Name = "particulars")]
        [XmlElement("particulars")]
        public string Particulars { get; set; }

        [DataMember(Name = "callerReference")]
        [XmlElement("callerReference")]
        public string CallerReference { get; set; }

        [DataMember(Name = "device")]
        [XmlElement("device")]
        public DeviceResource Device { get; set; }

        [DataMember(Name = "geoLocation")]
        [XmlElement("geoLocation")]
        public GeoLocationResource GeoLocation { get; set; }
    }
}
