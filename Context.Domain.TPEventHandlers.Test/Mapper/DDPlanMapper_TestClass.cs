using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Notific.TPF.WebApi.Client.Resources;
using Example.Notific.Context.Domain.TPEventHandlers.Mapper;
using Example.Notific.Context.Common;

namespace Example.Notific.Context.Domain.TPEventHandlers.Test.Mapper
{
    [TestClass]
    public class DDPlanMapper_TestClass
    {
        [TestMethod]
        public void DDPlanMapper_ToResource_PaymentSchedule()
        {
            TPRecurringDDPlan plan = new TPRecurringDDPlan()
            {
                AmendmentDate = DateTime.Now,
                Amount = 10,
                Frequency = 1,
                NextPaymentDate = DateTime.Now,
                Id = 1234,
                Merchant = new MerchantDetails()
                {
                    Id = 92325
                },
                Particulars = "my Particulars",
                Reference = "my Reference",
                Type = 1

            };

            var list = new List<DDPlanPaymentSchedule>();

            var payment1 = new DDPlanPaymentSchedule()
            {
                Amount = 10,
                Date = DateTime.Now,
                Status = 2,
                Skip = false,
                PaymentDateId = 1,
                DirectDebit = new DDPlanPayment()
                {
                    Number = "D005222222",
                    ResponseCode = 0,
                    ResponseMessage = "Success",
                    SettlementDate = DateTime.Now,
                    Status = 1,
                    TransactionDate = DateTime.Now
                }
            };

            list.Add(payment1);

            var payment2= new DDPlanPaymentSchedule()
            {
                Amount = 10,
                Date = DateTime.Now,
                Status = 1,
                Skip = false,
                PaymentDateId = 11                
            };

            list.Add(payment2);
            plan.PaymentSchedule = list;

            var actual = DDPlanMapper.ToResource(plan);

            Assert.AreEqual(actual.PaymentSchedule[0].Amount, payment1.Amount, "Payment 1 Amount values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Date.ToString(), payment1.Date.ToString(), "Payment 1 Payment date values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Skip, payment1.Skip, "Payment 1 Skip values are not same");          
            Assert.AreEqual(actual.PaymentSchedule[0].Status, (Enum.GetName(typeof(PlanScheduleStatus), payment1.Status)).ToLower(), "Payment 1 Status values are not same");

            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Number, payment1.DirectDebit.Number, "Payment 1 transaction number values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.SettlementDate.ToString(), payment1.DirectDebit.SettlementDate.ToString(), "Payment 1 settlement date values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.TransactionDate.ToString(), payment1.DirectDebit.TransactionDate.ToString(), "Payment 1 transaction date values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Status, (Enum.GetName(typeof(DDStatus), payment1.DirectDebit.Status)).ToLower(), "Payment 1 transaction status values are not same");

            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Response.Code, payment1.DirectDebit.ResponseCode, "Payment 1 response code values are not same");
            Assert.AreEqual(actual.PaymentSchedule[0].Payment.Response.Message, payment1.DirectDebit.ResponseMessage, "Payment 1 response messages values are not same");

            Assert.AreEqual(actual.PaymentSchedule[1].Amount, payment2.Amount, "Payment 2 Amount values are not same");
            Assert.AreEqual(actual.PaymentSchedule[1].Date.ToString(), payment2.Date.ToString(), "Payment 2 Payment date values are not same");
            Assert.AreEqual(actual.PaymentSchedule[1].Skip, payment2.Skip, "Payment 2 Skip values are not same");
            Assert.AreEqual(actual.PaymentSchedule[1].Status, (Enum.GetName(typeof(PlanScheduleStatus), payment2.Status)).ToLower(), "Payment 2 Status values are not same");

            Assert.IsNull(actual.PaymentSchedule[1].Payment, "Payment 2 payment should be NULL");
            
        }
    }
}
