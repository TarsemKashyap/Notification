using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Example.Notific.TPF.WebApi.Client.Resources
{
    [DataContract(Name = "cardPayment", Namespace = "")]
    [XmlRoot("cardPayment", Namespace = "")]
    public class TPCardPayment
    {
        [DataMember(Name = "number", Order = 1)]
        [XmlElement("number", IsNullable = true, Order = 1)]
        public string Number { get; set; }

        [DataMember(Name = "receipt", Order = 2)]
        [XmlElement("receipt", IsNullable = true, Order = 2)]
        public string Receipt { get; set; }

        [DataMember(Name = "type", Order = 3)]
        [XmlElement("type", IsNullable = true, Order = 3)]
        public int Type { get; set; }

        [DataMember(Name = "service", Order = 4)]
        [XmlElement("service", IsNullable = true, Order = 4)]
        public int Service { get; set; }

        [DataMember(Name = "callerReference", Order = 5)]
        [XmlElement("callerReference", IsNullable = true, Order = 5)]
        public string CallerReference { get; set; }

        [DataMember(Name = "initiatedBy", Order = 6)]
        [XmlElement("initiatedBy", IsNullable = true, Order = 6)]
        public string InitiatedBy { get; set; }

        [DataMember(Name = "ipAddress", Order = 7)]
        [XmlElement("ipAddress", IsNullable = true, Order = 7)]
        public string IpAddress { get; set; }

        [DataMember(Name = "callerId", Order = 8)]
        [XmlElement("callerId", IsNullable = true, Order = 8)]
        public string CallerId { get; set; }

        [DataMember(Name = "receiptRecipient", Order = 9)]
        [XmlElement("receiptRecipient", IsNullable = true, Order = 9)]
        public string ReceiptRecipient { get; set; }

        [DataMember(Name = "parent", Order = 10)]
        [XmlElement("parent", IsNullable = true, Order = 10)]
        public string Parent { get; set; }

        [DataMember(Name = "status", Order = 11)]
        [XmlElement("status", IsNullable = true, Order = 11)]
        public int Status { get; set; }

        [DataMember(Name = "merchant", Order = 12)]
        [XmlElement("merchant", IsNullable = true, Order = 12)]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "card", Order = 13)]
        [XmlElement("card", IsNullable = true, Order = 13)]
        public Card Card { get; set; }

        [DataMember(Name = "transaction", Order = 14)]
        [XmlElement("transaction", IsNullable = true, Order = 14)]
        public Transaction Transaction { get; set; }

        [DataMember(Name = "device", Order = 15)]
        [XmlElement("device", IsNullable = true, Order = 15)]
        public Device Device { get; set; }

        [DataMember(Name = "geolocation", Order = 16)]
        [XmlElement("geolocation", IsNullable = true, Order = 16)]
        public Geolocation Geolocation { get; set; }

        [DataMember(Name = "refunds", Order = 17)]
        [XmlElement("refunds", IsNullable = true, Order = 17)]
        public List<Refunds> Refunds { get; set; }

        [DataMember(Name = "captures", Order = 18)]
        [XmlElement("captures", IsNullable = true, Order = 18)]
        public List<Captures> Captures { get; set; }
    }

    [DataContract(Name = "merchant", Namespace = "")]
    [XmlRoot("merchant", Namespace = "")]
    public class Merchant
    {
        [DataMember(Name = "id", Order = 1)]
        [XmlElement("id", IsNullable = true, Order = 1)]
        public int Id { get; set; }

        [DataMember(Name = "subAccountId", Order = 2)]
        [XmlElement("subAccountId", IsNullable = true, Order = 2)]
        public int SubAccountId { get; set; }
    }

    [DataContract(Name = "card", Namespace = "")]
    [XmlRoot("card", Namespace = "")]
    public class Card
    {
        [DataMember(Name = "mapToken", Order = 1)]
        [XmlElement("mapToken", IsNullable = true, Order = 1)]
        public string MapToken { get; set; }

        [DataMember(Name = "cardScheme", Order = 2)]
        [XmlElement("cardScheme", IsNullable = true, Order = 2)]
        public string CardScheme { get; set; }

        [DataMember(Name = "cardNumber", Order = 3)]
        [XmlElement("cardNumber", IsNullable = true, Order = 3)]
        public string CardNumber { get; set; }

        [DataMember(Name = "expiryDate", Order = 4)]
        [XmlElement("expiryDate", IsNullable = true, Order = 4)]
        public string ExpiryDate { get; set; }

        [DataMember(Name = "nameOnCard", Order = 5)]
        [XmlElement("nameOnCard", IsNullable = true, Order = 5)]
        public string NameOnCard { get; set; }

        [DataMember(Name = "cardBin", Order = 6)]
        [XmlElement("cardBin", IsNullable = true, Order = 6)]
        public string CardBin { get; set; }

        [DataMember(Name = "cardLastFour", Order = 7)]
        [XmlElement("cardLastFour", IsNullable = true, Order = 7)]
        public string CardLastFour { get; set; }

        [DataMember(Name = "cardTypeId", Order = 8)]
        [XmlElement("cardTypeId", IsNullable = true, Order = 8)]
        public int CardTypeId { get; set; }
    }

    [DataContract(Name = "transaction", Namespace = "")]
    [XmlRoot("transaction", Namespace = "")]
    public class Transaction
    {
        [DataMember(Name = "date", Order = 1)]
        [XmlElement("date", IsNullable = true, Order = 1)]
        public DateTime Date { get; set; }

        [DataMember(Name = "amount", Order = 2)]
        [XmlElement("amount", IsNullable = true, Order = 2)]
        public decimal Amount { get; set; }

        [DataMember(Name = "currency", Order = 3)]
        [XmlElement("currency", IsNullable = true, Order = 3)]
        public string Currency { get; set; }

        [DataMember(Name = "reference", Order = 4)]
        [XmlElement("reference", IsNullable = true, Order = 4)]
        public string Reference { get; set; }

        [DataMember(Name = "particulars", Order = 5)]
        [XmlElement("particulars", IsNullable = true, Order = 5)]
        public string Particulars { get; set; }

        [DataMember(Name = "providerResponse", Order = 6)]
        [XmlElement("providerResponse", IsNullable = true, Order = 6)]
        public string ProviderResponse { get; set; }

        [DataMember(Name = "responseCode", Order = 7)]
        [XmlElement("responseCode", IsNullable = true, Order = 7)]
        public int ResponseCode { get; set; }

        [DataMember(Name = "responseMessage", Order = 8)]
        [XmlElement("responseMessage", IsNullable = true, Order = 8)]
        public string ResponseMessage { get; set; }

        [DataMember(Name = "authCode", Order = 9)]
        [XmlElement("authCode", IsNullable = true, Order = 9)]
        public string AuthCode { get; set; }

        [DataMember(Name = "settlementDate", Order = 10)]
        [XmlElement("settlementDate", IsNullable = true, Order = 10)]
        public string SettlementDate { get; set; }

        [DataMember(Name = "amount_refunded", Order = 11)]
        [XmlElement("amount_refunded", IsNullable = true, Order = 11)]
        public decimal AmountRefunded { get; set; }

        [DataMember(Name = "amount_captured", Order = 12)]
        [XmlElement("amount_captured", IsNullable = true, Order = 12)]
        public decimal AmountCaptured { get; set; }
    }

    [DataContract(Name = "device", Namespace = "")]
    [XmlRoot("device", Namespace = "")]
    public class Device
    {
        [DataMember(Name = "id", Order = 1)]
        [XmlElement("id", IsNullable = true, Order = 1)]
        public string Id { get; set; }

        [DataMember(Name = "description", Order = 2)]
        [XmlElement("description", IsNullable = true, Order = 2)]
        public string Description { get; set; }
    }

    [DataContract(Name = "geolocation", Namespace = "")]
    [XmlRoot("geolocation", Namespace = "")]
    public class Geolocation
    {
        [DataMember(Name = "latitude", Order = 1)]
        [XmlElement("latitude", IsNullable = true, Order = 1)]
        public string Latitude { get; set; }

        [DataMember(Name = "longitude", Order = 2)]
        [XmlElement("longitude", IsNullable = true, Order = 2)]
        public string Longitude { get; set; }
    }

    [DataContract(Name = "refunds", Namespace = "")]
    [XmlRoot("refunds", Namespace = "")]
    public class Refunds
    {
        [DataMember(Name = "number", Order = 1)]
        [XmlElement("number", IsNullable = true, Order = 1)]
        public string Number { get; set; }

        [DataMember(Name = "date", Order = 2)]
        [XmlElement("date", IsNullable = true, Order = 2)]
        public DateTime Date { get; set; }

        [DataMember(Name = "amount", Order = 3)]
        [XmlElement("amount", IsNullable = true, Order = 3)]
        public decimal Amount { get; set; }

        [DataMember(Name = "currency", Order = 4)]
        [XmlElement("currency", IsNullable = true, Order = 4)]
        public string Currency { get; set; }

        [DataMember(Name = "reference", Order = 5)]
        [XmlElement("reference", IsNullable = true, Order = 5)]
        public string Reference { get; set; }

        [DataMember(Name = "particulars", Order = 6)]
        [XmlElement("particulars", IsNullable = true, Order = 6)]
        public string Particulars { get; set; }

        [DataMember(Name = "providerResponse", Order = 7)]
        [XmlElement("providerResponse", IsNullable = true, Order = 7)]
        public string ProviderResponse { get; set; }

        [DataMember(Name = "responseCode", Order = 8)]
        [XmlElement("responseCode", IsNullable = true, Order = 8)]
        public int ResponseCode { get; set; }

        [DataMember(Name = "responseMessage", Order = 9)]
        [XmlElement("responseMessage", IsNullable = true, Order = 9)]
        public string ResponseMessage { get; set; }

        [DataMember(Name = "authCode", Order = 10)]
        [XmlElement("authCode", IsNullable = true, Order = 10)]
        public string AuthCode { get; set; }
    }

    [DataContract(Name = "captures", Namespace = "")]
    [XmlRoot("captures", Namespace = "")]
    public class Captures
    {
        [DataMember(Name = "number", Order = 1)]
        [XmlElement("number", IsNullable = true, Order = 1)]
        public string Number { get; set; }

        [DataMember(Name = "date", Order = 2)]
        [XmlElement("date", IsNullable = true, Order = 2)]
        public DateTime Date { get; set; }

        [DataMember(Name = "amount", Order = 3)]
        [XmlElement("amount", IsNullable = true, Order = 3)]
        public decimal Amount { get; set; }

        [DataMember(Name = "currency", Order = 4)]
        [XmlElement("currency", IsNullable = true, Order = 4)]
        public string Currency { get; set; }

        [DataMember(Name = "reference", Order = 5)]
        [XmlElement("reference", IsNullable = true, Order = 5)]
        public string Reference { get; set; }

        [DataMember(Name = "particulars", Order = 6)]
        [XmlElement("particulars", IsNullable = true, Order = 6)]
        public string Particulars { get; set; }

        [DataMember(Name = "providerResponse", Order = 7)]
        [XmlElement("providerResponse", IsNullable = true, Order = 7)]
        public string ProviderResponse { get; set; }

        [DataMember(Name = "responseCode", Order = 8)]
        [XmlElement("responseCode", IsNullable = true, Order = 8)]
        public int ResponseCode { get; set; }

        [DataMember(Name = "responseMessage", Order = 9)]
        [XmlElement("responseMessage", IsNullable = true, Order = 9)]
        public string ResponseMessage { get; set; }

        [DataMember(Name = "authCode", Order = 10)]
        [XmlElement("authCode", IsNullable = true, Order = 10)]
        public string AuthCode { get; set; }
    }
}
