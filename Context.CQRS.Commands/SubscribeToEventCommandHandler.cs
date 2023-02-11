using Example.Common.Context.CQRS;
using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Logging;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.CQRS.Commands
{
    public class SubscribeToEventCommandHandler : ICommandHandler<SubscribeToEventCommand>
    {
        #region Properties and local variables

        ILog _logger = LogManager.GetLogger(typeof(SubscribeToEventCommandHandler));

        internal IUnitOfWorkStorage _storage;

        internal ISubscriptionRepository _subscriptionRepository;

        #endregion

        #region Ctors

        public SubscribeToEventCommandHandler(
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

        public void Handle(SubscribeToEventCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("Command cannot be passed null");

            using (_logger.Push())
            {
                _logger.Info("Handling command: SubscribeToEventCommand", command);

                using (var uow = _storage.NewUnitOfWork())
                {
                    Subscription subscription = new Subscription(command.MerchantId, command.EventType, command.DeliveryMethod, command.DeliveryAddress, command.Description, command.Subscriber, "SubscribeToEventCommandHandler");

                    _logger.Debug("Persisting Subscription", subscription);

                    using (var tran = uow.BeginTransaction())
                    {
                        _subscriptionRepository.Save(subscription);

                        tran.Commit();

                        command.SubscriptionId = subscription.Id;
                    }

                    _logger.Info("Command execution complete : SubscribeToEventCommand", command);
                }
            }
        }

    }
}
