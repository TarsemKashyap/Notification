using Example.Notific.Context.Common;
using Example.Notific.Context.Domain.TPEventHandlers.Mapper;
using Example.Notific.TPF.WebApi.Client.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.TPEventHandlers.Test.Mapper
{
    [TestClass]
    public class CardPlanMapper_TestClass
    {
        [TestMethod]
        public void CardPaymentMapper_ToResource()
        {
            TPRecurringCardPlan cardPlan = new TPRecurringCardPlan()
            {
                             Amount = 30,
                Created = Convert.ToDateTime("5/07/2016 5:51:05 p.m."),
                StartDate = Convert.ToDateTime("20/07/2016 12:00:00 a.m."),
                Type = 3,
                Frequency = 7,
                Status = 2,
                StatusChangedDate = Convert.ToDateTime("22/07/2016 1:02:37 p.m."),
                TotalAmount = 0,
                Reference = "898229",
                Particulars = "Oxfam",
                InstalmentFailOption = 0,
                RetryPreferences = new RetryPreferences()
                {
                    FrequencyInDays = 0,
                    MaxAttempts = 0,
                    Perform = false
                },
                Card = new RdCard()
                {
                    CardBin = "543250",
                    CardLastFour = "2441",
                    CardNumber = "12345679",
                    CardScheme = "MasterCard",
                    CardTypeId = 4,
                    ExpiryDate = "0618",
                    NameOnCard = "Scott Samson"
                },
                Id = 87592,
                InitialPayment = new InitialPayment()
                {
                    Amount=10           
                },
                Merchant = new RdMerchant()
                {
                    Id = 23045,
                    SubAccountId = 10366
                },
                Amendments = new List<Amendments>()
                {
                    new Amendments()
                    {
                        Comment="Test",
                        Type=2,
                        User="Test"
                    }
                },
                Payer = new Payer()
                {
                    DateOfBirth = null,
                    CompanyName=null
                }
            };

            var actual = CardPlanMapper.ToResource(cardPlan);

        }

        [TestMethod]
        public void CardPaymentMapper_ToResource_PaymentSchedule()
        {
            TPRecurringCardPlan cardPlan = new TPRecurringCardPlan()
            {
                Amount = 30,
                Created = Convert.ToDateTime("5/07/2016 5:51:05 p.m."),
                StartDate = Convert.ToDateTime("20/07/2016 12:00:00 a.m."),
                Type = 3,
                Frequency = 7,
                Status = 2,
                StatusChangedDate = Convert.ToDateTime("22/07/2016 1:02:37 p.m."),
                TotalAmount = 0,
                Reference = "898229",
                Particulars = "Oxfam",
                InstalmentFailOption = 0,
                RetryPreferences = new RetryPreferences()
                {
                    FrequencyInDays = 0,
                    MaxAttempts = 0,
                    Perform = false
                },
                Card = new RdCard()
                {
                    CardBin = "543250",
                    CardLastFour = "2441",
                    CardNumber = "12345679",
                    CardScheme = "MasterCard",
                    CardTypeId = 4,
                    ExpiryDate = "0618",
                    NameOnCard = "Scott Samson"
                },
                Id = 87592,
                InitialPayment = new InitialPayment()
                {
                    Amount = 10
                },
                Merchant = new RdMerchant()
                {
                    Id = 23045,
                    SubAccountId = 10366
                },
                Amendments = new List<Amendments>()
                {
                    new Amendments()
                    {
                        Comment="Test",
                        Type=2,
                        User="Test"
                    }
                },
                Payer = new Payer()
                {
                    DateOfBirth = null,
                    CompanyName = null
                }
            };

            var list = new List<CardPlanPaymentSchedule>();

            var payment1 = new CardPlanPaymentSchedule()
            {
                Amount = 10,
                Date = DateTime.Now,
                Status = 2,
                Skip = false, 
                OriginalPaymentId=0,
                PaymentId=1,
                Retry=false,             
                Payment = new CardPlanPayment()
                {
                    Number = "P1623526522",
                    ResponseCode = 0,
                    ResponseMessage = "Success",                 
                    Status = 2,
                    AuthCode="1234",
                    ProviderResponse="00",
                    TimeStamp=DateTime.Now                   
                }
            };

            list.Add(payment1);

            var payment2 = new CardPlanPaymentSchedule()
            {
                Amount = 10,
                Date = DateTime.Now,
                Status = 2,
                Skip = false,
                OriginalPaymentId = 0,
                PaymentId = 1,
                Retry = false
            };


            list.Add(payment2);

            cardPlan.PaymentSchedule = list;

            var actual = CardPlanMapper.ToResource(cardPlan);

            Assert.AreEqual(actual.PaymentSchedule[0].Amount, payment1.Amount, "Payment 1 Amount values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Date.ToString(), payment1.Date.ToString(), "Payment 1 Payment date values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Skip, payment1.Skip, "Payment 1 Skip values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Status, (Enum.GetName(typeof(PlanScheduleStatus), payment1.Status)).ToLower(), "Payment 1 Status values are not same");

            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Number, payment1.Payment.Number, "Payment 1 transaction number values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.TimeStamp.ToString(), payment1.Payment.TimeStamp.ToString(), "Payment 1 timestamp values are not same");          
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Status, (Enum.GetName(typeof(PaymentStatus), payment1.Payment.Status)).ToLower(), "Payment 1 transaction status values are not same");

            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Response.Code, payment1.Payment.ResponseCode, "Payment 1 response code values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Response.Message, payment1.Payment.ResponseMessage, "Payment 1 response messages values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Response.AuthCode, payment1.Payment.AuthCode, "Payment 1 auth code values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Response.ProviderResponse, payment1.Payment.ProviderResponse, "Payment 1 provider response values are not same");

            Assert.AreEqual(actual.PaymentSchedule[1].Amount, payment2.Amount, "Payment 2 Amount values are not same");
            Assert.AreEqual(actual.PaymentSchedule[1].Date.ToString(), payment2.Date.ToString(), "Payment 2 Payment date values are not same");
            Assert.AreEqual(actual.PaymentSchedule[1].Skip, payment2.Skip, "Payment 2 Skip values are not same");
            Assert.AreEqual(actual.PaymentSchedule[1].Status, (Enum.GetName(typeof(PlanScheduleStatus), payment2.Status)).ToLower(), "Payment 2 Status values are not same");

            Assert.IsNull(actual.PaymentSchedule[1].Payment, "Payment 2 payment should be NULL");

        }

        [TestMethod]
        public void CardPaymentMapper_ToResource_Retry()
        {
            TPRecurringCardPlan cardPlan = new TPRecurringCardPlan()
            {
                Amount = 30,
                Created = Convert.ToDateTime("5/07/2016 5:51:05 p.m."),
                StartDate = Convert.ToDateTime("20/07/2016 12:00:00 a.m."),
                Type = 3,
                Frequency = 7,
                Status = 2,
                StatusChangedDate = Convert.ToDateTime("22/07/2016 1:02:37 p.m."),
                TotalAmount = 0,
                Reference = "898229",
                Particulars = "Oxfam",
                InstalmentFailOption = 0,
                RetryPreferences = new RetryPreferences()
                {
                    FrequencyInDays = 0,
                    MaxAttempts = 0,
                    Perform = false
                },
                Card = new RdCard()
                {
                    CardBin = "543250",
                    CardLastFour = "2441",
                    CardNumber = "12345679",
                    CardScheme = "MasterCard",
                    CardTypeId = 4,
                    ExpiryDate = "0618",
                    NameOnCard = "Scott Samson"
                },
                Id = 87592,
                InitialPayment = new InitialPayment()
                {
                    Amount = 10
                },
                Merchant = new RdMerchant()
                {
                    Id = 23045,
                    SubAccountId = 10366
                },
                Amendments = new List<Amendments>()
                {
                    new Amendments()
                    {
                        Comment="Test",
                        Type=2,
                        User="Test"
                    }
                },
                Payer = new Payer()
                {
                    DateOfBirth = null,
                    CompanyName = null
                }
            };

            var list = new List<CardPlanPaymentSchedule>();

            var payment1 = new CardPlanPaymentSchedule()
            {
                Amount = 10,
                Date = DateTime.Now,
                Status = 2,
                Skip = false,
                OriginalPaymentId = 0,
                PaymentId = 1,
                Retry = false,
                Payment = new CardPlanPayment()
                {
                    Number = "P1623526522",
                    ResponseCode = 0,
                    ResponseMessage = "Failed",
                    Status = 2,
                    AuthCode = "1234",
                    ProviderResponse = "00",
                    TimeStamp = DateTime.Now
                }
            };

            list.Add(payment1);

            var payment2 = new CardPlanPaymentSchedule()
            {
                Amount = 10,
                Date = DateTime.Now,
                Status = 2,
                Skip = false,
                OriginalPaymentId = 1,
                PaymentId = 2,
                Retry = true,
                Payment = new CardPlanPayment()
                {
                    Number = "P1623526522",
                    ResponseCode = 0,
                    ResponseMessage = "Success",
                    Status = 2,
                    AuthCode = "1234",
                    ProviderResponse = "00",
                    TimeStamp = DateTime.Now
                }
            };


            list.Add(payment2);

            cardPlan.PaymentSchedule = list;

            var actual = CardPlanMapper.ToResource(cardPlan);

            Assert.AreEqual(actual.PaymentSchedule[0].Amount, payment1.Amount, "Payment 1 Amount values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Date.ToString(), payment1.Date.ToString(), "Payment 1 Payment date values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Skip, payment1.Skip, "Payment 1 Skip values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Status, (Enum.GetName(typeof(PlanScheduleStatus), payment1.Status)).ToLower(), "Payment 1 Status values are not same");

            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Number, payment1.Payment.Number, "Payment 1 transaction number values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.TimeStamp.ToString(), payment1.Payment.TimeStamp.ToString(), "Payment 1 timestamp values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Status, (Enum.GetName(typeof(PaymentStatus), payment1.Payment.Status)).ToLower(), "Payment 1 transaction status values are not same");

            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Response.Code, payment1.Payment.ResponseCode, "Payment 1 response code values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Response.Message, payment1.Payment.ResponseMessage, "Payment 1 response messages values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Response.AuthCode, payment1.Payment.AuthCode, "Payment 1 auth code values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Response.ProviderResponse, payment1.Payment.ProviderResponse, "Payment 1 provider response values are not same");

            Assert.AreEqual(actual.PaymentSchedule[0].Retries[0].Amount, payment2.Amount, "Payment 2 Amount values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Retries[0].Date.ToString(), payment2.Date.ToString(), "Payment 2 Payment date values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Retries[0].Skip, payment2.Skip, "Payment 2 Skip values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Retries[0].Status, (Enum.GetName(typeof(PlanScheduleStatus), payment2.Status)).ToLower(), "Payment 2 Status values are not same");

            Assert.AreEqual(actual.PaymentSchedule[0].Retries[0].Payment.Number, payment2.Payment.Number, "Payment 2 transaction number values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Retries[0].Payment.TimeStamp.ToString(), payment2.Payment.TimeStamp.ToString(), "Payment 2 timestamp values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Retries[0].Payment.Status, (Enum.GetName(typeof(PaymentStatus), payment2.Payment.Status)).ToLower(), "Payment 2 transaction status values are not same");

            Assert.AreEqual(actual.PaymentSchedule[0].Retries[0].Payment.Response.Code, payment2.Payment.ResponseCode, "Payment 2 response code values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Retries[0].Payment.Response.Message, payment2.Payment.ResponseMessage, "Payment 2 response messages values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Retries[0].Payment.Response.AuthCode, payment2.Payment.AuthCode, "Payment 2 auth code values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Retries[0].Payment.Response.ProviderResponse, payment2.Payment.ProviderResponse, "Payment 2 provider response values are not same");
            
        }
    }
}
