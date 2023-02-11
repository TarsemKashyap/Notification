using Example.Common.Context.CQRS;
using Example.Common.Logging;
using Example.Common.Logging.Web;
using Example.Common.WebAPI2.Controllers;
using Example.Common.WebAPI2.Results;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.WebApi.Contract.Resources;
using Example.Notific.WebApi.Mappers;
using Example.Notific.WebApi.RequestValidators;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Example.Notific.WebApi.Controllers
{
    [RoutePrefix("events")]
    public class EventsController : BaseController
    {
        public const string RouteNameGetEventDetails = "GetEventDetails";
        public const string RouteNameGetEventDetailsMerchant = "GetEventDetailsMerchant";

        #region locals

        ILog _logger = LogManager.GetLogger(typeof(EventsController));
        internal IQueryHandler<RetrieveEventQuery, EventDto> _retrieveEventQueryHandler;
        internal IQueryHandler<RetrieveNotificationQuery, Object> _retrieveNotificationQueryHandler;

        #endregion

        #region Ctors

        public EventsController(IQueryHandler<RetrieveEventQuery, EventDto> retrieveEventQueryHandler, IQueryHandler<RetrieveNotificationQuery, Object> retrieveNotificationQueryHandler)
        {
            if (retrieveEventQueryHandler == null) throw new ArgumentNullException("Reterieve event details handler cannot be passed null");
            if (retrieveNotificationQueryHandler == null) throw new ArgumentNullException("Reterieve notifications details handler cannot be passed null");

            _retrieveEventQueryHandler = retrieveEventQueryHandler;
            _retrieveNotificationQueryHandler = retrieveNotificationQueryHandler;
        }

        #endregion

        /// <summary>
        /// Get the event details
        /// </summary>
        /// <param name="id">Event id</param>
        /// <returns></returns>
        /// <remarks>Returns event details</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>  
        /// <response code="504">Gateway timeout</response>            
        [Route(@"{id}", Name = RouteNameGetEventDetails)]
        [HttpGet]
        [ResponseType(typeof(Event))]
        public IHttpActionResult Get(string id)
        {
            using (_logger.PushWeb())
            {
                _logger.Info("Handling api request: Reteriving the event details", id);

                IHttpActionResult result = null;

                try
                {
                    if (RequestValidator.Validate(Request, id, new EventIdRequestValidator(), ref result))
                    {
                        var query = new RetrieveEventQuery() { EventId = new Guid(id) };

                        var data = _retrieveEventQueryHandler.Handle(query);

                        var response = EventDtoMapper.ToResource(data, Url);

                        _logger.Debug("RetrieveEventQuery successfully handled", response);

                        result = Ok(response);

                        _logger.Info("Request successfully executed, returning response", result);
                    }

                }
                catch (QueryNotValidException ex)
                {
                    _logger.Warn("Get event details failed validation", new { Errors = string.Join(", ", ex.ErrorMessages.ToArray()) }, ex);

                    _logger.Info("Returning query validation error response", new { StatusCode = HttpStatusCode.BadRequest });

                    result = new BadRequest(ex.ErrorMessages.ToList(), Request);

                }
                catch (QueryTimeoutException ex)
                {
                    _logger.Warn("QueryTimeoutException caught handling a RetrieveEventQuery query", ex);

                    result = new GatewayTimeout(new List<string>() { "Upstream Timeout", ex.Message }, Request);
                }
                catch (EventNotFoundException ex)
                {
                    _logger.Warn("EventNotFoundException caught handling a RetrieveEventQuery query", ex);

                    result = new NotFound(new List<string>() { ex.Message }, Request);
                }

                return result;
            }

        }

        /// <summary>
        /// Get the event details
        /// </summary>
        /// <param name="id">Event id</param>
        /// <param name="merchantId">Merchant Id</param>
        /// <returns></returns>
        /// <remarks>Returns event details</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>  
        /// <response code="504">Gateway timeout</response>            
        [Route(@"", Name = RouteNameGetEventDetailsMerchant)]
        [HttpGet]
        [ResponseType(typeof(Event))]
        public IHttpActionResult Get([FromUri] string id, [FromUri] int merchantId = 0)
        {
            using (_logger.PushWeb())
            {
                _logger.Info("Handling api request: Reteriving the event details", id);

                IHttpActionResult result = null;

                try
                {

                    var query = new RetrieveNotificationQuery() { EventId = new Guid(id), MerchantId = merchantId };

                    var data = _retrieveNotificationQueryHandler.Handle(query);

                    _logger.Debug("RetrieveNotificationQuery successfully handled", data);

                    result = Ok(data);

                    _logger.Info("Request successfully executed, returning response", result);


                }
                catch (QueryNotValidException ex)
                {
                    _logger.Warn("Get event details failed validation", new { Errors = string.Join(", ", ex.ErrorMessages.ToArray()) }, ex);

                    _logger.Info("Returning query validation error response", new { StatusCode = HttpStatusCode.BadRequest });

                    result = new BadRequest(ex.ErrorMessages.ToList(), Request);

                }
                catch (QueryTimeoutException ex)
                {
                    _logger.Warn("QueryTimeoutException caught handling a RetrieveEventQuery query", ex);

                    result = new GatewayTimeout(new List<string>() { "Upstream Timeout", ex.Message }, Request);
                }
                catch (EventNotFoundException ex)
                {
                    _logger.Warn("EventNotFoundException caught handling a RetrieveEventQuery query", ex);

                    result = new NotFound(new List<string>() { ex.Message }, Request);
                }
                catch (EventNotBelongException ex)
                {
                    _logger.Warn("EventNotBelongException caught handling a RetrieveEventQuery query", ex);

                    result = new NotFound(new List<string>() { ex.Message }, Request);
                }

                return result;
            }

        }
    }
}
