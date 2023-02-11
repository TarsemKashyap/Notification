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
    public class CardPlanMapper
    {
        public static CardPlan ToResource(TPRecurringCardPlan cardPlan)
        {
            CardPlan result = new CardPlan()
            {
                AmendmentDate = cardPlan.AmendmentDate,
                Amount = cardPlan.Amount,
                Created = cardPlan.Created,
                Frequency = cardPlan.Frequency == 0 ? "" : EnumExtensions.GetString((CardPlanFrequency)cardPlan.Frequency),
                Reference = cardPlan.Reference,
                StartDate = cardPlan.StartDate.ToString("yyyy-MM-dd"),
                Status = cardPlan.Status == 0 ? "" : (Enum.GetName(typeof(CardPlanStatus), cardPlan.Status)).ToLower(),
                Id = cardPlan.Id,
                InstalmentFailOption = cardPlan.InstalmentFailOption == 0 ? "" : EnumExtensions.GetString((CardPlanInstalementOptions)cardPlan.InstalmentFailOption),
                Particulars = cardPlan.Particulars,
                StatusChangedDate = cardPlan.StatusChangedDate,
                TotalAmount = cardPlan.TotalAmount,
                Type = cardPlan.Type == 0 ? "" : EnumExtensions.GetString((CardPlanType)cardPlan.Type),
                NextPaymentDate = cardPlan.NextPaymentDate.HasValue ? cardPlan.NextPaymentDate.Value.ToString("yyyy-MM-dd") : null
            };

            if (cardPlan.RetryPreferences != null)
            {
                RecurringRetryPreferences retryReferences = new RecurringRetryPreferences()
                {
                    FrequencyInDays = cardPlan.RetryPreferences.FrequencyInDays,
                    MaxAttempts = cardPlan.RetryPreferences.MaxAttempts,
                    Perform = cardPlan.RetryPreferences.Perform
                };

                result.RetryPreferences = retryReferences;
            }

            RecurringCard card = new RecurringCard()
            {
                Bin = cardPlan.Card.CardBin,
                ExpiryDate = cardPlan.Card.ExpiryDate,
                LastFour = cardPlan.Card.CardLastFour,
                Mask = cardPlan.Card.CardNumber,
                NameOnCard = cardPlan.Card.NameOnCard,
                Type = cardPlan.Card.CardTypeId == 0 ? "" : EnumExtensions.GetString((CardType)cardPlan.Card.CardTypeId)
            };

            result.Card = card;

            if (cardPlan.Payer != null)
            {
                RecurringPayer payer = new RecurringPayer()
                {
                    Address1 = cardPlan.Payer.Address1,
                    Address2 = cardPlan.Payer.Address2,
                    Address3 = cardPlan.Payer.address3,
                    City = cardPlan.Payer.City,
                    CompanyName = cardPlan.Payer.CompanyName,
                    Country = cardPlan.Payer.Country,
                    DateOfBirth = cardPlan.Payer.DateOfBirth.HasValue ? cardPlan.Payer.DateOfBirth.Value.ToString("yyyy-MM-dd") : null,
                    Email = cardPlan.Payer.Email,
                    Fax = cardPlan.Payer.Fax,
                    FirstNames = cardPlan.Payer.FirstNames,
                    LastName = cardPlan.Payer.LastName,
                    Postcode = cardPlan.Payer.Postcode,
                    Suburb = cardPlan.Payer.Suburb,
                    TelephoneHome = cardPlan.Payer.TelephoneHome,
                    TelephoneMobile = cardPlan.Payer.TelephoneMobile,
                    TelephoneWork = cardPlan.Payer.TelephoneWork,
                    Title = cardPlan.Payer.Title,
                    State = cardPlan.Payer.State
                };

                result.Payer = payer;
            }

            RecurringMerchant merchant = new RecurringMerchant()
            {
                Id = cardPlan.Merchant.Id,
                SubAccount = cardPlan.Merchant.SubAccountId
            };

            result.Merchant = merchant;

            if (cardPlan.InitialPayment != null)
            {
                RecurringInitialPayment initialPayment = new RecurringInitialPayment()
                {
                    Amount = cardPlan.InitialPayment.Amount,
                    Date = cardPlan.InitialPayment.Date.ToString("yyyy-MM-dd")
                };
                result.InitialPayment = initialPayment;
            }

            if (cardPlan.Amendments != null)
            {
                List<RecurringAmendment> amendmentList = new List<RecurringAmendment>();

                foreach (var item in cardPlan.Amendments)
                {
                    RecurringAmendment amendment = new RecurringAmendment()
                    {
                        Comment = item.Comment,
                        Date = item.Date,
                        User = item.User,
                        Type = item.Type == 0 ? "" : (Enum.GetName(typeof(CardPlanAmendmentType), item.Type)).ToLower(),
                        PreviousData = item.PreviousData
                    };

                    amendmentList.Add(amendment);
                }

                result.Amendments = amendmentList.ToArray();
            }

            List<RecurringCardPlanPaymentSchedule> listSchedule = new List<RecurringCardPlanPaymentSchedule>();

            if (cardPlan.PaymentSchedule != null)
            {
                // Find the Payment Schdeule without Retry

                var paymentWithoutRetry = cardPlan.PaymentSchedule.Where(i => i.Retry == false).OrderBy(i => i.Date).ToList();

                foreach (var item in paymentWithoutRetry)
                {
                    var objPaymentSchedule = new RecurringCardPlanPaymentSchedule();

                    objPaymentSchedule.Amount = item.Amount;
                    objPaymentSchedule.Date = item.Date.ToString("yyyy-MM-dd");
                    objPaymentSchedule.Skip = item.Skip;
                    objPaymentSchedule.Status = item.Status == 0 ? "" : (Enum.GetName(typeof(PlanScheduleStatus), item.Status)).ToLower();

                    if (item.Payment != null)
                    {
                        var objPayment = new RecurringCardPlanPayment();

                        objPayment.Number = item.Payment.Number;
                        objPayment.Status = item.Payment.Status == 0 ? "" : (Enum.GetName(typeof(PaymentStatus), item.Payment.Status)).ToLower();
                        objPayment.TimeStamp = item.Payment.TimeStamp;

                        var objResponse = new Response();

                        objResponse.AuthCode = item.Payment.AuthCode;
                        objResponse.Code = item.Payment.ResponseCode;
                        objResponse.Message = item.Payment.ResponseMessage;
                        objResponse.ProviderResponse = item.Payment.ProviderResponse;

                        objPayment.Response = objResponse;
                        objPaymentSchedule.Payment = objPayment;

                    }

                    //Try to find its Retries
                    var listRetry = new List<RecurringCardPlanPaymentRetries>();

                    var paymentWithRetry = cardPlan.PaymentSchedule.Where(i => i.Retry == true && i.OriginalPaymentId == item.PaymentId).OrderBy(i => i.Date).ToList();

                    foreach (var itemRetry in paymentWithRetry)
                    {
                        var objPaymentScheduleRetry = new RecurringCardPlanPaymentRetries();

                        objPaymentScheduleRetry.Amount = itemRetry.Amount;
                        objPaymentScheduleRetry.Date = itemRetry.Date.ToString("yyyy-MM-dd");
                        objPaymentScheduleRetry.Skip = itemRetry.Skip;

                        objPaymentScheduleRetry.Status = itemRetry.Status == 0 ? "" : (Enum.GetName(typeof(PlanScheduleStatus), itemRetry.Status)).ToLower();

                        if (itemRetry.Payment != null)
                        {
                            var objPaymentRetry = new RecurringCardPlanPayment();

                            objPaymentRetry.Number = itemRetry.Payment.Number;
                            objPaymentRetry.Status = itemRetry.Payment.Status == 0 ? "" : (Enum.GetName(typeof(PaymentStatus), itemRetry.Payment.Status)).ToLower();
                            objPaymentRetry.TimeStamp = itemRetry.Payment.TimeStamp;

                            var objResponseRetry = new Response();

                            objResponseRetry.AuthCode = itemRetry.Payment.AuthCode;
                            objResponseRetry.Code = itemRetry.Payment.ResponseCode;
                            objResponseRetry.Message = itemRetry.Payment.ResponseMessage;
                            objResponseRetry.ProviderResponse = itemRetry.Payment.ProviderResponse;

                            objPaymentRetry.Response = objResponseRetry;

                            objPaymentScheduleRetry.Payment = objPaymentRetry;

                            listRetry.Add(objPaymentScheduleRetry);
                        }
                    }

                    objPaymentSchedule.Retries = listRetry.ToArray();

                    listSchedule.Add(objPaymentSchedule);

                }

            }

            result.PaymentSchedule = listSchedule.ToArray();

            return result;
        }
    }
}
