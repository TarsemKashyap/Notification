using Example.Common.Context.CQRS;
using Example.Common.Logging;
using Example.Common.Logging.Web;
using Example.Common.WebAPI2.Controllers;
using Example.Common.WebAPI2.Results;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.WebApi.Contract.Requests;
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
using System.Web.Http.Results;

namespace Example.Notific.WebApi.Controllers
{
    [RoutePrefix("subscriptions")]
    public class SubscriptionsController : BaseController
    {
        public const string RouteNameGetEventSubscription = "GetEventSubscription";
        public const string RouteNameCreateEventSubscription = "CreateEventSubscription";
        public const string RouteNameRemoveEventSubscription = "RemoveEventSubscription";
        public const string RouteNameGetMerchantSubscription = "GetMerchantSubscription";

        #region locals

        ILog _logger = LogManager.GetLogger(typeof(SubscriptionsController));
        internal IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto> _retrieveSubscriptionQueryHandler;
        internal IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]> _retrieveMerchantSubscriptionQueryHandler;
        internal ICommandHandler<SubscribeToEventCommand> _subscribeToEventCommandHandler;
        internal ICommandHandler<TerminateSubscriptionCommand> _terminateSubscriptionCommandHandler;

        #endregion

        #region Ctors

        public SubscriptionsController(IQueryHandler<RetrieveSubscriptionQuery, SubscriptionDto> retrieveSubscriptionQueryHandler, ICommandHandler<SubscribeToEventCommand> subscribeToEventCommandHandler, ICommandHandler<TerminateSubscriptionCommand> terminateSubscriptionCommandHandler, IQueryHandler<RetrieveMerchantSubscriptionsQuery, SubscriptionDto[]> retrieveMerchantSubscriptionQueryHandler)
        {
            if (retrieveSubscriptionQueryHandler == null) throw new ArgumentNullException("Reterieve event subscription handler cannot be passed null");

            if (subscribeToEventCommandHandler == null) throw new ArgumentNullException("Subscribe event handler cannot be passed null");

            if (terminateSubscriptionCommandHandler == null) throw new ArgumentNullException("Terminate event handler cannot be passed null");

            if (retrieveMerchantSubscriptionQueryHandler == null) throw new ArgumentNullException("Reterieve merchant event subscription handler cannot be passed null");

            _retrieveSubscriptionQueryHandler = retrieveSubscriptionQueryHandler;

            _subscribeToEventCommandHandler = subscribeToEventCommandHandler;

            _terminateSubscriptionCommandHandler = terminateSubscriptionCommandHandler;

            _retrieveMerchantSubscriptionQueryHandler = retrieveMerchantSubscriptionQueryHandler;
        }

        #endregion

        /// <summary>
        /// Get the event subscription details by subscription id
        /// </summary>
        /// <param name="id">Subscription id</param>
        /// <returns></returns>
        /// <remarks>Returns subscription details</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>  
        /// <response code="504">Gateway timeout</response>            
        [Route(@"{id}", Name = RouteNameGetEventSubscription)]
        [HttpGet]
        [ResponseType(typeof(Subscription))]
        public IHttpActionResult Get(string id)
        {
            using (_logger.PushWeb())
            {
                _logger.Info("Handling api request: Reteriving the event subscription details", id);

                IHttpActionResult result = null;

                try
                {
                    if (RequestValidator.Validate(Request, id, new SubscriptionIdRequestValidator(), ref result))
                    {
                        var query = new RetrieveSubscriptionQuery() { SubscriptionId = Convert.ToInt64(id) };

                        var data = _retrieveSubscriptionQueryHandler.Handle(query);

                        var response = SubscriptionDtoMapper.ToResource(data, Url);

                        _logger.Debug("RetrieveSubscriptionQuery successfully handled", response);

                        result = Ok<Subscription>(response);

                        _logger.Info("Request successfully executed, returning response", result);
                    }

                }
                catch (QueryNotValidException ex)
                {
                    _logger.Warn("Get subscription by subscription id failed validation", new { Errors = string.Join(", ", ex.ErrorMessages.ToArray()) }, ex);

                    _logger.Info("Returning query validation error response", new { StatusCode = HttpStatusCode.BadRequest });

                    result = new BadRequest(ex.ErrorMessages.ToList(), Request);

                }
                catch (QueryTimeoutException ex)
                {
                    _logger.Warn("QueryTimeoutException caught handling a RetrieveSubscriptionQuery query", ex);

                    result = new GatewayTimeout(new List<string>() { "Upstream Timeout", ex.Message }, Request);
                }
                catch (SubscriptionNotFoundException ex)
                {
                    _logger.Warn("SubscriptionDoesNotExist caught handling a RetrieveSubscriptionQuery query", ex);

                    result = new NotFound(new List<string>() { ex.Message }, Request);
                }

                return result;
            }

        }

        /// <summary>
        /// Subscribe to events
        /// </summary>
        /// <param name="requestData">Request data for subscribing the events</param>
        /// <remarks>Returns subscription details</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>  
        /// <response code="504">Gateway timeout</response>    
        [Route(Name = RouteNameCreateEventSubscription)]
        [HttpPost]
        [ResponseType(typeof(Subscription))]
        public IHttpActionResult Post(CreateEventSubscriptionRequest requestData)
        {
            using (_logger.PushWeb())
            {
                _logger.Info("Handling api request: create event subscription", requestData);

                IHttpActionResult result = null;

                try
                {
                    if (RequestValidator.Validate<CreateEventSubscriptionRequest>(Request, requestData, new CreateEventSubscriptionRequestValidator(), ref result))
                    {
                        var command = CreateEventSubscriptionRequestMapper.ToCommand(requestData);

                        _subscribeToEventCommandHandler.Handle(command);

                        Subscription response = GetSubscriptionDetails(command);

                        _logger.Debug("SubscribeToEventCommand successfully handled", response);

                        result = Created<Subscription>(response.Links[0].Href, response);

                        _logger.Info("Request successfully executed, returning response", result);
                    }
                }
                catch (CommandNotValidException ex)
                {
                    _logger.Warn("CommandNotValidException caught handling a CreateCardTokenCommand command", ex);

                    result = new BadRequest(ex.ErrorMessages.ToList(), Request);
                }
                catch (CommandTimeoutException ex)
                {
                    _logger.Warn("CommandTimeoutException caught handling a CreateCardTokenCommand command", ex);

                    result = new GatewayTimeout(new List<string>() { "Upstream Timeout", ex.Message }, Request);
                }
                catch (QueryNotValidException ex)
                {
                    _logger.Warn("Get subscription by subscription id failed validation", new { Errors = string.Join(", ", ex.ErrorMessages.ToArray()) }, ex);

                    _logger.Info("Returning query validation error response", new { StatusCode = HttpStatusCode.BadRequest });

                    result = new BadRequest(ex.ErrorMessages.ToList(), Request);
                }
                catch (QueryTimeoutException ex)
                {
                    _logger.Warn("QueryTimeoutException caught handling a RetrieveSubscriptionQuery query", ex);

                    result = new GatewayTimeout(new List<string>() { "Upstream Timeout", ex.Message }, Request);
                }


                return result;
            }
        }

        private Subscription GetSubscriptionDetails(SubscribeToEventCommand command)
        {
            var query = new RetrieveSubscriptionQuery() { SubscriptionId = command.SubscriptionId };

            var data = _retrieveSubscriptionQueryHandler.Handle(query);

            var response = SubscriptionDtoMapper.ToResource(data, Url);

            return response;
        }

        /// <summary>
        /// Terminate the event subscription
        /// </summary>
        /// <param name="id">Subscription id</param>
        /// <returns></returns>
        /// <response code="204">No content</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>  
        /// <response code="504">Gateway timeout</response>
        [Route(@"{id}", Name = RouteNameRemoveEventSubscription)]
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            using (_logger.PushWeb())
            {
                _logger.Info("Handling api request: remove event subscription", id);

                IHttpActionResult result = null;

                try
                {
                    if (RequestValidator.Validate(Request, id, new SubscriptionIdRequestValidator(), ref result))
                    {
                        var command = new TerminateSubscriptionCommand() { SubscriptionId = Convert.ToInt64(id) };

                        _terminateSubscriptionCommandHandler.Handle(command);

                        result = new StatusCodeResult(HttpStatusCode.NoContent, Request);

                        _logger.Info("Request successfully executed, returning response", result);
                    }

                }
                catch (CommandNotValidException ex)
                {
                    _logger.Warn("CommandNotValidException caught handling a RemoveCardTokenCommand command", ex);

                    result = new BadRequest(ex.ErrorMessages.ToList(), Request);
                }
                catch (CommandTimeoutException ex)
                {
                    _logger.Warn("CommandTimeoutException caught handling a RemoveCardTokenCommand command", ex);

                    result = new GatewayTimeout(new List<string>() { "Upstream Timeout", ex.Message }, Request);
                }
                catch (SubscriptionNotFoundException ex)
                {
                    _logger.Warn("SubscriptionDoesNotExist caught handling a RemoveCardTokenCommand command", ex);

                    result = new NotFound(new List<string>() { ex.Message }, Request);
                }

                return result;
            }

        }

        /// <summary>
        /// Get the all subscriptions of merchant
        /// </summary>
        /// <param name="merchantId">Merchant Id</param>
        /// <returns></returns>
        /// <remarks>Returns merchant subscriptions details</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>       
        /// <response code="500">Internal Server Error</response>  
        /// <response code="504">Gateway timeout</response>          
        [Route(@"", Name = RouteNameGetMerchantSubscription)]
        [HttpGet]
        [ResponseType(typeof(Subscription[]))]
        public IHttpActionResult GetMerchantSubscriptions([FromUri] string merchantId)
        {
            using (_logger.PushWeb())
            {
                _logger.Info("Handling api request: Reteriving the merchant subscription details", merchantId);

                IHttpActionResult result = null;

                try
                {
                    if (RequestValidator.Validate(Request, merchantId, new MerchantIdRequestValidator(), ref result))
                    {
                        var query = new RetrieveMerchantSubscriptionsQuery() { MerchantId = Convert.ToInt32(merchantId), IncludeTerminated = true };

                        var data = _retrieveMerchantSubscriptionQueryHandler.Handle(query);

                        var response = SubscriptionDtoMapper.ToResource(data, Url);

                        _logger.Debug("RetrieveMerchantSubscriptionsQuery successfully handled", response);

                        result = Ok(response);

                        _logger.Info("Request successfully executed, returning response", result);
                    }

                }
                catch (QueryNotValidException ex)
                {
                    _logger.Warn("Get subscription by merchant id failed validation", new { Errors = string.Join(", ", ex.ErrorMessages.ToArray()) }, ex);

                    _logger.Info("Returning query validation error response", new { StatusCode = HttpStatusCode.BadRequest });

                    result = new BadRequest(ex.ErrorMessages.ToList(), Request);

                }
                catch (QueryTimeoutException ex)
                {
                    _logger.Warn("QueryTimeoutException caught handling a RetrieveMerchantSubscriptionsQuery query", ex);

                    result = new GatewayTimeout(new List<string>() { "Upstream Timeout", ex.Message }, Request);
                }

                return result;
            }

        }
    }
}
