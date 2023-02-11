using Example.Common.Context.CQRS;
using Example.Common.Logging;
using Example.Common.Logging.Web;
using Example.Common.WebAPI2.Controllers;
using Example.Common.WebAPI2.Results;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Example.Notific.WebApi.Controllers
{
    [RoutePrefix("notifications")]
    public class NotificationsController : BaseController
    {
        public const string RouteNameGetNotificationDetails = "GetNotificationDetails";

        #region locals

        ILog _logger = LogManager.GetLogger(typeof(NotificationsController));

        #endregion

        #region Ctors

        public NotificationsController()
        {

        }

        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route(@"{id}", Name = RouteNameGetNotificationDetails)]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            using (_logger.PushWeb())
            {
                _logger.Info("Handling api request: Reteriving the event subscription details", id);

                IHttpActionResult result = null;

                try
                {


                }
                catch (QueryNotValidException ex)
                {
                    _logger.Warn("Get card by token failed validation", new { Errors = string.Join(", ", ex.ErrorMessages.ToArray()) }, ex);

                    _logger.Info("Returning query validation error response", new { StatusCode = HttpStatusCode.BadRequest });

                    result = new BadRequest(ex.ErrorMessages.ToList(), Request);

                }
                catch (QueryTimeoutException ex)
                {
                    _logger.Warn("QueryTimeoutException caught handling a RetrieveCardTokenQuery query", ex);

                    result = new GatewayTimeout(new List<string>() { "Upstream Timeout", ex.Message }, Request);
                }

                return result;
            }

        }
    }
}
