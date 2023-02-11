using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "poliPayment", Namespace = "")]
    [XmlRoot("poliPayment")]
    public class PoliPaymentResource
    {
        [DataMember(Name = "number", Order = 1)]
        [XmlElement("number", Order = 1)]
        public string Number { get; set; }

        [DataMember(Name = "timestampUtc", Order = 2)]
        [XmlElement("timestampUtc", Order = 2)]
        public string TimestampUtc { get; set; }

        [DataMember(Name = "type", Order = 3)]
        [XmlElement("type", Order = 3)]
        public int Type { get; set; }

        [DataMember(Name = "channel", Order = 4)]
        [XmlElement("channel", Order = 4)]
        public int Channel { get; set; }

        [DataMember(Name = "service", Order = 5)]
        [XmlElement("navigateUrl", Order = 5)]
        public int Service { get; set; }

        [DataMember(Name = "status", Order = 6)]
        [XmlElement("status", Order = 6)]
        public string Status { get; set; }

        [DataMember(Name = "callerReference", Order = 7)]
        [XmlElement("callerReference", Order = 7)]
        public string CallerReference { get; set; }

        [DataMember(Name = "initiatedBy", Order = 8)]
        [XmlElement("initiatedBy", Order = 8)]
        public string InitiatedBy { get; set; }

        [DataMember(Name = "ipAddress", Order = 9)]
        [XmlElement("ipAddress", Order = 9)]
        public string IpAddress { get; set; }

        [DataMember(Name = "receiptRecipient", Order = 10)]
        [XmlElement("receiptRecipient", Order = 10)]
        public string ReceiptRecipient { get; set; }

        [DataMember(Name = "merchant", Order = 11)]
        [XmlElement("merchant", Order = 11)]
        public MerchantResource Merchant { get; set; }

        [DataMember(Name = "transaction", Order = 12)]
        [XmlElement("transaction", Order = 12)]
        public PoliTransactionResource Transaction { get; set; }

        [DataMember(Name = "response", Order = 13)]
        [XmlElement("response", Order = 13)]
        public ResponseResource Response { get; set; }

        [DataMember(Name = "poliResponse", Order = 17)]
        [XmlElement("poliResponse", Order = 17)]
        public PoliPaymentResponse PoliResponse { get; set; }

        [DataMember(Name = "device", Order = 18)]
        [XmlElement("device", Order = 18)]
        public DeviceResource Device { get; set; }

        [DataMember(Name = "geoLocation", Order = 19)]
        [XmlElement("geoLocation", Order = 19)]
        public GeoLocationResource GeoLocation { get; set; }

        [DataMember(Name = "metadata", Order = 20)]
        [XmlElement("metadata", Order = 20)]
        public List<MetaDataItemResource> Metadata { get; set; }

        [DataContract(Name = "transaction", Namespace = "")]
        [XmlRoot("transaction")]
        public class PoliTransactionResource
        {
            [DataMember(Name = "amount", Order = 1)]
            [XmlElement("amount", Order = 1)]
            public decimal Amount { get; set; }

            [DataMember(Name = "currency", Order = 2)]
            [XmlElement("currency", Order = 2)]
            public string Currency { get; set; }

            [DataMember(Name = "reference1", Order = 3)]
            [XmlElement("reference1", Order = 3)]
            public string Reference1 { get; set; }

            [DataMember(Name = "reference2", Order = 4)]
            [XmlElement("reference2", Order = 4)]
            public string Reference2 { get; set; }
        }
    }
}
