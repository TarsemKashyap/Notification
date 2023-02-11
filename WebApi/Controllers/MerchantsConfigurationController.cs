using Example.Common.Context.CQRS;
using Example.Common.Logging;
using Example.Common.Logging.Web;
using Example.Common.WebAPI2.Controllers;
using Example.Common.WebAPI2.Results;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.WebApi.Contract.Resources;
using Example.Notific.WebApi.Mappers;
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
    [RoutePrefix("merchantsconfiguration/{merchantId:int}")]
    public class MerchantsConfigurationController : BaseController
    {
        public const string RouteNameGetMerchantConfiguration = "GetMerchantConfiguration";
        public const string RouteNameCreateMerchantConfiguration = "RouteNameCreateMerchantConfiguration";

        private static ILog _logger = LogManager.GetLogger(typeof(MerchantsConfigurationController));
        internal IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto> _retrieveConfigurationQueryHandler;
        internal ICommandHandler<CreateMerchantConfigurationCommand> _configurationMerchantCommandHandler;

        #region Ctors

        public MerchantsConfigurationController(IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto> retrieveConfigurationQueryHandler, ICommandHandler<CreateMerchantConfigurationCommand> configurationMerchantCommandHandler)
        {
            if (retrieveConfigurationQueryHandler == null) throw new ArgumentNullException("Reterieve merchant configuration handler cannot be passed null");

            if (configurationMerchantCommandHandler == null) throw new ArgumentNullException("Merchant configuration command handler cannot be passed null");

            _retrieveConfigurationQueryHandler = retrieveConfigurationQueryHandler;

            _configurationMerchantCommandHandler = configurationMerchantCommandHandler;
        }

        #endregion

        /// <summary>
        /// Get the merchant configuration details
        /// </summary>
        /// <param name="merchantId">Merchant Id</param>
        /// <returns>Returns the merchant configuration details</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>  
        /// <response code="504">Gateway timeout</response>            
        [Route(@"", Name = RouteNameGetMerchantConfiguration)]
        [HttpGet]
        [ResponseType(typeof(Configuration))]
        public IHttpActionResult Get(int merchantId)
        {
            using (_logger.PushWeb())
            {
                _logger.Info("Handling api request: Reteriving the merchant configuration details", merchantId);

                IHttpActionResult result = null;

                try
                {
                    var query = new RetrieveConfigurationQuery() { MerchantId = merchantId };

                    var data = _retrieveConfigurationQueryHandler.Handle(query);

                    var response = ConfigurationDtoMapper.ToConfigurationResource(data);

                    _logger.Debug("RetrieveConfigurationQuery successfully handled", response);

                    result = Ok(response);

                    _logger.Info("Request successfully executed, returning response", result);

                }
                catch (QueryNotValidException ex)
                {
                    _logger.Warn("Get merchant configuration details failed validation", new { Errors = string.Join(", ", ex.ErrorMessages.ToArray()) }, ex);

                    _logger.Info("Returning query validation error response", new { StatusCode = HttpStatusCode.BadRequest });

                    result = new BadRequest(ex.ErrorMessages.ToList(), Request);

                }
                catch (QueryTimeoutException ex)
                {
                    _logger.Warn("QueryTimeoutException caught handling a RetrieveConfigurationQuery query", ex);

                    result = new GatewayTimeout(new List<string>() { "Upstream Timeout", ex.Message }, Request);
                }
                catch (ConfigurationNotFoundException ex)
                {
                    _logger.Warn("ConfigurationNotFoundException caught handling a RetrieveConfigurationQuery query", ex);

                    result = new NotFound(new List<string>() { ex.Message }, Request);
                }

                return result;
            }

        }

        /// <summary>
        /// Save the merchant configuration
        /// </summary>
        /// <param name="merchantId">Merchant Id</param>
        /// <returns>Returns the merchant configuration details</returns>
        /// <response code="201">Created</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>  
        /// <response code="504">Gateway timeout</response>       
        [Route(@"", Name = RouteNameCreateMerchantConfiguration)]
        [HttpPost]
        [ResponseType(typeof(MerchantConfiguration))]
        public IHttpActionResult Post(int merchantId)
        {
            using (_logger.PushWeb())
            {
                _logger.Info("Handling api request: Save the merchant configuration details", merchantId);

                IHttpActionResult result = null;

                try
                {
                    var command = new CreateMerchantConfigurationCommand() { MerchantId = merchantId };

                    _configurationMerchantCommandHandler.Handle(command);

                    var query = new RetrieveConfigurationQuery() { MerchantId = merchantId };

                    var data = _retrieveConfigurationQueryHandler.Handle(query);

                    var response = ConfigurationDtoMapper.ToResource(data, Url);

                    _logger.Debug("RetrieveConfigurationQuery successfully handled", response);

                    result = Created<MerchantConfiguration>(response.Links[0].Href, response);

                    _logger.Info("Request successfully executed, returning response", result);

                }
                catch (CommandNotValidException ex)
                {
                    _logger.Warn("CommandNotValidException caught handling a CreateMerchantConfigurationCommand command", ex);

                    result = new BadRequest(ex.ErrorMessages.ToList(), Request);
                }
                catch (CommandTimeoutException ex)
                {
                    _logger.Warn("CommandTimeoutException caught handling a CreateMerchantConfigurationCommand command", ex);

                    result = new GatewayTimeout(new List<string>() { "Upstream Timeout", ex.Message }, Request);
                }
                catch (QueryNotValidException ex)
                {
                    _logger.Warn("Get merchant configuration details failed validation", new { Errors = string.Join(", ", ex.ErrorMessages.ToArray()) }, ex);

                    _logger.Info("Returning query validation error response", new { StatusCode = HttpStatusCode.BadRequest });

                    result = new BadRequest(ex.ErrorMessages.ToList(), Request);

                }
                catch (QueryTimeoutException ex)
                {
                    _logger.Warn("QueryTimeoutException caught handling a RetrieveConfigurationQuery query", ex);

                    result = new GatewayTimeout(new List<string>() { "Upstream Timeout", ex.Message }, Request);
                }
                catch (ConfigurationNotFoundException ex)
                {
                    _logger.Warn("ConfigurationNotFoundException caught handling a RetrieveConfigurationQuery query", ex);

                    result = new NotFound(new List<string>() { ex.Message }, Request);
                }

                return result;
            }

        }
    }
}
