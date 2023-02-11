using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Common.Rest.Client;
using log4net;
using RestSharp;
using Example.Notific.TPF.WebApi.Client.Resources;

namespace Example.Notific.TPF.WebApi.Client
{
    public class TPFrameworkWebApiClient : ApiClient, ITPFrameworkWebApiClient
    {
        #region Properties and local variables

        ILog _logger = LogManager.GetLogger(typeof(TPFrameworkWebApiClient));

        #endregion

        #region Ctors

        public TPFrameworkWebApiClient(string baseUrl) : base(baseUrl) { }

        #endregion

        public IApiResponse<TPCardPayment> GetCardPaymentDetails(string number)
        {
            const string endpointCardPayment = "cardpayments/{number}";

            var request = GenerateNewGetRequest(endpointCardPayment);

            request.AddParameter("number", number, ParameterType.UrlSegment);

            return Execute<TPCardPayment>(request);
        }

        public IApiResponse<TPRecurringDDPlan> GetDDPlanDetails(int id)
        {
            const string endpointDDPlan = "recurringddplans/{id}";

            var request = GenerateNewGetRequest(endpointDDPlan);

            request.AddParameter("id", id, ParameterType.UrlSegment);

            return Execute<TPRecurringDDPlan>(request);
        }

        public IApiResponse<TPDDPayment> GetDirectDebitPaymentDetails(string number)
        {
            const string endpointDirectDebitPayment = "ddpayments/{number}";

            var request = GenerateNewGetRequest(endpointDirectDebitPayment);

            request.AddParameter("number", number, ParameterType.UrlSegment);

            return Execute<TPDDPayment>(request);
        }

        public IApiResponse<TPRecurringCardPlan> GetRecurringPlanDetails(int id)
        {
            const string endpointRecurringPlan = "recurringcardplans/{id}";

            var request = GenerateNewGetRequest(endpointRecurringPlan);

            request.AddParameter("id", id, ParameterType.UrlSegment);

            return Execute<TPRecurringCardPlan>(request);
        }
    }
}
