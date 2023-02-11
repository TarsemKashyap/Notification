using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "transactionRequest")]
    [XmlRoot("transactionRequest")]
    public class TransactionRequestResource
    {
        [DataMember(Name = "amount")]
        [XmlElement("amount")]
        public decimal Amount { get; set; }

        [DataMember(Name = "amountRefunded")]
        [XmlElement("amountRefunded")]
        public decimal AmountRefunded { get; set; }

        [DataMember(Name = "amountCaptured")]
        [XmlElement("amountCaptured")]
        public decimal AmountCaptured { get; set; }

        [DataMember(Name = "currency")]
        [XmlElement("currency")]
        public string Currency { get; set; }

        [DataMember(Name = "reference")]
        [XmlElement("reference")]
        public string Reference { get; set; }

        [DataMember(Name = "particulars")]
        [XmlElement("particulars")]
        public string Particulars { get; set; }
    }

    [DataContract(Name = "transaction")]
    [XmlRoot("transaction")]
    public class TransactionResource: TransactionRequestResource
    {      

        [DataMember(Name = "source")]
        [XmlElement("source")]
        public string Source { get; set; }

        [DataMember(Name = "frequency")]
        [XmlElement("frequency")]
        public string Frequency { get; set; }


        [DataMember(Name = "providerResponse")]
        [XmlElement("providerResponse")]
        public string ProviderResponse { get; set; }

        [DataMember(Name = "authCode")]
        [XmlElement("authCode")]
        public string AuthCode { get; set; }

        [DataMember(Name = "settlementDate")]
        [XmlElement("settlementDate")]
        public string SettlementDate { get; set; }
    }
}
