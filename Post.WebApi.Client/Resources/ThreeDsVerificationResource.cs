using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "threeDsVerificationResource")]
    [XmlRoot("threeDsVerificationResource")]
    public class ThreeDsVerificationResource : ThreeDsVerificationRequestResource
    {
        //[DataContract(Name = "merchant")]
        //[XmlRoot("merchant")]
        //public class MerchantResource
        //{
        //    [DataMember(Name = "id")]
        //    [XmlElement("id")]
        //    public string Id { get; set; }
        //}

        [DataContract(Name = "verificationResponse")]
        [XmlRoot("verificationResponse")]
        public class VerificationResponse
        {
            [DataMember(Name = "enrolled")]
            [XmlElement("enrolled")]
            public string Enrolled { get; set; }

            [DataMember(Name = "acsUrl")]
            [XmlElement("acsUrl")]
            public string AcsUrl {get; set; }

            [DataMember(Name = "pareq")]
            [XmlElement("pareq")]
            public string Pareq { get; set; }
        }

        [DataContract(Name = "result")]
        [XmlRoot("result")]
        public class Result
        {
            [DataMember(Name = "eci")]
            [XmlElement("eci")]
            public string Eci { get; set; }

            [DataMember(Name = "xid")]
            [XmlElement("xid")]
            public string Xid { get; set; }

            [DataMember(Name = "status")]
            [XmlElement("status")]
            public string Status { get; set; }

            [DataMember(Name = "cavv")]
            [XmlElement("cavv")]
            public string Cavv { get; set; }

            [DataMember(Name = "cavvAlgorithm")]
            [XmlElement("cavvAlgorithm")]
            public string CavvAlgorithm { get; set; }

            [DataMember(Name = "continueStatus")]
            [XmlElement("continueStatus")]
            public string ContinueStatus { get; set; }

            [DataMember(Name = "continueToProcess")]
            [XmlElement("continueToProcess")]
            public bool ContinueToProcess { get; set; }
        }


        //todo: Id is long
        [DataMember(Name = "id")]
        [XmlElement("id")]
        public string Id { get; set; }

        //[DataMember(Name = "ipAddress")]
        //[XmlElement("ipAddress")]
        //public string IpAddress { get; set; }

        //[DataMember(Name = "userAgent")]
        //[XmlElement("userAgent")]
        //public string UserAgent { get; set; }

        //[DataMember(Name = "amount")]
        //[XmlElement("amount")]
        //public decimal Amount { get; set; }

        //[DataMember(Name = "currency")]
        //[XmlElement("currency")]
        //public string Currency { get; set; }

        //[DataMember(Name = "description")]
        //[XmlElement("description")]
        //public string Description { get; set; }

        //[DataMember(Name = "merchant")]
        //[XmlElement("merchant")]
        //public MerchantResource Merchant { get; set; }

        //[DataMember(Name = "card")]
        //[XmlElement("card")]
        //public CardResource Card { get; set; }

        [DataMember(Name = "verificationResponse")]
        [XmlElement("verificationResponse")]
        public VerificationResponse Response { get; set; }

        [DataMember(Name = "result")]
        [XmlElement("result")]
        public Result ThreeDsResult { get; set; }
    }
}
