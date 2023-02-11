using Example.Common.Context.Bus;
using Example.Common.Context.CQRS;
using Example.Common.Logging;
using Example.Notific.Context.Contract.CQRS.Commands;
using Example.Notific.Context.CQRS.Bus;
using log4net;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Example.Service.Contract;
using Example.Context.Contract;
using Example.Notific.MaS.WebApi.Client;

namespace Example.Notific.AsyncCommandHandler
{
    public class SendNotificationRetiresScheduledTaskBusCommandHandler : IHandleMessages<SendNotificationRetiresScheduledTaskBusCommand>
    {
        public IBus Bus { get; set; }
        IServiceProvider _container;
        IMaSApiClient _masClient;

        #region Locals

        ILog _logger = LogManager.GetLogger(typeof(SendNotificationRetiresScheduledTaskBusCommandHandler));

        ICommandHandler<SendNotificationRetiresScheduledTaskCommand> _commandHandler;

        IBusAdaptor _adapter;

        #endregion

        public SendNotificationRetiresScheduledTaskBusCommandHandler(IBus bus, IBusAdaptor busAdaptor, IServiceProvider container)
        {
            if (busAdaptor == null) throw new ArgumentNullException("Bus adapter cannot be passed null");
            if (bus == null) throw new ArgumentNullException("Bus cannot be passed null");
            if (container == null) throw new ArgumentNullException("Container cannot be passed null");

            busAdaptor.AttachBus(bus);

            _adapter = busAdaptor;

            _container = container;

            _commandHandler = _container.GetService(typeof(ICommandHandler<SendNotificationRetiresScheduledTaskCommand>)) as ICommandHandler<SendNotificationRetiresScheduledTaskCommand>;

            _masClient = _container.GetService(typeof(IMaSApiClient)) as IMaSApiClient;

            if (_commandHandler == null)
            {
                _logger.Error("Failed to get ICommandHandler<SendNotificationRetiresScheduledTaskCommand> from container");
                throw new ArgumentNullException("Failed to get ICommandHandler<SendNotificationRetiresScheduledTaskCommand> from container");
            }

            if (_masClient == null)
            {
                _logger.Error("Failed to get IMaSApiClient from container");
                throw new ArgumentNullException("Failed to get IMaSApiClient from container");
            }

        }

        public void Handle(SendNotificationRetiresScheduledTaskBusCommand message)
        {
            using (_logger.Push())
            {
                _logger.Info("Handling message: SendNotificationRetiresScheduledTaskBusCommand", message);

                using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    SchedulingJobResultResponse masResponse = null;

                    try
                    {
                        _commandHandler.Handle(message as SendNotificationRetiresScheduledTaskCommand);

                        masResponse = new SchedulingJobResultResponse()
                        {
                            Result = ExecutionResultEnum.Success,
                            TimeFinished = DateTime.Now
                        };
                    }
                    catch (CommandNotValidException ex)
                    {
                        _logger.Error("CommandNotValidException caught handling a SendNotificationRetiresScheduledTaskCommand command", message, ex);
                    }
                    catch (CommandTimeoutException ex)
                    {
                        _logger.Warn("CommandTimeoutException caught handling a SendNotificationRetiresScheduledTaskCommand command", message, ex);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Faield to process command", ex);

                        masResponse = new SchedulingJobResultResponse()
                        {
                            AdditionalData = "Internal error",
                            Result = ExecutionResultEnum.Failure,
                            TimeFinished = DateTime.Now
                        };
                    }

                    try
                    {
                        _logger.Info("Going to submit execution result to MaS", new { Result = masResponse, Uri = message.CallbackUrl });

                        var result = _masClient.SubmitExecutionResult(message.CallbackUrl, masResponse);
                        _logger.Info("Submit Execution Result", new { Result = result });
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Failed to submit job execution result to MaS", message, ex);
                    }
                }
            }
        }
    }
}
