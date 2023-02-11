using Example.MAP.Common.API.Contract.Resources.V1;
using Example.Notific.Context.Common;
using Example.Notific.Context.Common.Helpers;
using Example.Notific.TPF.WebApi.Client.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.TPEventHandlers.Mapper
{
    public class CardPaymentMapper
    {
        public static Payment ToResource(TPCardPayment cardPayment)
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
                Timestamp = cardPayment.Transaction.Date,
                Type = cardPayment.Type == 0 ? "" : (Enum.GetName(typeof(PaymentType), cardPayment.Type)).ToLower()
            };

            if (cardPayment.Transaction != null)
            {
                Response response = new Response()
                {
                    AuthCode = cardPayment.Transaction.AuthCode,
                    Code = cardPayment.Transaction.ResponseCode,
                    Message = cardPayment.Transaction.ResponseMessage,
                    ProviderResponse = cardPayment.Transaction.ProviderResponse
                };

                result.Response = response;
            }

            PaymentCardDetails cardDetails = new PaymentCardDetails()
            {
                ExpiryDate = cardPayment.Card.ExpiryDate,
                NameOnCard = cardPayment.Card.NameOnCard,
                Bin = cardPayment.Card.CardBin,
                LastFour = cardPayment.Card.CardLastFour,
                Mask = cardPayment.Card.CardNumber,
                Token = cardPayment.Card.MapToken,
                Type = cardPayment.Card.CardTypeId == 0 ? "" : (Enum.GetName(typeof(CardType), cardPayment.Card.CardTypeId)).ToLower(),
            };

            PaymentMethod method = new PaymentMethod()
            {
                Card = cardDetails
            };

            result.PaymentMethod = method;

            PaymentMerchant merchant = new PaymentMerchant()
            {
                Id = cardPayment.Merchant.Id,
                SubAccountId = cardPayment.Merchant.SubAccountId
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

            if (cardPayment.Geolocation != null)
            {
                PaymentGeolocation geoLocation = new PaymentGeolocation()
                {
                    Latitude = cardPayment.Geolocation.Latitude,
                    Longitude = cardPayment.Geolocation.Longitude
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
                        Timestamp = item.Date,
                        Number = item.Number,
                        Particulars = item.Particulars,
                        Reference = item.Reference,
                        Response = new Response
                        {
                            AuthCode = item.AuthCode,
                            ProviderResponse = item.ProviderResponse,
                            Code = item.ResponseCode,
                            Message = item.ResponseMessage
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
                        Timestamp = item.Date,
                        Number = item.Number,
                        Particulars = item.Particulars,
                        Reference = item.Reference,
                        Response = new Response
                        {
                            AuthCode = item.AuthCode,
                            ProviderResponse = item.ProviderResponse,
                            Code = item.ResponseCode,
                            Message = item.ResponseMessage
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
