using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Infrastructure.Interfaces
{
    public interface INotificationSigGenerator
    {
        string ComputeHMACSHA256(string key, string message);
    }
}
