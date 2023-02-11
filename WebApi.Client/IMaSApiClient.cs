using Example.MaS.Service.Contract;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.MaS.WebApi.Client
{
    public interface IMaSApiClient
    {
        /// <summary>
        /// Submit the MaS job execution result
        /// </summary>
        /// <param name="callbackUri">Callback URL</param>
        /// <param name="scheduledJobExecutionResult">Job Result response</param>
        IRestResponse SubmitExecutionResult(string callbackUri, SchedulingJobResultResponse scheduledJobExecutionResult);
    }
}
