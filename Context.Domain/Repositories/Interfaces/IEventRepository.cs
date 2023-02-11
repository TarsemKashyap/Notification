using Example.Common.Context.DDD.Persistence;
using Example.Notific.Context.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Repositories.Interfaces
{
    public interface IEventRepository : IRepositoryWithTypedId<Event, Guid>
    {
    }
}
