using Example.MAP.Common.API.Contract.Resources.V1;
using Example.Notific.Context.Common;
using Example.Notific.PG.WebApi.Client.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.PGEventHandlers.Mapper
{
    public class PgCardPaymentMapper
    {
        public static Payment ToResource(PaymentResource cardPayment)
        {
            Payment result = new Payment()
            {
                Amount = cardPayment.Transaction.Amount,
                AmountCaptured = cardPayment.Transaction.AmountCaptured,
                AmountRefunded = cardPayment.Transaction.AmountRefunded,
                Channel = cardPayment.Service == 0 ? "" : Dictionaries.Channel[cardPayment.Service],
                Currency = cardPayment.Transaction.Currency,
                InitiatedBy = cardPayment.InitiatedBy,
                Number = cardPayment.Number,
                Particulars = cardPayment.Transaction.Particulars,
                ReceiptRecipient = cardPayment.ReceiptRecipient,
                Reference = cardPayment.Transaction.Reference,
                Status = cardPayment.Status == 0 ? "" : (Enum.GetName(typeof(PaymentStatus), cardPayment.Status)).ToLower(),
                Timestamp = Convert.ToDateTime(cardPayment.Timestamp),
                Type = cardPayment.Type == 0 ? "" : (Enum.GetName(typeof(PaymentType), cardPayment.Type)).ToLower()
            };

            if (cardPayment.Transaction != null)
            {
                Response response = new Response()
                {
                    AuthCode = cardPayment.Transaction.AuthCode,
                    Code = Convert.ToInt32(cardPayment.Response.Code),
                    Message = cardPayment.Response.Message,
                    ProviderResponse = cardPayment.Transaction.ProviderResponse
                };

                result.Response = response;
            }

            PaymentCardDetails cardDetails = new PaymentCardDetails()
            {
                ExpiryDate = cardPayment.PaymentMethod.Card.ExpiryDate,
                NameOnCard = cardPayment.PaymentMethod.Card.NameOnCard,
                Bin = cardPayment.PaymentMethod.Card.CardBin,
                LastFour = cardPayment.PaymentMethod.Card.CardLastFour,
                Mask = cardPayment.PaymentMethod.Card.Number,
                Token = cardPayment.PaymentMethod.Card.MapToken,
                Type = cardPayment.PaymentMethod.Card.CardScheme,
            };

            PaymentMethod method = new PaymentMethod()
            {
                Card = cardDetails
            };

            result.PaymentMethod = method;

            PaymentMerchant merchant = new PaymentMerchant()
            {
                Id = cardPayment.Merchant.Id.HasValue ? cardPayment.Merchant.Id.Value : 0,
                SubAccountId = cardPayment.Merchant.SubAccount.HasValue ? cardPayment.Merchant.SubAccount.Value : 0
            };

            result.Merchant = merchant;

            if (cardPayment.Device != null)
            {
                PaymentDevice device = new PaymentDevice()
                {
                    Description = cardPayment.Device.Description,
                    Id = cardPayment.Device.Id
                };

                result.Device = device;
            }

            if (cardPayment.GeoLocation != null)
            {
                PaymentGeolocation geoLocation = new PaymentGeolocation()
                {
                    Latitude = cardPayment.GeoLocation.Latitude,
                    Longitude = cardPayment.GeoLocation.Longitude
                };

                result.Geolocation = geoLocation;
            }

            if (cardPayment.Refunds != null)
            {
                List<PaymentRefunds> refundList = new List<PaymentRefunds>();

                foreach (var item in cardPayment.Refunds)
                {
                    PaymentRefunds refund = new PaymentRefunds()
                    {
                        Amount = item.Amount,

                        Currency = item.Currency,
                        Timestamp = Convert.ToDateTime(item.Timestamp),
                        Number = item.Number,
                        Particulars = item.Particulars,
                        Reference = item.Reference,
                        Response = new Response
                        {
                            AuthCode = item.AuthCode,
                            ProviderResponse = item.ProviderResponse,
                            Code = item.ResponseCode.HasValue ? item.ResponseCode.Value : 0,
                            Message = item.ResponseMessage,
                        }
                    };

                    refundList.Add(refund);
                }

                result.Refunds = refundList.ToArray();
            }

            if (cardPayment.Captures != null)
            {
                List<PaymentCaptures> captureList = new List<PaymentCaptures>();

                foreach (var item in cardPayment.Captures)
                {
                    PaymentCaptures refund = new PaymentCaptures()
                    {
                        Amount = item.Amount,

                        Currency = item.Currency,
                        Timestamp = Convert.ToDateTime(item.Timestamp),
                        Number = item.Number,
                        Particulars = item.Particulars,

                        Reference = item.Reference,

                        Response = new Response
                        {
                            Code = item.ResponseCode.HasValue ? item.ResponseCode.Value : 0,
                            Message = item.ResponseMessage,
                            ProviderResponse = item.ProviderResponse,
                            AuthCode = item.AuthCode,
                        }
                    };

                    captureList.Add(refund);
                }

                result.Captures = captureList.ToArray();
            }

            return result;
        }
    }
}
