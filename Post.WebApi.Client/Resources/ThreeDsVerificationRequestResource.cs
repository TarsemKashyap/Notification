using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "threeDsVerificationRequestResource")]
    [XmlRoot("threeDsVerificationRequestResource")]
    public class ThreeDsVerificationRequestResource
    {
        //[DataContract(Name = "merchant")]
        //[XmlRoot("merchant")]
        //public class MerchantResource
        //{
        //    [DataMember(Name = "id")]
        //    [XmlElement("id")]
        //    public string Id { get; set; }
        //}

        [DataMember(Name = "ipAddress")]
        [XmlElement("ipAddress")]
        public string IpAddress { get; set; }

        [DataMember(Name = "callerReference")]
        [XmlElement("callerReference")]
        public string CallerReference { get; set; }

        [DataMember(Name = "userAgent")]
        [XmlElement("userAgent")]
        public string UserAgent { get; set; }

        [DataMember(Name = "amount")]
        [XmlElement("amount")]
        public decimal Amount { get; set; }

        [DataMember(Name = "currency")]
        [XmlElement("currency")]
        public string Currency { get; set; }

        [DataMember(Name = "description")]
        [XmlElement("description")]
        public string Description { get; set; }

        [DataMember(Name = "merchant")]
        [XmlElement("merchant")]
        public MerchantResource Merchant { get; set; }

        [DataMember(Name = "card")]
        [XmlElement("card")]
        public CardResource Card { get; set; }
    }
}
