using Example.Common.Context.Bus;
using Example.Common.Context.CQRS;
using Example.Common.Logging;
using Example.Common.Logging.Web;
using Example.Common.WebAPI2.Controllers;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.MaS.Context.Contract;
using Example.MaS.Service.Contract;
using Example.MaS.Service.Contract.Exceptions;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Example.Notific.WebApi.Controllers
{
    [RoutePrefix("scheduledtasks/sendnotificationretries")]
    public class ScheduledTasksSendNotificationRetriesController : BaseController
    {
        public const string RouteNameScheduleRetryNotifications = "ScheduleRetryNotifications";

        private static ILog _logger = LogManager.GetLogger(typeof(ScheduledTasksSendNotificationRetriesController));
        IBusAdaptor _busAdapter;

        #region Ctors
        public ScheduledTasksSendNotificationRetriesController(IBusAdaptor busAdapter)
        {
            if (busAdapter == null) throw new ArgumentNullException("Bus adapter cannot be passed as null");

            _busAdapter = busAdapter;
        }

        #endregion

        [Route("", Name = RouteNameScheduleRetryNotifications)]
        [HttpPost]
        public SchedulingJobResponse Post([FromBody] SchedulingJobRequest request)
        {
            using (_logger.PushWeb())
            {
                _logger.Info("Incoming SchedulingJobRequest request", request);


                SchedulingJobResponse result = new SchedulingJobResponse()
                {
                    Result = ExecutionResultEnum.Failure,
                    TimeStarted = DateTime.Now
                };
              
                try
                {
                    if (request != null)
                    {
                        SendNotificationRetiresScheduledTaskCommand command = new SendNotificationRetiresScheduledTaskCommand() { CallbackUrl = request.CallBackUrl };

                        _logger.Info("Going to send command", command);

                        _busAdapter.Send(command);

                        _logger.Debug("Send notifications retired started", command);

                        result = new SchedulingJobResponse()
                        {
                            Result = ExecutionResultEnum.Accepted,
                            TimeStarted = DateTime.Now
                        };
                    }
                }
                catch (CommandNotValidException e)
                {
                    string errors = string.Join(", ", e.ErrorMessages.ToArray());

                    _logger.Warn("Command failed validation in ScheduledTasksSendNotificationRetries", new { Errors = errors }, e);

                    result.Result = ExecutionResultEnum.Failure;

                    result.Errors = errors;
                }
                catch (ParameterExpectedException ex)
                {
                    _logger.Error("Failed to execute scheduling job ScheduledTasksSendNotificationRetries", request, ex);

                    result.Result = ExecutionResultEnum.Failure;
                    result.Errors = ex.Message;
                }
                catch (Exception ex)
                {
                    _logger.Error("Failed to execute scheduling job ScheduledTasksSendNotificationRetries", request, ex);

                    result.Result = ExecutionResultEnum.Failure;
                    result.Errors = ex.Message;
                }

                _logger.Info("Returning result to MaS", result);

                return result;
            }
        }
    }
}