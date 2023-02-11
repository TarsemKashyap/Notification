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
    public class DDPlanMapper
    {
        public static DirectDebitPlan ToResource(TPRecurringDDPlan ddPlan)
        {
            DirectDebitPlan result = new DirectDebitPlan()
            {
                AmendmentDate = ddPlan.AmendmentDate,
                Amount = ddPlan.Amount,
                ApprovalDate = ddPlan.ApprovalDate.HasValue ? ddPlan.ApprovalDate.Value.ToString("yyyy-MM-dd") : null,
                Created = ddPlan.Created,
                Frequency = ddPlan.Frequency == 0 ? "" : EnumExtensions.GetString((DDPlanFrequency)ddPlan.Frequency),
                InstalmentFailOption = ddPlan.InstalmentFailOption == 0 ? "" : EnumExtensions.GetString((DDPlanInstalementOptions)ddPlan.InstalmentFailOption),
                Particulars = ddPlan.Particulars,
                Reference = ddPlan.Reference,
                StartDate = ddPlan.StartDate.ToString("yyyy-MM-dd"),
                Status = ddPlan.Status == 0 ? "" : Dictionaries.DDPlanStatus[ddPlan.Status],
                StatusChangedDate = ddPlan.StatusChangedDate,
                TotalAmount = ddPlan.TotalAmount,
                Type = ddPlan.Type == 0 ? "" : EnumExtensions.GetString((DDPlanType)ddPlan.Type),
                Id = ddPlan.Id,
                NextPaymentDate = ddPlan.NextPaymentDate.HasValue ? ddPlan.NextPaymentDate.Value.ToString("yyyy-MM-dd") : null,
                MerchantReference1 = ddPlan.MerchantReference1,
                MerchantReference2 = ddPlan.MerchantReference2,
                MerchantReference3 = ddPlan.MerchantReference3
            };

            if (ddPlan.Payer != null)
            {
                RecurringPayer payer = new RecurringPayer()
                {
                    Address1 = ddPlan.Payer.Address1,
                    Address2 = ddPlan.Payer.Address2,
                    Address3 = ddPlan.Payer.address3,
                    City = ddPlan.Payer.City,
                    CompanyName = ddPlan.Payer.CompanyName,
                    Country = ddPlan.Payer.Country,
                    DateOfBirth = ddPlan.Payer.DateOfBirth.HasValue ? ddPlan.Payer.DateOfBirth.Value.ToString("yyyy-MM-dd") : null,
                    Email = ddPlan.Payer.Email,
                    Fax = ddPlan.Payer.Fax,
                    FirstNames = ddPlan.Payer.FirstNames,
                    LastName = ddPlan.Payer.LastName,
                    Postcode = ddPlan.Payer.Postcode,
                    Suburb = ddPlan.Payer.Suburb,
                    TelephoneHome = ddPlan.Payer.TelephoneHome,
                    TelephoneMobile = ddPlan.Payer.TelephoneMobile,
                    TelephoneWork = ddPlan.Payer.TelephoneWork,
                    Title = ddPlan.Payer.Title,
                    State=ddPlan.Payer.State
                };

                result.Payer = payer;
            }

            RecurringDDMerchant merchant = new RecurringDDMerchant()
            {
                Id = ddPlan.Merchant.Id
            };

            result.Merchant = merchant;

            if (ddPlan.InitialPayment != null)
            {
                RecurringInitialPayment initialPayment = new RecurringInitialPayment()
                {
                    Amount = ddPlan.InitialPayment.Amount,
                    Date = ddPlan.InitialPayment.Date.ToString("yyyy-MM-dd")
                };

                result.InitialPayment = initialPayment;
            }

            MAP.Common.API.Contract.Resources.V1.BankAccount account = new MAP.Common.API.Contract.Resources.V1.BankAccount();

            if (ddPlan.BankAccount != null)
            {
                account.Name = ddPlan.BankAccount.Name;
                account.Number = ddPlan.BankAccount.Number;
            }

            if (ddPlan.BankAccount != null)
            {
                RecurringDDBankDetails details = new RecurringDDBankDetails()
                {
                    BranchAddress1 = ddPlan.BankAccount.BranchAddress1,
                    BranchAddress2 = ddPlan.BankAccount.BranchAddress2,
                    Name = ddPlan.BankAccount.BankName,
                    Account = account
                };

                result.BankDetails = details;
            }

            if (ddPlan.Amendments != null)
            {
                List<RecurringAmendment> amendmentList = new List<RecurringAmendment>();

                foreach (var item in ddPlan.Amendments)
                {
                    RecurringAmendment amendment = new RecurringAmendment()
                    {
                        Comment = item.Comment,
                        Date = item.Date,
                        User = item.User,
                        Type = item.Type == 0 ? "" : (Enum.GetName(typeof(DDPlanAmendmentType), item.Type)).ToLower(),
                        PreviousData = item.PreviousData
                    };

                    amendmentList.Add(amendment);
                }

                result.Amendments = amendmentList.ToArray();
            }

            List<RecurringDDPlanPaymentSchedule> listSchedule = new List<RecurringDDPlanPaymentSchedule>();

            if (ddPlan.PaymentSchedule != null)
            {

                foreach (var item in ddPlan.PaymentSchedule)
                {
                    var objPaymentSchedule = new RecurringDDPlanPaymentSchedule();

                    objPaymentSchedule.Amount = item.Amount;
                    objPaymentSchedule.Date = item.Date.ToString("yyyy-MM-dd");
                    objPaymentSchedule.Skip = item.Skip;
                    objPaymentSchedule.Status = item.Status == 0 ? "" : (Enum.GetName(typeof(PlanScheduleStatus), item.Status)).ToLower();

                    if (item.DirectDebit != null)
                    {
                        var objPayment = new RecurringDDPlanPayment();

                        objPayment.Number = item.DirectDebit.Number;
                        objPayment.Status = item.DirectDebit.Status == 0 ? "" : (Enum.GetName(typeof(DDStatus), item.DirectDebit.Status)).ToLower();
                        objPayment.SettlementDate = item.DirectDebit.SettlementDate.HasValue? item.DirectDebit.SettlementDate.Value.ToString("yyyy-MM-dd"):null;
                        objPayment.TransactionDate = item.DirectDebit.TransactionDate.ToString("yyyy-MM-dd");

                        var objResponse = new DDResponse();

                        objResponse.Code = item.DirectDebit.ResponseCode;
                        objResponse.Message = item.DirectDebit.ResponseMessage;


                        objPayment.Response = objResponse;
                        objPaymentSchedule.Payment = objPayment;

                    }

                    listSchedule.Add(objPaymentSchedule);

                }

            }

            result.PaymentSchedule = listSchedule.ToArray();

            return result;
        }
    }
}
