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
    public class RetrieveEventQueryHandler : IQueryHandler<RetrieveEventQuery, EventDto>
    {
        #region Properties and local variables

        private static ILog _logger = LogManager.GetLogger(typeof(RetrieveSubscriptionQueryHandler));

        internal IUnitOfWorkStorage _storage;

        internal IEventRepository _eventRepository;

        #endregion

        #region Ctors

        public RetrieveEventQueryHandler(IUnitOfWorkStorage storage, IEventRepository eventRepository)
        {
            if (storage == null) throw new ArgumentNullException("storage", "UnitOfWorkStorage cannot be passed null");

            if (eventRepository == null) throw new ArgumentNullException("Event repository cannot be passed null");

            _storage = storage;

            _eventRepository = eventRepository;
        }

        #endregion

        public EventDto Handle(RetrieveEventQuery query)
        {
            if (query == null)
                throw new ArgumentNullException("Query cannot be passed null");

            using (_logger.Push())
            {
                _logger.Info("Handling query : RetrieveEventQuery", query);

                try
                {
                    using (var uow = _storage.NewUnitOfWork())
                    {
                        using (var txn = uow.BeginTransaction())
                        {
                            Event eventDetails = _eventRepository.Get(query.EventId);

                            CheckEventExistence(eventDetails, query.EventId);

                            _logger.Debug("Event details", eventDetails);

                            var eventDto = DomainEventMapper.ToEventDto(eventDetails);

                            _logger.Info("Query executed: RetrieveEventQuery", eventDto);

                            return eventDto;
                        }
                    }

                }
                catch (SqlException ex)
                {
                    _logger.Warn("Exception occured: RetrieveEventQuery", ex);
                    // rethrow exception from context for timeout or deadlock
                    if (ex.Number == -2 || ex.Number == 1205)
                        throw new QueryTimeoutException(String.Format("Failed to retreive event details. Id: {0}", query.EventId));
                    else
                        throw;
                }
            }
        }

        private void CheckEventExistence(Event eventDetails, Guid eventId)
        {
            using (_logger.Push())
            {
                if (eventDetails == null)
                {
                    _logger.Warn("Event not found. Id: " + eventId);
                    throw new EventNotFoundException(eventId);
                }
            }
        }
    }
}
