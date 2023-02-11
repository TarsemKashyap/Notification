using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Logging;
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
    public class UpdateMerchantConfigurationSecretCommandHandler : ICommandHandler<UpdateMerchantConfigurationSecretCommand>
    {
        private static ILog _logger = LogManager.GetLogger(typeof(UpdateMerchantConfigurationSecretCommandHandler));

        private readonly IUnitOfWorkStorage _storage;

        private readonly IMerchantConfigRepository _merchantConfigRepository;

        #region Ctors

        public UpdateMerchantConfigurationSecretCommandHandler(IUnitOfWorkStorage storage, IMerchantConfigRepository merchantConfigRepository)
        {
            if (storage == null) throw new ArgumentNullException("storage", "UnitOfWorkStorage cannot be passed null");

            if (merchantConfigRepository == null) throw new ArgumentNullException("Merchant config repository cannot be passed null");

            _storage = storage;

            _merchantConfigRepository = merchantConfigRepository;
        }

        #endregion

        public void Handle(UpdateMerchantConfigurationSecretCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("UpdateMerchantConfigurationSecretCommand command cannot be passed null");

            using (_logger.Push())
            {
                _logger.Info("Handling command: UpdateMerchantConfigurationSecretCommand", command);

                using (var uow = _storage.NewUnitOfWork())
                {
                    MerchantConfig configurationDetails = _merchantConfigRepository.GetByMerchant(command.MerchantId);

                    if (configurationDetails == null)
                    {
                        _logger.Warn("Merchant configuration not found. Id: " + command.MerchantId);
                        throw new ConfigurationNotFoundException(command.MerchantId);
                    }

                    using (var tran = uow.BeginTransaction())
                    {
                        configurationDetails.ChangeSecret("UpdateMerchantConfigurationSecretCommandHandler");

                        _merchantConfigRepository.Update(configurationDetails);

                        tran.Commit();
                    }

                    _logger.Info("Command execution complete : UpdateMerchantConfigurationSecretCommand", command);
                }
            }
        }
    }
}
