using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Common.Rest.Client;
using Example.Notific.TPF.WebApi.Client.Resources;

namespace Example.Notific.TPF.WebApi.Client
{
    public interface ITPFrameworkWebApiClient
    {
        /// <summary>
        /// Get the card payment details
        /// </summary>
        /// <param name="number">Transaction number</param>
        /// <returns>Returns the card payment details</returns>
        IApiResponse<TPCardPayment> GetCardPaymentDetails(string number);

        /// <summary>
        /// Get the direct debit payment details
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        IApiResponse<TPDDPayment> GetDirectDebitPaymentDetails(string number);

        /// <summary>
        /// Get the recurring card payment details
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Returns the recurring card plan details</returns>
        IApiResponse<TPRecurringCardPlan> GetRecurringPlanDetails(int id);

        /// <summary>
        /// Get the direct debit plan details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IApiResponse<TPRecurringDDPlan> GetDDPlanDetails(int id);
    }
}
