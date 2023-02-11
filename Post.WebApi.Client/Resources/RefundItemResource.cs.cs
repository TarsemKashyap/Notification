using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "refundItem")]
    [XmlRoot("refundItem")]
    public class RefundItemResource
    {
        [DataMember(Name = "number")]
        [XmlElement("number")]
        public string Number { get; set; }

        [DataMember(Name = "timestamp")]
        [XmlElement("timestamp")]
        public string Timestamp { get; set; }

        [DataMember(Name = "amount")]
        [XmlElement("amount")]
        public decimal Amount { get; set; }

        [DataMember(Name = "status")]
        [XmlElement("status")]
        public int Status { get; set; }

        [DataMember(Name = "currency")]
        [XmlElement("currency")]
        public string Currency { get; set; }

        [DataMember(Name = "reference")]
        [XmlElement("reference")]
        public string Reference { get; set; }

        [DataMember(Name = "particulars")]
        [XmlElement("particulars")]
        public string Particulars { get; set; }

        [DataMember(Name = "providerResponse")]
        [XmlElement("providerResponse")]
        public string ProviderResponse { get; set; }

        [DataMember(Name = "responseCode")]
        [XmlElement("responseCode")]
        public int? ResponseCode { get; set; }

        [DataMember(Name = "responseMessage")]
        [XmlElement("responseMessage")]
        public string ResponseMessage { get; set; }

        [DataMember(Name = "authCode")]
        [XmlElement("authCode")]
        public string AuthCode { get; set; }
    }
}
