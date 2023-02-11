using Example.Notific.Context.Common;
using Example.Notific.Context.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Infrastructure.Interfaces
{
    public interface INotificationJsonGenerator
    {
        /// <summary>
        /// Generate the JSON string for sending to merchant
        /// </summary>
        /// <param name="eventModel">Event model data</param>
        /// <returns>Returns the JSON string</returns>
        string Generate(Event eventModel);
        string Generate(Event eventModel, VerificationMethod verificationMethod);
    }
}
