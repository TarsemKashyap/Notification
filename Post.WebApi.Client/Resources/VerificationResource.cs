using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "verification")]
    [XmlRoot("verification")]
    public class VerificationResource
    {
        [DataMember(Name = "id")]
        [XmlElement("id")]
        public long Id { get; set; }

        [DataMember(Name = "number")]
        [XmlElement("number")]
        public string Number { get; set; }

        [DataMember(Name = "timestamp")]
        [XmlElement("timestamp")]
        public string Timestamp { get; set; }

        [DataMember(Name = "status")]
        [XmlElement("status")]
        public int Status { get; set; }

        [DataMember(Name = "callerReference")]
        [XmlElement("callerReference")]
        public string CallerReference { get; set; }

        [DataMember(Name = "initiatedBy")]
        [XmlElement("initiatedBy")]

        public string InitiatedBy { get; set; }
        [DataMember(Name = "response")]
        [XmlElement("response")]
        public ResponseResource Response { get; set; }

        [DataMember(Name = "merchant")]
        [XmlElement("merchant")]
        public MerchantResource Merchant { get; set; }

        [DataMember(Name = "transaction")]
        [XmlElement("transaction")]
        public TransactionResource Transaction { get; set; }

        [DataMember(Name = "paymentMethod")]
        [XmlElement("paymentMethod")]
        public PaymentMethodResource PaymentMethod { get; set; }
    }
}
