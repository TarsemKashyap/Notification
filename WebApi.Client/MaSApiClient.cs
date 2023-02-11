using Example.Common.Rest.Client;
using Example.MaS.Service.Contract;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.MaS.WebApi.Client
{
    public class MaSApiClient :  IMaSApiClient
    { 
        public IRestResponse SubmitExecutionResult(string callbackUri, SchedulingJobResultResponse scheduledJobExecutionResult)
        {
            var client = new RestClient();

            client.BaseUrl = callbackUri;

            var request = new RestRequest(Method.POST);

            request.RequestFormat = DataFormat.Json;

            request.JsonSerializer = new RestSharpJsonNetSerializer();

            request.AddBody(scheduledJobExecutionResult);

            return client.Execute(request);
        }
    }
}
