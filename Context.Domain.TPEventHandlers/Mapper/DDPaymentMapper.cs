using Example.MAP.Common.API.Contract.Resources.V1;
using Example.Notific.Context.Common;
using Example.Notific.TPF.WebApi.Client.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.TPEventHandlers.Mapper
{
    public class DDPaymentMapper
    {
        public static DirectDebit ToResource(TPDDPayment ddPayment)
        {
            DirectDebit result = new DirectDebit()
            {
                Amount = ddPayment.Transaction.Amount,
                Currency = ddPayment.Transaction.Currency,
                DueDate = ddPayment.DueDate.ToString("yyyy-MM-dd"),
                Number = ddPayment.Number,
                SettlementDate = ddPayment.Transaction.SettlementDate.HasValue? ddPayment.Transaction.SettlementDate.Value.ToString("yyyy-MM-dd"):null ,
                TransactionDate = ddPayment.Transaction.Date.ToString("yyyy-MM-dd"),
                Status = ddPayment.Status == 0 ? "" : (Enum.GetName(typeof(DDStatus), ddPayment.Status)).ToLower()
            };

            DDResponse response = new DDResponse()
            {
                Code = ddPayment.Transaction.ResponseCode,
                Message = ddPayment.Transaction.ResponseMessage
            };

            result.Response = response;

            DDMerchant merchant = new DDMerchant()
            {
                Id = ddPayment.Merchant.Id
            };

            result.Merchant = merchant;

            DDStatement statement = new DDStatement()
            {
                Code = ddPayment.Transaction.Code,
                Particulars = ddPayment.Transaction.Particulars,
                Reference = ddPayment.Transaction.Reference
            };

            result.Statement = statement;

            if (ddPayment.SourceAccount != null)
            {
                DDSourceAccount sourceAccount = new DDSourceAccount()
                {
                    Name = ddPayment.SourceAccount.Name,
                    Number = ddPayment.SourceAccount.Number
                };
                result.SourceAccount = sourceAccount;
            }

            if (ddPayment.DestinationAccount != null)
            {
                DDDestinationAccount destinationAccount = new DDDestinationAccount()
                {
                    Name = ddPayment.DestinationAccount.Name,
                    Number = ddPayment.DestinationAccount.Number
                };

                result.DestinationAccount = destinationAccount;
            }

            DDPlan plan = new DDPlan()
            {
                Id = ddPayment.Plan.Id
            };

            result.Plan = plan;

            return result;
        }
    }
}
