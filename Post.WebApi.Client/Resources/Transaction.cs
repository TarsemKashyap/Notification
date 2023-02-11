using Example.Notific.Context.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace Example.Notific.PG.WebApi.Client.Resources
{
    [DataContract(Name = "transaction")]
    [XmlRoot("transaction")]
    public class Transaction 
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

        [DataMember(Name = "subAccountId", Order = 5)]
        [XmlElement("subAccountId", IsNullable = false, Order = 5)]
        public int SubAccountId { get; set; }

        [DataMember(Name = "service", Order = 6)]
        [XmlElement("service", IsNullable = false, Order = 6)]
        public PaymentService Service { get; set; }

        [DataMember(Name = "status", Order = 7)]
        [XmlElement("status", IsNullable = true, Order = 7)]
        public PaymentTransactionStatus Status { get; set; }

        [DataMember(Name = "settlementDate", Order = 8)]
        [XmlElement("settlementDate", IsNullable = false, Order = 8)]
        public DateTime? SettlementDate { get; set; }

        [DataMember(Name = "parentTransactionNumber", Order = 9)]
        [XmlElement("parentTransactionNumber", IsNullable = false, Order = 9)]
        public string ParentTransactionNumber { get; set; }

        [DataMember(Name = "isRecurring", Order = 10)]
        [XmlElement("isRecurring", IsNullable = false, Order = 10)]
        public bool? IsRecurring { get; set; }


        [DataMember(Name = "amount", Order = 11)]
        [XmlElement("amount", IsNullable = false, Order = 11)]
        public decimal Amount { get; set; }

        [DataMember(Name = "currency", Order = 12)]
        [XmlElement("currency", IsNullable = false, Order = 12)]
        public string Currency { get; set; }


        //[DataMember(Name = "callerTxnReference", Order = 13)]
        //[XmlElement("callerTxnReference", IsNullable = false, Order = 13)]
        //public string CallerTxnReference { get; set; }

        [DataMember(Name = "reference", Order = 14)]
        [XmlElement("reference", IsNullable = false, Order = 14)]
        public string Reference { get; set; }

        [DataMember(Name = "particular", Order = 15)]
        [XmlElement("particular", IsNullable = false, Order = 15)]
        public string Particular { get; set; }

        [DataMember(Name = "merchantTransactionReference", Order = 16)]
        [XmlElement("merchantTransactionReference", IsNullable = false, Order = 16)]
        public string MerchantTransactionReference { get; set; }

        [DataMember(Name = "gatewayRequestCallerReference", Order = 17)]
        [XmlElement("gatewayRequestCallerReference", IsNullable = false, Order = 17)]
        public string GatewayRequestCallerReference { get; set; }

        [DataMember(Name = "checkoutId", Order = 18)]
        [XmlElement("checkoutId", IsNullable = false, Order = 18)]
        public string CheckoutId { get; set; }

        [DataMember(Name = "ipAddress", Order = 19)]
        [XmlElement("ipAddress", IsNullable = false, Order = 19)]
        public string IpAddress { get; set; }

        [DataMember(Name = "callerId", Order = 20)]
        [XmlElement("callerId", IsNullable = false, Order = 20)]
        public string CallerId { get; set; }

        [DataMember(Name = "initiatedBy", Order = 21)]
        [XmlElement("initiatedBy", IsNullable = false, Order = 21)]
        public string InitiatedBy { get; set; }


        [DataMember(Name = "gatewayCode", Order = 22)]
        [XmlElement("gatewayCode", IsNullable = false, Order = 22)]
        public char GatewayCode { get; set; }

        [DataMember(Name = "gatewayId", Order = 23)]
        [XmlElement("gatewayId", IsNullable = false, Order = 23)]
        public Guid? GatewayId { get; set; }

        [DataMember(Name = "gatewayRequestId", Order = 24)]
        [XmlElement("gatewayRequestId", IsNullable = false, Order = 24)]
        public long GatewayRequestId { get; set; }

        [DataMember(Name = "gatewayResponseId", Order = 25)]
        [XmlElement("gatewayResponseId", IsNullable = true, Order = 25)]
        public long? GatewayResponseId { get; set; }


        [DataMember(Name = "acqResponseCode", Order = 26)]
        [XmlElement("acqResponseCode", IsNullable = false, Order = 26)]
        public string AcqResponseCode { get; set; }

        [DataMember(Name = "authorizeId", Order = 27)]
        [XmlElement("authorizeId", IsNullable = false, Order = 27)]
        public string AuthorizeId { get; set; }

        [DataMember(Name = "settlementBatchNo", Order = 28)]
        [XmlElement("settlementBatchNo", IsNullable = false, Order = 28)]
        public string SettlementBatchNo { get; set; }

        [DataMember(Name = "threeDSecureAuthId", Order = 29)]
        [XmlElement("threeDSecureAuthId", IsNullable = false, Order = 29)]
        public long? ThreeDSecureAuthId { get; set; }


        [DataMember(Name = "deviceId", Order = 30)]
        [XmlElement("deviceId", IsNullable = false, Order = 30)]
        public string DeviceId { get; set; }

        [DataMember(Name = "deviceDescription", Order = 31)]
        [XmlElement("deviceDescription", IsNullable = false, Order = 31)]
        public string DeviceDescription { get; set; }


        [DataMember(Name = "latitude", Order = 32)]
        [XmlElement("latitude", IsNullable = false, Order = 32)]
        public string Latitude { get; set; }

        [DataMember(Name = "longitude", Order = 33)]
        [XmlElement("longitude", IsNullable = false, Order = 33)]
        public string Longitude { get; set; }


        [DataMember(Name = "customerMobileNumber", Order = 34)]
        [XmlElement("customerMobileNumber", IsNullable = false, Order = 34)]
        public string CustomerMobileNumber { get; set; }

        [DataMember(Name = "customerEmail", Order = 35)]
        [XmlElement("customerEmail", IsNullable = false, Order = 35)]
        public string CustomerEmail { get; set; }


        [DataMember(Name = "responseCode", Order = 36)]
        [XmlElement("responseCode", IsNullable = true, Order = 36)]
        public int? ResponseCode { get; set; }

        [DataMember(Name = "responseMessage", Order = 37)]
        [XmlElement("responseMessage", IsNullable = true, Order = 37)]
        public string ResponseMessage { get; set; }

        [DataMember(Name = "error", Order = 38)]
        [XmlElement("error", IsNullable = false, Order = 38)]
        public string Error { get; set; }


        [DataMember(Name = "cardToken", Order = 39)]
        [XmlElement("cardToken", IsNullable = false, Order = 39)]
        public string CardToken { get; set; }

        [DataMember(Name = "cardMask", Order = 40)]
        [XmlElement("cardMask", IsNullable = false, Order = 40)]
        public string CardMask { get; set; }

        [DataMember(Name = "ardExpiry", Order = 41)]
        [XmlElement("ardExpiry", IsNullable = false, Order = 41)]
        public string CardExpiry { get; set; }

        [DataMember(Name = "cardTypeId", Order = 42)]
        [XmlElement("cardTypeId", IsNullable = false, Order = 42)]
        public CardType CardTypeId { get; set; }

        [DataMember(Name = "cardName", Order = 43)]
        [XmlElement("cardName", IsNullable = false, Order = 43)]
        public string CardName { get; set; }

        [DataMember(Name = "cardHash", Order = 44)]
        [XmlElement("cardHash", IsNullable = false, Order = 44)]
        public string CardHash { get; set; }

        [DataMember(Name = "salt", Order = 45)]
        [XmlElement("salt", IsNullable = false, Order = 45)]
        public List<byte> Salt { get; set; }

        [DataMember(Name = "cardBIN", Order = 46)]
        [XmlElement("cardBIN", IsNullable = false, Order = 46)]
        public string CardBIN { get; set; }

        [DataMember(Name = "cardLastFour", Order = 47)]
        [XmlElement("cardLastFour", IsNullable = false, Order = 47)]
        public string CardLastFour { get; set; }

        [DataMember(Name = "expiryPassed", Order = 48)]
        [XmlElement("expiryPassed", IsNullable = false, Order = 48)]
        public bool? ExpiryPassed { get; set; }


        [DataMember(Name = "mnsEntropy", Order = 49)]
        [XmlElement("mnsEntropy", IsNullable = false, Order = 49)]
        public string MnsEntropy { get; set; }

        [DataMember(Name = "legacyResponseMessage", Order = 50)]
        [XmlElement("legacyResponseMessage", IsNullable = true, Order = 50)]
        public string LegacyResponseMessage { get; set; }

        [DataMember(Name = "legacyReceiptNumber", Order = 51)]
        [XmlElement("legacyReceiptNumber", IsNullable = false, Order = 51)]
        public long? LegacyReceiptNumber { get; set; }

        //[DataMember(Name = "createdBy", Order = 52)]
        //[XmlElement("createdBy", IsNullable = false, Order = 52)]
        //public string CreatedBy { get; set; }

        //[DataMember(Name = "updateUrl", Order = 5)]
        //[XmlElement("updateUrl", IsNullable = false, Order = 5)]
        //public string UpdateUrl { get; set; }

        //public Transaction() : base() { }
    }
}
