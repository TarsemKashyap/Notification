using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Example.Notific.TPF.WebApi.Client.Resources
{
    [DataContract(Name = "recurringCardPlan", Namespace = "")]
    [XmlRoot("recurringCardPlan", Namespace = "")]
    public class TPRecurringCardPlan
    {
        [DataMember(Name = "id", Order = 1)]
        [XmlElement("id", IsNullable = true, Order = 1)]
        public int Id { get; set; }

        [DataMember(Name = "created", Order = 2)]
        [XmlElement("created", IsNullable = true, Order = 2)]
        public DateTime Created { get; set; }

        [DataMember(Name = "startDate", Order = 3)]
        [XmlElement("startDate", IsNullable = true, Order = 3)]
        public DateTime StartDate { get; set; }

        [DataMember(Name = "amendmentDate", Order = 4)]
        [XmlElement("amendmentDate", IsNullable = true, Order = 4)]
        public DateTime? AmendmentDate { get; set; }

        [DataMember(Name = "nextPaymentDate", Order = 4)]
        [XmlElement("nextPaymentDate", IsNullable = true, Order = 4)]
        public DateTime? NextPaymentDate { get; set; }

        [DataMember(Name = "type", Order = 5)]
        [XmlElement("type", IsNullable = true, Order = 5)]
        public int Type { get; set; }

        [DataMember(Name = "frequency", Order = 7)]
        [XmlElement("frequency", IsNullable = true, Order = 7)]
        public int Frequency { get; set; }

        [DataMember(Name = "status", Order = 8)]
        [XmlElement("status", IsNullable = true, Order = 8)]
        public int Status { get; set; }

        [DataMember(Name = "statusChangedDate", Order = 9)]
        [XmlElement("statusChangedDate", IsNullable = true, Order = 9)]
        public DateTime StatusChangedDate { get; set; }

        [DataMember(Name = "amount", Order = 10)]
        [XmlElement("amount", IsNullable = true, Order = 10)]
        public decimal Amount { get; set; }

        [DataMember(Name = "totalAmount", Order = 11)]
        [XmlElement("totalAmount", IsNullable = true, Order = 11)]
        public decimal TotalAmount { get; set; }

        [DataMember(Name = "reference", Order = 12)]
        [XmlElement("reference", IsNullable = true, Order =12)]
        public string Reference { get; set; }

        [DataMember(Name = "particulars", Order = 13)]
        [XmlElement("particulars", IsNullable = true, Order = 13)]
        public string Particulars { get; set; }

        [DataMember(Name = "instalmentFailOption", Order = 14)]
        [XmlElement("instalmentFailOption", IsNullable = true, Order = 14)]
        public int InstalmentFailOption { get; set; }

        [DataMember(Name = "retryPreferences", Order = 15)]
        [XmlElement("retryPreferences", IsNullable = true, Order = 15)]
        public RetryPreferences RetryPreferences { get; set; }

        [DataMember(Name = "merchant", Order = 16)]
        [XmlElement("merchant", IsNullable = true, Order = 16)]
        public RdMerchant Merchant { get; set; }

        [DataMember(Name = "initialPayment", Order = 17)]
        [XmlElement("initialPayment", IsNullable = true, Order = 17)]
        public InitialPayment InitialPayment { get; set; }

        [DataMember(Name = "payer", Order = 18)]
        [XmlElement("payer", IsNullable = true, Order = 18)]
        public Payer Payer { get; set; }

        [DataMember(Name = "card", Order = 19)]
        [XmlElement("card", IsNullable = true, Order = 19)]
        public RdCard Card { get; set; }

        [DataMember(Name = "card", Order = 20)]
        [XmlElement("card", IsNullable = true, Order = 20)]
        public List<Amendments> Amendments { get; set; }

        [DataMember(Name = "paymentSchedule", Order = 19)]
        [XmlElement("paymentSchedule", IsNullable = true, Order = 19)]
        public List<CardPlanPaymentSchedule> PaymentSchedule { get; set; }
        

    }

    [DataContract(Name = "retryPreferences", Namespace = "")]
    [XmlRoot("retryPreferences", Namespace = "")]
    public class RetryPreferences
    {
        [DataMember(Name = "perform", Order = 1)]
        [XmlElement("perform", IsNullable = true, Order = 1)]
        public bool Perform { get; set; }

        [DataMember(Name = "frequencyInDays", Order = 2)]       
        [XmlElement("frequencyInDays", IsNullable = true, Order = 2)]
        public int FrequencyInDays { get; set; }

        [DataMember(Name = "maxAttempts", Order = 3)]
        [XmlElement("maxAttempts", IsNullable = true, Order = 3)]
        public int MaxAttempts { get; set; }
    }

    [DataContract(Name = "merchant", Namespace = "")]
    [XmlRoot("merchant", Namespace = "")]
    public class RdMerchant
    {
        [DataMember(Name = "id", Order = 1)]
        [XmlElement("id", IsNullable = true, Order = 1)]
        public int Id { get; set; }

        [DataMember(Name = "subAccount", Order = 2)]
        [XmlElement("subAccount", IsNullable = true, Order = 2)]
        public int SubAccountId { get; set; }
    }

    [DataContract(Name = "initialPayment", Namespace = "")]
    [XmlRoot("initialPayment", Namespace = "")]
    public class InitialPayment
    {
        [DataMember(Name = "date", Order = 1)]
        [XmlElement("date", IsNullable = true, Order = 1)]
        public DateTime Date { get; set; }

        [DataMember(Name = "amount", Order = 2)]
        [XmlElement("amount", IsNullable = true, Order = 2)]
        public decimal Amount { get; set; }
    }

    [DataContract(Name = "payer", Namespace = "")]
    [XmlRoot("payer", Namespace = "")]
    public class Payer
    {
        [DataMember(Name = "companyName", Order = 1)]
        [XmlElement("companyName", IsNullable = true, Order = 1)]
        public string CompanyName { get; set; }

        [DataMember(Name = "title", Order = 2)]
        [XmlElement("title", IsNullable = true, Order = 2)]
        public string Title { get; set; }

        [DataMember(Name = "firstNames", Order = 3)]
        [XmlElement("firstNames", IsNullable = true, Order = 3)]
        public string FirstNames { get; set; }

        [DataMember(Name = "lastName", Order = 4)]
        [XmlElement("lastName", IsNullable = true, Order = 4)]
        public string LastName { get; set; }

        [DataMember(Name = "dateOfBirth", Order = 5)]
        [XmlElement("dateOfBirth", IsNullable = true, Order = 5)]
        public DateTime? DateOfBirth { get; set; }

        [DataMember(Name = "telephoneHome", Order = 6)]
        [XmlElement("telephoneHome", IsNullable = true, Order = 6)]
        public string TelephoneHome { get; set; }

        [DataMember(Name = "telephoneWork", Order = 7)]
        [XmlElement("telephoneWork", IsNullable = true, Order = 7)]
        public string TelephoneWork { get; set; }

        [DataMember(Name = "telephoneMobile", Order = 8)]
        [XmlElement("telephoneMobile", IsNullable = true, Order = 8)]
        public string TelephoneMobile { get; set; }

        [DataMember(Name = "fax", Order = 9)]
        [XmlElement("fax", IsNullable = true, Order = 9)]
        public string Fax { get; set; }

        [DataMember(Name = "email", Order = 10)]
        [XmlElement("email", IsNullable = true, Order = 10)]
        public string Email { get; set; }

        [DataMember(Name = "address1", Order = 11)]
        [XmlElement("address1", IsNullable = true, Order = 11)]
        public string Address1 { get; set; }

        [DataMember(Name = "address2", Order = 12)]
        [XmlElement("address2", IsNullable = true, Order = 12)]
        public string Address2 { get; set; }

        [DataMember(Name = "address3", Order = 13)]
        [XmlElement("address3", IsNullable = true, Order = 13)]
        public string address3 { get; set; }

        [DataMember(Name = "suburb", Order = 14)]
        [XmlElement("suburb", IsNullable = true, Order = 14)]
        public string Suburb { get; set; }

        [DataMember(Name = "city", Order = 15)]
        [XmlElement("city", IsNullable = true, Order = 15)]
        public string City { get; set; }

        [DataMember(Name = "postcode", Order = 16)]
        [XmlElement("postcode", IsNullable = true, Order = 16)]
        public string Postcode { get; set; }

        [DataMember(Name = "state", Order = 17)]
        [XmlElement("state", IsNullable = true, Order = 17)]
        public string State { get; set; }

        [DataMember(Name = "country", Order = 18)]
        [XmlElement("country", IsNullable = true, Order = 18)]
        public string Country { get; set; }
    }

    [DataContract(Name = "card", Namespace = "")]
    [XmlRoot("card", Namespace = "")]
    public class RdCard
    {
        [DataMember(Name = "cardScheme", Order = 1)]
        [XmlElement("cardScheme", IsNullable = true, Order = 1)]
        public string CardScheme { get; set; }

        [DataMember(Name = "cardNumber", Order = 2)]
        [XmlElement("cardNumber", IsNullable = true, Order = 2)]
        public string CardNumber { get; set; }

        [DataMember(Name = "expiryDate", Order = 3)]
        [XmlElement("expiryDate", IsNullable = true, Order = 3)]
        public string ExpiryDate { get; set; }

        [DataMember(Name = "nameOnCard", Order = 4)]
        [XmlElement("nameOnCard", IsNullable = true, Order = 4)]
        public string NameOnCard { get; set; }

        [DataMember(Name = "cardBin", Order = 5)]
        [XmlElement("cardBin", IsNullable = true, Order = 5)]
        public string CardBin { get; set; }

        [DataMember(Name = "cardLastFour", Order = 6)]
        [XmlElement("cardLastFour", IsNullable = true, Order = 6)]
        public string CardLastFour { get; set; }

        [DataMember(Name = "cardTypeId", Order = 7)]
        [XmlElement("cardTypeId", IsNullable = true, Order = 7)]
        public int CardTypeId { get; set; }
    }
    
    [DataContract(Name = "amendments", Namespace = "")]
    [XmlRoot("amendments", Namespace = "")]
    public class Amendments
    {
        [DataMember(Name = "type", Order = 1)]
        [XmlElement("type", IsNullable = true, Order = 1)]
        public int Type { get; set; }

        [DataMember(Name = "date", Order = 2)]
        [XmlElement("date", IsNullable = true, Order = 2)]
        public DateTime Date { get; set; }

        [DataMember(Name = "user", Order = 3)]
        [XmlElement("user", IsNullable = true, Order = 3)]
        public string User { get; set; }

        [DataMember(Name = "comment", Order = 4)]
        [XmlElement("comment", IsNullable = true, Order = 4)]
        public string Comment { get; set; }

        [DataMember(Name = "previousData", Order = 5)]
        [XmlElement("previousData", IsNullable = true, Order = 5)]
        public string PreviousData { get; set; }
    }

    [DataContract(Name = "paymentSchedule", Namespace = "")]
    public class CardPlanPaymentSchedule
    {
        [DataMember(Name = "date", Order = 1)]
        public DateTime Date { get; set; }

        [DataMember(Name = "status", Order = 2)]
        public int Status { get; set; }

        [DataMember(Name = "amount", Order = 3)]
        public decimal Amount { get; set; }

        [DataMember(Name = "skip", Order = 3)]
        public bool Skip { get; set; }

        [DataMember(Name = "retry", Order = 2)]
        public bool Retry { get; set; }

        [DataMember(Name = "paymentId", Order = 2)]
        public int PaymentId { get; set; }

        [DataMember(Name = "originalPaymentId", Order = 2)]
        public int OriginalPaymentId { get; set; }

        [DataMember(Name = "payment", Order = 3)]
        public CardPlanPayment Payment { get; set; }        

    }

    [DataContract(Name = "payment", Namespace = "")]
    public class CardPlanPayment
    {
        [DataMember(Name = "number", Order = 1)]
        public string Number { get; set; }

        [DataMember(Name = "timestamp", Order = 2)]
        public DateTime TimeStamp { get; set; }

        [DataMember(Name = "status", Order = 3)]
        public int Status { get; set; }

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
