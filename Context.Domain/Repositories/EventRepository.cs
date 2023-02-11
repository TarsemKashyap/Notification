using Example.Common.Context.DDD.UnitOfWork;
using Example.Common.Context.Infrastructure.NHibernate.Repositories;
using Example.Notific.Context.Domain.Model;
using Example.Notific.Context.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Repositories
{
    public class EventRepository : NHRepositoryWithTypedId<Event, Guid>, IEventRepository
    {
        #region Ctors

        public EventRepository(IUnitOfWorkStorage storage)
            : base(storage)
        {
        }

        #endregion
    }
}
