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
    public class TPRecurringDDPlan
    {
        [DataMember(Name = "id", Order = 1)]
        [XmlElement("id", IsNullable = true, Order = 1)]
        public int Id { get; set; }

        [DataMember(Name = "created", Order = 2)]
        [XmlElement("created", IsNullable = true, Order = 2)]
        public DateTime Created { get; set; }

        [DataMember(Name = "approvalDate", Order = 3)]
        [XmlElement("approvalDate", IsNullable = true, Order = 3)]
        public DateTime? ApprovalDate { get; set; }

        [DataMember(Name = "startDate", Order = 4)]
        [XmlElement("startDate", IsNullable = true, Order = 4)]
        public DateTime StartDate { get; set; }

        [DataMember(Name = "amendmentDate", Order = 5)]
        [XmlElement("amendmentDate", IsNullable = true, Order = 5)]
        public DateTime? AmendmentDate { get; set; }

        [DataMember(Name = "nextPaymentDate", Order = 4)]
        [XmlElement("nextPaymentDate", IsNullable = true, Order = 4)]
        public DateTime? NextPaymentDate { get; set; }

        [DataMember(Name = "type", Order = 6)]
        [XmlElement("type", IsNullable = true, Order = 6)]
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
        [XmlElement("reference", IsNullable = true, Order = 12)]
        public string Reference { get; set; }

        [DataMember(Name = "particulars", Order = 13)]
        [XmlElement("particulars", IsNullable = true, Order = 13)]
        public string Particulars { get; set; }

        [DataMember(Name = "merchantReference1", Order = 8)]
        [XmlElement("merchantReference1", IsNullable = false, Order = 8)]
        public string MerchantReference1 { get; set; }

        [DataMember(Name = "merchantReference2", Order = 9)]
        [XmlElement("merchantReference2", IsNullable = false, Order = 9)]
        public string MerchantReference2 { get; set; }

        [DataMember(Name = "merchantReference3", Order = 10)]
        [XmlElement("merchantReference3", IsNullable = false, Order = 10)]
        public string MerchantReference3 { get; set; }

        [DataMember(Name = "instalmentFailOption", Order = 14)]
        [XmlElement("instalmentFailOption", IsNullable = true, Order = 14)]
        public int InstalmentFailOption { get; set; }        

        [DataMember(Name = "merchant", Order = 15)]
        [XmlElement("merchant", IsNullable = true, Order = 15)]
        public MerchantDetails Merchant { get; set; }

        [DataMember(Name = "initialPayment", Order = 16)]
        [XmlElement("initialPayment", IsNullable = true, Order = 16)]
        public InitialPayment InitialPayment { get; set; }

        [DataMember(Name = "payer", Order = 17)]
        [XmlElement("payer", IsNullable = true, Order = 17)]
        public Payer Payer { get; set; }

        [DataMember(Name = "bankAccount", Order = 18)]
        [XmlElement("bankAccount", IsNullable = true, Order = 18)]
        public BankAccount BankAccount { get; set; }

        [DataMember(Name = "amendments", Order = 20)]
        [XmlElement("amendments", IsNullable = true, Order = 20)]
        public List<Amendments> Amendments { get; set; }

        [DataMember(Name = "paymentSchedule", Order = 19)]
        [XmlElement("paymentSchedule", IsNullable = true, Order = 19)]
        public List<DDPlanPaymentSchedule> PaymentSchedule { get; set; }

    }

    [DataContract(Name = "bankAccount", Namespace = "")]
    [XmlRoot("bankAccount", Namespace = "")]
    public class BankAccount
    {
        [DataMember(Name = "bankName", Order = 1)]
        [XmlElement("bankName", IsNullable = true, Order = 1)]
        public string BankName { get; set; }

        [DataMember(Name = "branchAddress1", Order = 2)]
        [XmlElement("branchAddress1", IsNullable = true, Order = 2)]
        public string BranchAddress1 { get; set; }

        [DataMember(Name = "branchAddress2", Order = 3)]
        [XmlElement("branchAddress2", IsNullable = true, Order = 3)]
        public string BranchAddress2 { get; set; }

        [DataMember(Name = "name", Order = 4)]
        [XmlElement("name", IsNullable = true, Order = 4)]
        public string Name { get; set; }

        [DataMember(Name = "number", Order = 5)]
        [XmlElement("number", IsNullable = true, Order = 5)]
        public string Number { get; set; }
    }

    [DataContract(Name = "paymentSchedule")]
    [XmlRoot("paymentSchedule")]
    public class DDPlanPaymentSchedule
    {

        [DataMember(Name = "paymentDateId")]
        [XmlElement("paymentDateId")]
        public long PaymentDateId { get; set; }

        [DataMember(Name = "date")]
        [XmlElement("date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "status")]
        [XmlElement("status")]
        public int Status { get; set; }

        [DataMember(Name = "amount")]
        [XmlElement("amount")]
        public decimal Amount { get; set; }

        [DataMember(Name = "skip")]
        [XmlElement("skip")]
        public bool Skip { get; set; }

        [DataMember(Name = "directDebit")]
        [XmlElement("directDebit", IsNullable = true)]
        public DDPlanPayment DirectDebit { get; set; }
    }


    [DataContract(Name = "directDebit")]
    [XmlRoot("directDebit")]
    public class DDPlanPayment
    {
        [DataMember(Name = "number", Order = 1)]
        [XmlElement("number", IsNullable = false, Order = 1)]
        public string Number { get; set; }

        [DataMember(Name = "transactionDate", Order = 2)]
        [XmlElement("transactionDate", IsNullable = false, Order = 2)]
        public DateTime TransactionDate { get; set; }

        [DataMember(Name = "settlementDate", Order = 3)]
        [XmlElement("settlementDate", IsNullable = false, Order = 3)]
        public DateTime? SettlementDate { get; set; }

        [DataMember(Name = "status", Order = 4)]
        [XmlElement("status", IsNullable = false, Order = 4)]
        public int Status { get; set; }

        [DataMember(Name = "responseCode", Order = 1)]
        [XmlElement("responseCode", IsNullable = false, Order = 1)]
        public int ResponseCode { get; set; }

        [DataMember(Name = "responseMessage", Order = 1)]
        [XmlElement("responseMessage", IsNullable = false, Order = 1)]
        public string ResponseMessage { get; set; }
    }
}
