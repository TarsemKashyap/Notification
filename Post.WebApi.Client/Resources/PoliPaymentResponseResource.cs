using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "poliResponse")]
    [XmlRoot("poliResponse")]
    public class PoliPaymentResponse
    {
        [DataMember(Name = "navigateUrl", Order = 1)]
        [XmlElement("navigateUrl", Order = 1)]
        public string NavigateUrl { get; set; }

        [DataMember(Name = "countryName", Order = 2)]
        [XmlElement("countryName", Order = 2)]
        public string CountryName { get; set; }

        [DataMember(Name = "countryCode", Order = 3)]
        [XmlElement("countryCode", Order = 3)]
        public string CountryCode { get; set; }

        [DataMember(Name = "amountPaid", Order = 4)]
        [XmlElement("amountPaid", Order = 4)]
        public decimal AmountPaid { get; set; }

        [DataMember(Name = "InitiateRequestRecievedTimestamp", Order = 5)]
        [XmlElement("InitiateRequestRecievedTimestamp", Order = 5)]
        public string InitiateRequestRecievedTimestamp { get; set; }

        [DataMember(Name = "paymentStartTimestamp", Order = 6)]
        [XmlElement("paymentStartTimestamp", Order = 6)]
        public string PaymentStartTimestamp { get; set; }

        [DataMember(Name = "paymentEndTimestamp", Order = 7)]
        [XmlElement("paymentEndTimestamp", Order = 7)]
        public string PaymentEndTimestamp { get; set; }

        [DataMember(Name = "bankReceipt", Order = 8)]
        [XmlElement("bankReceipt", Order = 8)]
        public string BankReceipt { get; set; }

        [DataMember(Name = "bankReceiptTimestamp", Order = 9)]
        [XmlElement("bankReceiptTimestamp", Order = 9)]
        public string BankReceiptTimestamp { get; set; }

        [DataMember(Name = "fiCode", Order = 10)]
        [XmlElement("fiCode", Order = 10)]
        public string FiCode { get; set; }

        [DataMember(Name = "fiCountryCode", Order = 11)]
        [XmlElement("fiCountryCode", Order = 11)]
        public string FiCountryCode { get; set; }

        [DataMember(Name = "fiInstitutionName", Order = 12)]
        [XmlElement("fiInstitutionName", Order = 12)]
        public string FiInstitutionName { get; set; }

        [DataMember(Name = "merchantReference", Order = 13)]
        [XmlElement("merchantReference", Order = 13)]
        public string MerchantReference { get; set; }

        [DataMember(Name = "merchantData", Order = 14)]
        [XmlElement("merchantData", Order = 14)]
        public string MerchantData { get; set; }

        [DataMember(Name = "merchantAccountName", Order = 15)]
        [XmlElement("merchantAccountName", Order = 15)]
        public string MerchantAccountName { get; set; }

        [DataMember(Name = "merchantAccountSortCode", Order = 16)]
        [XmlElement("merchantAccountSortCode", Order = 16)]
        public string MerchantAccountSortCode { get; set; }

        [DataMember(Name = "merchantAccountSuffix", Order = 17)]
        [XmlElement("merchantAccountSuffix", Order = 17)]
        public string MerchantAccountSuffix { get; set; }

        [DataMember(Name = "merchantAccountNumber", Order = 18)]
        [XmlElement("merchantAccountNumber", Order = 18)]
        public string MerchantAccountNumber { get; set; }

        [DataMember(Name = "payerFirstName", Order = 19)]
        [XmlElement("payerFirstName", Order = 19)]
        public string PayerFirstName { get; set; }

        [DataMember(Name = "payerFamilyName", Order = 20)]
        [XmlElement("payerFamilyName", Order = 20)]
        public string PayerFamilyName { get; set; }

        [DataMember(Name = "payerAccountSortCode", Order = 21)]
        [XmlElement("payerAccountSortCode", Order = 21)]
        public string PayerAccountSortCode { get; set; }

        [DataMember(Name = "payerAccountNumber", Order = 22)]
        [XmlElement("payerAccountNumber", Order = 22)]
        public string PayerAccountNumber { get; set; }

        [DataMember(Name = "payerAccountSuffix", Order = 23)]
        [XmlElement("payerAccountSuffix", Order = 23)]
        public string PayerAccountSuffix { get; set; }

        [DataMember(Name = "transactionId", Order = 23)]
        [XmlElement("transactionId", Order = 23)]
        public string TransactionId { get; set; }
    }
}
