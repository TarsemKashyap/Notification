using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "verificationRequestResource")]
    [XmlRoot("verificationRequestResource")]
    public class VerificationRequestResource
    {
        [DataMember(Name = "callerReference")]
        [XmlElement("callerReference")]
        public string CallerReference { get; set; }

        [DataMember(Name = "initiatedBy")]
        [XmlElement("initiatedBy")]
        public string InitiatedBy { get; set; }

        [DataMember(Name = "ipAddress")]
        [XmlElement("ipAddress")]
        public string IpAddress { get; set; }

        [DataMember(Name = "merchant")]
        [XmlElement("merchant")]
        public MerchantResource Merchant { get; set; }

        [DataMember(Name = "paymentMethod")]
        [XmlElement("paymentMethod")]
        public PaymentMethodResource PaymentMethod { get; set; }

        [DataMember(Name = "transaction")]
        [XmlElement("transaction")]
        public TransactionRequestResource Transaction { get; set; }
    }
}
