using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Example.Notific.TPF.WebApi.Client.Resources
{
    [DataContract(Name = "ddPayment", Namespace = "")]
    [XmlRoot("ddPayment", Namespace = "")]
    public class TPDDPayment
    {
        [DataMember(Name = "number", Order = 1)]
        [XmlElement("number", IsNullable = true, Order = 1)]
        public string Number { get; set; }

        [DataMember(Name = "dueDate", Order = 2)]
        [XmlElement("dueDate", IsNullable = true, Order = 2)]
        public DateTime DueDate { get; set; }

        [DataMember(Name = "type", Order = 3)]
        [XmlElement("type", IsNullable = true, Order = 3)]
        public int Type { get; set; }

        [DataMember(Name = "subType", Order = 4)]
        [XmlElement("subType", IsNullable = true, Order = 4)]
        public string SubType { get; set; }

        [DataMember(Name = "status", Order = 5)]
        [XmlElement("status", IsNullable = true, Order = 5)]
        public int Status { get; set; }

        [DataMember(Name = "merchant", Order = 6)]
        [XmlElement("merchant", IsNullable = true, Order = 6)]
        public MerchantDetails Merchant { get; set; }

        [DataMember(Name = "sourceAccount", Order = 7)]
        [XmlElement("sourceAccount", IsNullable = true, Order = 7)]
        public SourceAccount SourceAccount { get; set; }       

        [DataMember(Name = "destinationAccount", Order = 8)]
        [XmlElement("destinationAccount", IsNullable = true, Order = 8)]
        public DestinationAccount DestinationAccount { get; set; }

        [DataMember(Name = "transaction", Order = 9)]
        [XmlElement("transaction", IsNullable = true, Order = 9)]
        public TransactionDetails Transaction { get; set; }

        [DataMember(Name = "plan", Order = 10)]
        [XmlElement("plan", IsNullable = true, Order = 10)]
        public Plan Plan { get; set; }

    }

    [DataContract(Name = "merchant", Namespace = "")]
    [XmlRoot("merchant", Namespace = "")]
    public class MerchantDetails
    {
        [DataMember(Name = "id", Order = 1)]
        [XmlElement("id", IsNullable = true, Order = 1)]
        public int Id { get; set; }
    }

    [DataContract(Name = "transaction", Namespace = "")]
    [XmlRoot("transaction", Namespace = "")]
    public class TransactionDetails
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

        [DataMember(Name = "code", Order = 6)]
        [XmlElement("code", IsNullable = true, Order = 6)]
        public string Code { get; set; }

        [DataMember(Name = "errorCode", Order = 7)]
        [XmlElement("errorCode", IsNullable = true, Order = 7)]
        public string ErrorCode { get; set; }

        [DataMember(Name = "responseCode", Order = 8)]
        [XmlElement("responseCode", IsNullable = true, Order = 8)]
        public int ResponseCode { get; set; }

        [DataMember(Name = "responseMessage", Order = 9)]
        [XmlElement("responseMessage", IsNullable = true, Order = 9)]
        public string ResponseMessage { get; set; }

        [DataMember(Name = "provider", Order = 10)]
        [XmlElement("provider", IsNullable = true, Order = 10)]
        public string Provider { get; set; }

        [DataMember(Name = "settlementTransactionNumber", Order = 11)]
        [XmlElement("settlementTransactionNumber", IsNullable = true, Order = 11)]
        public string SettlementTransactionNumber { get; set; }

        [DataMember(Name = "settlementDate", Order = 12)]
        [XmlElement("settlementDate", IsNullable = true, Order = 12)]
        public DateTime? SettlementDate { get; set; }
    }

    [DataContract(Name = "sourceAccount", Namespace = "")]
    [XmlRoot("sourceAccount", Namespace = "")]
    public class SourceAccount
    {
        [DataMember(Name = "name", Order = 1)]
        [XmlElement("name", IsNullable = true, Order = 1)]
        public string Name { get; set; }

        [DataMember(Name = "number", Order = 2)]
        [XmlElement("number", IsNullable = true, Order = 2)]
        public string Number { get; set; }
    }

    [DataContract(Name = "destinationAccount", Namespace = "")]
    [XmlRoot("destinationAccount", Namespace = "")]
    public class DestinationAccount
    {
        [DataMember(Name = "name", Order = 1)]
        [XmlElement("name", IsNullable = true, Order = 1)]
        public string Name { get; set; }

        [DataMember(Name = "number", Order = 2)]
        [XmlElement("number", IsNullable = true, Order = 2)]
        public string Number { get; set; }
    }

    [DataContract(Name = "plan", Namespace = "")]
    [XmlRoot("plan", Namespace = "")]
    public class Plan
    {
        [DataMember(Name = "id", Order = 1)]
        [XmlElement("id", IsNullable = true, Order = 1)]
        public int? Id { get; set; }
    }

}
