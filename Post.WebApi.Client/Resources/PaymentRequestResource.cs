using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "paymentRequestResource")]
    [XmlRoot("paymentRequestResource")]
    public class PaymentRequestResource
    {
        [DataMember(Name = "type")]
        [XmlElement("type")]
        public int Type { get; set; }

        [DataMember(Name = "subType")]
        [XmlElement("subType")]
        public int Subtype { get; set; }

        [DataMember(Name = "channel")]
        [XmlElement("channel")]
        public int Channel { get; set; }

        [DataMember(Name = "service")]
        [XmlElement("service")]
        public int Service { get; set; }

        [DataMember(Name = "callerReference")]
        [XmlElement("callerReference")]
        public string CallerReference { get; set; }

        [DataMember(Name = "initiatedBy")]
        [XmlElement("initiatedBy")]
        public string InitiatedBy { get; set; }

        [DataMember(Name = "ipAddress")]
        [XmlElement("ipAddress")]
        public string IpAddress { get; set; }

        [DataMember(Name = "callerId")]
        [XmlElement("callerId")]
        public string CallerId { get; set; }

        [DataMember(Name = "receiptRecipient")]
        [XmlElement("receiptRecipient")]
        public string ReceiptRecipient { get; set; }

        [DataMember(Name = "mnsUrl")]
        [XmlElement("mnsUrl")]
        public string MnsUrl { get; set; }

        [DataMember(Name = "merchant")]
        [XmlElement("merchant")]
        public MerchantResource Merchant { get; set; }

        [DataMember(Name = "transaction")]
        [XmlElement("transaction")]
        public TransactionRequestResource Transaction { get; set; }

        [DataMember(Name = "paymentMethod")]
        [XmlElement("paymentMethod")]
        public PaymentMethodResource PaymentMethod { get; set; }

        [DataMember(Name = "device")]
        [XmlElement("device")]
        public DeviceResource Device { get; set; }

        [DataMember(Name = "geoLocation")]
        [XmlElement("geoLocation")]
        public GeoLocationResource GeoLocation { get; set; }

        [DataMember(Name = "metadata", Order = 0)]
        [XmlArray("metadata", Order = 0)]
        [XmlArrayItem("metaDataItemResource")]
        public List<MetaDataItemResource> Metadata { get; set; }
    }
}
