using Example.Common.Context.Bus;
using Example.Common.Context.CQRS;
using Example.Common.Logging;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.Contract.CQRS.Exceptions;
using Example.Notific.Context.CQRS.Bus;
using log4net;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Example.Notific.AsyncCommandHandler
{
    public class RetryNotificationByHttpPostBusCommandHandler : IHandleMessages<RetryNotificationByHttpPostBusCommand>
    {
        public IBus Bus { get; set; }
        IServiceProvider _container;

        #region Locals

        ILog _logger = LogManager.GetLogger(typeof(RetryNotificationByHttpPostBusCommandHandler));

        ICommandHandler<RetryNotificationByHttpPostCommand> _commandHandler;

        IBusAdaptor _adapter;

        #endregion

        public RetryNotificationByHttpPostBusCommandHandler(IBus bus, IBusAdaptor busAdaptor, IServiceProvider container)
        {
            if (busAdaptor == null) throw new ArgumentNullException("Bus adapter cannot be passed null");
            if (bus == null) throw new ArgumentNullException("Bus cannot be passed null");
            if (container == null) throw new ArgumentNullException("Container cannot be passed null");

            busAdaptor.AttachBus(bus);

            _adapter = busAdaptor;

            _container = container;

            _commandHandler = _container.GetService(typeof(ICommandHandler<RetryNotificationByHttpPostCommand>)) as ICommandHandler<RetryNotificationByHttpPostCommand>;

            if (_commandHandler == null)
            {
                _logger.Error("Failed to get ICommandHandler<RetryNotificationByHttpPostCommand> from container");
                throw new ArgumentNullException("Failed to get ICommandHandler<RetryNotificationByHttpPostCommand> from container");
            }

        }

        public void Handle(RetryNotificationByHttpPostBusCommand message)
        {
            using (_logger.Push())
            {
                _logger.Info("Handling message: RetryNotificationByHttpPostCommand", message);

                using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    try
                    {
                        _commandHandler.Handle(message as RetryNotificationByHttpPostCommand);
                    }
                    catch (NotificationNotFoundException ex)
                    {
                        _logger.Warn("NotificationNotFoundException caught handling a RetryNotificationByHttpPostCommand command", message, ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        _logger.Warn("InvalidOperationException caught handling a RetryNotificationByHttpPostCommand command", message, ex);
                    }                   
                    catch (CommandNotValidException ex)
                    {
                        _logger.Error("CommandNotValidException caught handling a SendNotificationByHttpPostBusCommand command", message, ex);
                    }
                    catch (CommandTimeoutException ex)
                    {
                        _logger.Warn("CommandTimeoutException caught handling a SendNotificationByHttpPostBusCommand command", message, ex);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Faield to process command", ex);
                        throw;
                    }
                }
            }
        }
    }
}
