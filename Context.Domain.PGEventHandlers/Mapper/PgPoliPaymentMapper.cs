#region using namepsaces..
using Example.Notific.PG.WebApi.Client.Resources;
using Example.MAP.Common.API.Contract.Resources.V1;
using System.Collections.Generic;
using System;
using Example.Notific.Context.Common;
#endregion

namespace Example.Notific.Context.Domain.PGEventHandlers.Mapper
{
    public class PgPoliPaymentMapper
    {
        public static PoliPayment ToResource(PoliPaymentResource poliPaymentResource)
        {
            PoliPayment result = new PoliPayment
            {
                Channel = poliPaymentResource.Service == 0 ? string.Empty : Dictionaries.Channel[poliPaymentResource.Service],
                InitiatedBy = poliPaymentResource.InitiatedBy,
                Number = poliPaymentResource.Number,
                ReceiptRecipient = poliPaymentResource.ReceiptRecipient,
                Status = poliPaymentResource.Status.ToLower(),
                TimestampUtc = poliPaymentResource.TimestampUtc,
                Amount = poliPaymentResource.Transaction.Amount,
                Currency = poliPaymentResource.Transaction.Currency,
                Reference1 = poliPaymentResource.Transaction.Reference1,
                Reference2 = poliPaymentResource.Transaction.Reference2,
                Device = poliPaymentResource.Device != null ? new MAP.Common.API.Contract.Resources.V1.DeviceResource
                {
                    Id = poliPaymentResource.Device.Id,
                    Description = poliPaymentResource.Device.Description
                } : null,
                GeoLocation = poliPaymentResource.GeoLocation != null ? new MAP.Common.API.Contract.Resources.V1.GeoLocationResource
                {
                    Latitude = poliPaymentResource.GeoLocation.Latitude,
                    Longitude = poliPaymentResource.GeoLocation.Longitude
                } : null,
                Merchant = poliPaymentResource.Merchant != null ? new MAP.Common.API.Contract.Resources.V1.MerchantResource
                {
                    Id = poliPaymentResource.Merchant.Id.Value,
                    SubAccount = poliPaymentResource.Merchant.SubAccount.Value

                } : null,
                PoliResponse = GetPoliResponse(poliPaymentResource),
                Response = poliPaymentResource.Response != null ? new Example.MAP.Common.API.Contract.Resources.V1.ResponseResource
                {
                    Code = poliPaymentResource.Response.Code,
                    Message = poliPaymentResource.Response.Message

                } : null
            };
            return result;
        }
        #region private methods
        private static Example.MAP.Common.API.Contract.Resources.V1.PoliResponse GetPoliResponse(PoliPaymentResource poliPaymentResource)
        {
            if (poliPaymentResource.PoliResponse != null)
            {
                return new PoliResponse
                {
                    AmountPaid = poliPaymentResource.PoliResponse.AmountPaid,
                    BankReceipt = poliPaymentResource.PoliResponse.BankReceipt,
                    BankReceiptTimestamp = poliPaymentResource.PoliResponse.BankReceiptTimestamp,
                    CountryCode = poliPaymentResource.PoliResponse.CountryCode,
                    CountryName = poliPaymentResource.PoliResponse.CountryName,
                    FiCode = poliPaymentResource.PoliResponse.FiCode,
                    FiCountryCode = poliPaymentResource.PoliResponse.FiCountryCode,
                    FiInstitutionName = poliPaymentResource.PoliResponse.FiInstitutionName,
                    InitiateRequestRecievedTimestamp = poliPaymentResource.PoliResponse.InitiateRequestRecievedTimestamp,
                    MerchantAccountName = poliPaymentResource.PoliResponse.MerchantAccountName,
                    MerchantAccountNumber = poliPaymentResource.PoliResponse.MerchantAccountNumber,
                    MerchantAccountSortCode = poliPaymentResource.PoliResponse.MerchantAccountSortCode,
                    MerchantAccountSuffix = poliPaymentResource.PoliResponse.MerchantAccountSuffix,
                    MerchantData = poliPaymentResource.PoliResponse.MerchantData,
                    MerchantReference = poliPaymentResource.PoliResponse.MerchantReference,
                    NavigateUrl = poliPaymentResource.PoliResponse.NavigateUrl,
                    PayerAccountNumber = poliPaymentResource.PoliResponse.PayerAccountNumber,
                    PayerAccountSortCode = poliPaymentResource.PoliResponse.PayerAccountSortCode,
                    PayerAccountSuffix = poliPaymentResource.PoliResponse.PayerAccountSuffix,
                    PayerFamilyName = poliPaymentResource.PoliResponse.PayerFamilyName,
                    PayerFirstName = poliPaymentResource.PoliResponse.PayerFirstName,
                    PaymentEndTimestamp = poliPaymentResource.PoliResponse.PaymentEndTimestamp,
                    PaymentStartTimestamp = poliPaymentResource.PoliResponse.PaymentStartTimestamp,
                    TransactionId = poliPaymentResource.PoliResponse.TransactionId
                };
            }
            else
                return null;
        }
        #endregion
    }
}
