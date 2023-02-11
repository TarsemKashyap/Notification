using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Logging;
using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Contract.CQRS.Queries;
using Example.Notific.Context.CQRS.Queries.DataMapper;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Queries.Presentation
{
    public class RetrieveConfigurationQueryHandler : IQueryHandler<RetrieveConfigurationQuery, ConfigurationDto>
    {
        private static ILog _logger = LogManager.GetLogger(typeof(RetrieveConfigurationQueryHandler));

        private readonly IUnitOfWorkStorage _storage;

        private readonly IMerchantConfigRepository _merchantConfigRepository;


        #region Ctors

        public RetrieveConfigurationQueryHandler(IUnitOfWorkStorage storage, IMerchantConfigRepository merchantConfigRepository)
        {
            if (storage == null) throw new ArgumentNullException("storage", "UnitOfWorkStorage cannot be passed null");

            if (merchantConfigRepository == null) throw new ArgumentNullException("Merchant config repository cannot be passed null");

            _storage = storage;

            _merchantConfigRepository = merchantConfigRepository;
        }

        #endregion

        public ConfigurationDto Handle(RetrieveConfigurationQuery query)
        {
            if (query == null)
                throw new ArgumentNullException("RetrieveConfigurationQuery query cannot be passed null");

            using (_logger.Push())
            {
                _logger.Info("Handling query : RetrieveConfigurationQuery", query);

                try
                {
                    using (var uow = _storage.NewUnitOfWork())
                    {
                        using (var txn = uow.BeginTransaction())
                        {
                            MerchantConfig configurationDetails = _merchantConfigRepository.GetByMerchant(query.MerchantId);

                            if (configurationDetails == null)
                            {
                                _logger.Warn("Merchant configuration not found. Id: " + query.MerchantId);
                                throw new ConfigurationNotFoundException(query.MerchantId);
                            }

                            _logger.Debug("Configuration details", configurationDetails);

                            var configurationDto = DomainConfigurationMapper.ToConfigurationDto(configurationDetails);

                            _logger.Info("Query executed: RetrieveConfigurationQuery", configurationDto);

                            return configurationDto;
                        }
                    }

                }
                catch (SqlException ex)
                {
                    _logger.Warn("Exception occured: RetrieveConfigurationQuery", ex);
                    // rethrow exception from context for timeout or deadlock
                    if (ex.Number == -2 || ex.Number == 1205)
                        throw new QueryTimeoutException(String.Format("Failed to retreive configuration details. Id: {0}", query.MerchantId));
                    else
                        throw;
                }
            }
        }
    }
}
