using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Logging;
using Example.Notific.Context.Common;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Example.Notific.Context.CQRS.Commands
{
    public class CreateMerchantConfigurationCommandHandler : ICommandHandler<CreateMerchantConfigurationCommand>
    {
        private static ILog _logger = LogManager.GetLogger(typeof(CreateMerchantConfigurationCommandHandler));

        private readonly IUnitOfWorkStorage _storage;

        private readonly IMerchantConfigRepository _merchantConfigRepository;

        #region Ctors

        public CreateMerchantConfigurationCommandHandler(IUnitOfWorkStorage storage, IMerchantConfigRepository merchantConfigRepository)
        {
            if (storage == null) throw new ArgumentNullException("storage", "UnitOfWorkStorage cannot be passed null");

            if (merchantConfigRepository == null) throw new ArgumentNullException("Merchant config repository cannot be passed null");

            _storage = storage;

            _merchantConfigRepository = merchantConfigRepository;
        }

        #endregion

        public void Handle(CreateMerchantConfigurationCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("ConfigurationMerchantCommand command cannot be passed null");

            using (_logger.Push())
            {
                _logger.Info("Handling command: ConfigurationMerchantCommand", command);

                using (var uow = _storage.NewUnitOfWork())
                {
                    MerchantConfig configurationDetails = _merchantConfigRepository.GetByMerchant(command.MerchantId);

                    if (configurationDetails == null)
                    {
                        using (var tran = uow.BeginTransaction())
                        {
                            var merchantConfig = new MerchantConfig(command.MerchantId, Guid.NewGuid().ToString(), "ConfigurationMerchantCommandHandler", (int)VerificationMethod.Hmac);

                            _merchantConfigRepository.Save(merchantConfig);

                            tran.Commit();

                            _logger.Info("Merchant config", merchantConfig);
                        }
                    }
                    else
                    {
                        using (var tran = uow.BeginTransaction())
                        {
                            configurationDetails.ChangeSecret("ConfigurationMerchantCommandHandler");

                            _merchantConfigRepository.Update(configurationDetails);

                            tran.Commit();

                            _logger.Info("Merchant config", configurationDetails);
                        }
                    }

                    _logger.Info("Command execution complete : ConfigurationMerchantCommand", command);
                }
            }
        }
    }
}
