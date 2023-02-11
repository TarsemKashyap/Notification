using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace Example.Notific.PG.WebApi.Client.Resources
{
    public enum TransactionFrequency
    {
        // ReSharper disable once InconsistentNaming
        SINGLE,
        // ReSharper disable once InconsistentNaming
        RECURRING
    };

    public enum TransactionStatus
    {
        // ReSharper disable once InconsistentNaming
        SUCCESSFUL,
        // ReSharper disable once InconsistentNaming
        DECLINED,
        // ReSharper disable once InconsistentNaming
        BLOCKED,
        // ReSharper disable once InconsistentNaming
        FAILED,
        // ReSharper disable once InconsistentNaming
        UNKNOWN
    };

    [DataContract(Name = "cardpayment")]
    [XmlRoot("cardpayment")]
    public class Cardpayment 
    {
        [DataMember(Name = "number")]
        [XmlElement("number")]
        public string Number { get; set; }

        [DataMember(Name = "receipt")]
        [XmlElement("receipt")]
        public string Receipt { get; set; }

        [DataMember(Name = "date")]
        [XmlElement("date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "status")]
        [XmlElement("status")]
        public TransactionStatus Status { get; set; }

        [DataMember(Name = "paymentChannel")]
        [XmlElement("paymentChannel")]
        public int PaymentChannel { get; set; }


        [DataMember(Name = "channelService")]
        [XmlElement("channelService")]
        public int ChannelService { get; set; }

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

        [DataMember(Name = "merchant")]
        [XmlElement("merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "transaction")]
        [XmlElement("transaction")]
        public PaymentTransaction Transaction { get; set; }

        [DataMember(Name = "card")]
        [XmlElement("card")]
        public Card Card { get; set; }

        [DataMember(Name = "device")]
        [XmlElement("device")]
        public Device Device { get; set; }

        [DataMember(Name = "geoLocation")]
        [XmlElement("geoLocation")]
        public GeoLocation GeoLocation { get; set; }
    }

    [DataContract(Name = "merchant")]
    [XmlRoot("merchant")]
    public class Merchant
    {
        [DataMember(Name = "id")]
        [XmlElement("id")]
        public int Id { get; set; }

        [DataMember(Name = "subAccount")]
        [XmlElement("subAccount")]
        public int SubAccount { get; set; }
    }

    [DataContract(Name = "transaction")]
    [XmlRoot("transaction")]
    public class PaymentTransaction
    {
        [DataMember(Name = "amount")]
        [XmlElement("amount")]
        public decimal Amount { get; set; }

        [DataMember(Name = "currency")]
        [XmlElement("currency")]
        public string Currency { get; set; }

        [DataMember(Name = "reference")]
        [XmlElement("reference")]
        public string Reference { get; set; }

        [DataMember(Name = "particulars")]
        [XmlElement("particulars")]
        public string Particulars { get; set; }

        [DataMember(Name = "frequency")]
        [XmlElement("frequency")]
        public string Frequency { get; set; }
    }

    [DataContract(Name = "card")]
    [XmlRoot("card")]
    public class Card
    {
        [DataMember(Name = "mapToken")]
        [XmlElement("mapToken")]
        public string MapToken { get; set; }

        [DataMember(Name = "dtpToken")]
        [XmlElement("dtpToken")]
        public string DtpToken { get; set; }

        [DataMember(Name = "cardNumber")]
        [XmlElement("cardNumber")]
        public string CardNumber { get; set; }

        [DataMember(Name = "expiryDate")]
        [XmlElement("expiryDate")]
        public string ExpiryDate { get; set; }

        [DataMember(Name = "cardHolderName")]
        [XmlElement("cardHolderName")]
        public string CardHolderName { get; set; }
    }

    [DataContract(Name = "device")]
    [XmlRoot("device")]
    public class Device
    {
        [DataMember(Name = "id")]
        [XmlElement("id")]
        public string Id { get; set; }

        [DataMember(Name = "description")]
        [XmlElement("description")]
        public string Description { get; set; }
    }


    [DataContract(Name = "geoLocation")]
    [XmlRoot("geoLocation")]
    public class GeoLocation
    {
        [DataMember(Name = "latitude")]
        [XmlElement("latitude")]
        public string Latitude { get; set; }

        [DataMember(Name = "longitude")]
        [XmlElement("longitude")]
        public string Longitude { get; set; }
    }
}
