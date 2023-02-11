using Example.Notific.Context.Common;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "transactionSummary")]
    [XmlRoot("transactionSummary")]
    public class TransactionSummary 
    {
        [DataMember(Name = "transactionId", Order = 1)]
        [XmlElement("transactionId", IsNullable = true, Order = 1)]
        public long TransactionId { get; set; }

        [DataMember(Name = "number", Order = 2)]
        [XmlElement("number", IsNullable = true, Order = 2)]
        public string Number { get; set; }

        [DataMember(Name = "date", Order = 3)]
        [XmlElement("date", IsNullable = true, Order = 3)]
        public DateTime? Date { get; set; }

        [DataMember(Name = "type", Order = 4)]
        [XmlElement("type", IsNullable = false, Order = 4)]
        public TransactionType Type { get; set; }

        [DataMember(Name = "status", Order = 5)]
        [XmlElement("status", IsNullable = false, Order = 5)]
        public PaymentTransactionStatus Status { get; set; }


        [DataMember(Name = "amount", Order = 6)]
        [XmlElement("amount", IsNullable = false, Order = 6)]
        public decimal Amount { get; set; }

        [DataMember(Name = "currency", Order = 7)]
        [XmlElement("currency", IsNullable = false, Order = 7)]
        public string Currency { get; set; }


        [DataMember(Name = "callerTxnReference", Order = 8)]
        [XmlElement("callerTxnReference", IsNullable = false, Order = 8)]
        public string CallerTxnReference { get; set; }


        [DataMember(Name = "responseCode", Order = 9)]
        [XmlElement("responseCode", IsNullable = true, Order = 9)]
        public int? ResponseCode { get; set; }

        [DataMember(Name = "responseMessage", Order = 10)]
        [XmlElement("responseMessage", IsNullable = true, Order = 10)]
        public string ResponseMessage { get; set; }


        [DataMember(Name = "legacyResponseMessage", Order = 11)]
        [XmlElement("legacyResponseMessage", IsNullable = true, Order = 11)]
        public string LegacyResponseMessage { get; set; }

        [DataMember(Name = "legacyReceiptNumber", Order = 12)]
        [XmlElement("legacyReceiptNumber", IsNullable = false, Order = 12)]
        public long? LegacyReceiptNumber { get; set; }

        [DataMember(Name = "settlementDate", Order = 13)]
        [XmlElement("settlementDate", IsNullable = false, Order = 13)]
        public DateTime? SettlementDate { get; set; }

        [DataMember(Name = "settlementBatchNo", Order = 14)]
        [XmlElement("settlementBatchNo", IsNullable = false, Order = 14)]
        public string SettlementBatchNo { get; set; }

    }
}
