using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    public enum PaymentMethodType
    {
        // ReSharper disable once InconsistentNaming
        card = 1,
        // ReSharper disable once InconsistentNaming
        dtpToken = 2,
        // ReSharper disable once InconsistentNaming
        dtpPreAuthToken = 3,
        // ReSharper disable once InconsistentNaming
        mapToken = 4
    };

    [DataContract(Name = "merchant")]
    [XmlRoot("merchant")]
    public class PaymentMethodResource
    {
        [DataContract(Name = "dtpToken")]
        [XmlRoot("dtpToken")]
        public class DtpTokenResource
        {
            [DataMember(Name = "value")]
            [XmlElement("value")]
            public string Value { get; set; }
        }

        [DataContract(Name = "dtpPreAuthToken")]
        [XmlRoot("dtpPreAuthToken")]
        public class DtpPreAuthTokenResource
        {
            [DataMember(Name = "value")]
            [XmlElement("value")]
            public string Value { get; set; }
        }

        [DataContract(Name = "mapToken")]
        [XmlRoot("mapToken")]
        public class MapTokenResource
        {
            [DataMember(Name = "value")]
            [XmlElement("value")]
            public string Value { get; set; }
        }

        

        [DataMember(Name = "type")]
        [XmlElement("type")]
        public PaymentMethodType Type { get; set; }

        [DataMember(Name = "dtpToken")]
        [XmlElement("dtpToken")]
        public DtpTokenResource DtpToken { get; set; }

        [DataMember(Name = "dtpPreAuthToken")]
        [XmlElement("dtpPreAuthToken")]
        public DtpPreAuthTokenResource DtpPreAuthToken { get; set; }

        [DataMember(Name = "mapToken")]
        [XmlElement("mapToken")]
        public MapTokenResource MapToken { get; set; }

        [DataMember(Name = "card")]
        [XmlElement("card")]
        public CardResource Card { get; set; }

    }

    [DataContract(Name = "card")]
    [XmlRoot("card")]
    public class CardResource
    {
        [DataMember(Name = "number")]
        [XmlElement("number")]
        public string Number { get; set; }

        [DataMember(Name = "expiryDate")]
        [XmlElement("expiryDate")]
        public string ExpiryDate { get; set; }

        [DataMember(Name = "cvv")]
        [XmlElement("cvv")]
        public string Cvv { get; set; }

        [DataMember(Name = "nameOnCard")]
        [XmlElement("nameOnCard")]
        public string NameOnCard { get; set; }

        [DataMember(Name = "createMapToken")]
        [XmlElement("createMapToken")]
        public bool CreateMapToken { get; set; }

        [DataMember(Name = "mapToken")]
        [XmlElement("mapToken")]
        public string MapToken { get; set; }

        [DataMember(Name = "cardScheme")]
        [XmlElement("cardScheme")]
        public string CardScheme { get; set; }

        [DataMember(Name = "cardBin")]
        [XmlElement("cardBin")]
        public string CardBin { get; set; }

        [DataMember(Name = "cardLastFour")]
        [XmlElement("cardLastFour")]
        public string CardLastFour { get; set; }
    }

    //[DataContract(Name = "dtpToken")]
    //[XmlRoot("dtpToken")]
    //public class DtpTokenResource
    //{
    //    [DataMember(Name = "value")]
    //    [XmlElement("value")]
    //    public string Value { get; set; }
    //}

    //[DataContract(Name = "dtpPreAuthToken")]
    //[XmlRoot("dtpPreAuthToken")]
    //public class DtpPreAuthTokenResource
    //{
    //    [DataMember(Name = "value")]
    //    [XmlElement("value")]
    //    public string Value { get; set; }
    //}

    //[DataContract(Name = "mapToken")]
    //[XmlRoot("mapToken")]
    //public class MapTokenResource
    //{
    //    [DataMember(Name = "value")]
    //    [XmlElement("value")]
    //    public string Value { get; set; }
    //}

    //[DataContract(Name = "card")]
    //[XmlRoot("card")]
    //public class CardResource
    //{
    //    [DataMember(Name = "number")]
    //    [XmlElement("number")]
    //    public string Number { get; set; }

    //    [DataMember(Name = "expiryDate")]
    //    [XmlElement("expiryDate")]
    //    public string ExpiryDate { get; set; }

    //    [DataMember(Name = "cvv")]
    //    [XmlElement("cvv")]
    //    public string Cvv { get; set; }

    //    [DataMember(Name = "nameOnCard")]
    //    [XmlElement("nameOnCard")]
    //    public string NameOnCard { get; set; }

    //    [DataMember(Name = "createMapToken")]
    //    [XmlElement("createMapToken")]
    //    public bool CreateMapToken { get; set; }

    //    [DataMember(Name = "mapToken")]
    //    [XmlElement("mapToken")]
    //    public string MapToken { get; set; }
    //}
}
