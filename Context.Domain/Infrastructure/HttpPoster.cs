using Example.Common.Logging;
using Example.Notific.Context.Domain.Infrastructure.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Example.Notific.Context.Domain.Infrastructure
{
    public class HttpPoster : IHttpPoster
    {
        ILog _logger = LogManager.GetLogger(typeof(HttpPoster));

        public HttpPostResult Post(string endpoint, string data)
        {

            if (endpoint == null || endpoint == "")
                throw new ArgumentNullException("Endpoint cannot be passed null or blank");

            if (data == null || data == "")
                throw new ArgumentNullException("Post data cannot be passed null or blank");

            using (_logger.Push())
            {
                _logger.Info("Preparing to post notification", new { Url = endpoint, Data = data });

                var result = new HttpPostResult();

                try
                {
                    WebRequest request = WebRequest.Create(endpoint);
                    request.Method = "POST";
                    byte[] byteArray = Encoding.UTF8.GetBytes(data);
                    request.ContentType = "application/json";
                    request.ContentLength = byteArray.Length;

                    // Get the request stream.
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();

                    // Get the response.
                    WebResponse response = request.GetResponse();

                    // Get the statuscode.
                    HttpStatusCode statusCode = ((HttpWebResponse)response).StatusCode;

                    result.Message = "";
                    result.HttpStatusCode = (int)statusCode;

                }
                catch (WebException ex)
                {
                    _logger.Warn("Web Exception in posting data to url " + endpoint, ex);

                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        using (WebResponse response = ex.Response)
                        {
                            HttpWebResponse httpResponse = (HttpWebResponse)response;

                            var statusCode = httpResponse.StatusCode;

                            _logger.Info("Http Post result", new { StatusCode = statusCode, FailureReason = ex.Message });

                            result.Message = ex.Message;
                            result.HttpStatusCode = (int)statusCode;
                        }
                    }
                    else
                    {
                        result.Message = ex.Message;
                        result.HttpStatusCode = (int)HttpStatusCode.BadRequest;

                        _logger.Info("Http Post result", new { StatusCode = HttpStatusCode.BadRequest, FailureReason = ex.Message });
                    }

                }
                catch (Exception ex)
                {
                    _logger.Warn("Exception in posting data to url " + endpoint, ex);

                    result.Message = ex.Message;
                    result.HttpStatusCode = (int)HttpStatusCode.InternalServerError;

                    _logger.Info("Http Post result", new { StatusCode = HttpStatusCode.InternalServerError, FailureReason = ex.Message });
                }

                return result;
            }
        }
        public HttpPostResult Post(string endpoint, string data, string notSignature)
        {

            if (endpoint == null || endpoint == "")
                throw new ArgumentNullException("Endpoint cannot be passed null or blank");

            if (data == null || data == "")
                throw new ArgumentNullException("Post data cannot be passed null or blank");

            using (_logger.Push())
            {
                _logger.Debug("Preparing to post notification with x-ExampleCompany-notsig header", new { Url = endpoint, Data = data });

                var result = new HttpPostResult();

                try
                {
                    WebRequest request = WebRequest.Create(endpoint);
                    request.Method = "POST";
                    byte[] byteArray = Encoding.UTF8.GetBytes(data);
                    request.ContentType = "application/json";
                    request.ContentLength = byteArray.Length;

                    request.Headers.Add("x-ExampleCompany-notsig", notSignature);

                    // Get the request stream.
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();

                    // Get the response.
                    WebResponse response = request.GetResponse();

                    // Get the statuscode.
                    HttpStatusCode statusCode = ((HttpWebResponse)response).StatusCode;

                    result.Message = "";
                    result.HttpStatusCode = (int)statusCode;

                }
                catch (WebException ex)
                {
                    _logger.Warn("Web Exception in posting data to url " + endpoint, ex);

                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        using (WebResponse response = ex.Response)
                        {
                            HttpWebResponse httpResponse = (HttpWebResponse)response;

                            var statusCode = httpResponse.StatusCode;

                            _logger.Info("Http Post result", new { StatusCode = statusCode, FailureReason = ex.Message });

                            result.Message = ex.Message;
                            result.HttpStatusCode = (int)statusCode;
                        }
                    }
                    else
                    {
                        result.Message = ex.Message;
                        result.HttpStatusCode = (int)HttpStatusCode.BadRequest;

                        _logger.Info("Http Post result", new { StatusCode = HttpStatusCode.BadRequest, FailureReason = ex.Message });
                    }

                }
                catch (Exception ex)
                {
                    _logger.Warn("Exception in posting data to url " + endpoint, ex);

                    result.Message = ex.Message;
                    result.HttpStatusCode = (int)HttpStatusCode.InternalServerError;

                    _logger.Info("Http Post result", new { StatusCode = HttpStatusCode.InternalServerError, FailureReason = ex.Message });
                }

                return result;
            }
        }
    }
}
