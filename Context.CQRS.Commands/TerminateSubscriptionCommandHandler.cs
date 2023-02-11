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
    public class TerminateSubscriptionCommandHandler : ICommandHandler<TerminateSubscriptionCommand>
    {
        #region Properties and local variables

        ILog _logger = LogManager.GetLogger(typeof(TerminateSubscriptionCommandHandler));

        internal IUnitOfWorkStorage _storage;

        internal ISubscriptionRepository _subscriptionRepository;

        #endregion

        #region Ctors

        public TerminateSubscriptionCommandHandler(
            IUnitOfWorkStorage storage,
            ISubscriptionRepository subscriptionRepository)
        {
            #region check for nulls

            if (storage == null) throw new ArgumentNullException("Unit of work storage cannot be passed null");

            if (subscriptionRepository == null) throw new ArgumentNullException("Subscription repository cannot be passed null");

            #endregion

            _storage = storage;

            _subscriptionRepository = subscriptionRepository;
        }

        #endregion

        public void Handle(TerminateSubscriptionCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("Command cannot be passed null");

            using (_logger.Push())
            {
                _logger.Info("Handling command: TerminateSubscriptionCommand", command);

                using (var uow = _storage.NewUnitOfWork())
                {
                    Subscription subscription = _subscriptionRepository.Get(command.SubscriptionId);

                    CheckSubscriptionExistence(subscription, command.SubscriptionId);

                    using (var tran = uow.BeginTransaction())
                    {
                        subscription.Terminate("TerminateSubscriptionCommandHandler");

                        _subscriptionRepository.Update(subscription);

                        tran.Commit();                       
                    }

                    _logger.Info("Command execution complete : TerminateSubscriptionCommand", command);
                }
            }
        }

        private void CheckSubscriptionExistence(Subscription subscription, long subscriptionId)
        {
            using (_logger.Push())
            {
                if (subscription == null || subscription.SubscriptionTerminated==true)
                {
                    _logger.Warn("Subscription not found. Id: " + subscriptionId);
                    throw new SubscriptionNotFoundException(subscriptionId);
                }
            }
        }
    }
}
