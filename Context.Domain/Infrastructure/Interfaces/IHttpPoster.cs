using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Domain.Infrastructure.Interfaces
{
    public interface IHttpPoster
    {
        /// <summary>
        /// Post the data to endpoint
        /// </summary>
        /// <param name="endpoint">Endpoint</param>
        /// <param name="data">Json data</param>
        /// <returns>Return the resposne code and message</returns>
        HttpPostResult Post(string endpoint, string data);
        HttpPostResult Post(string endpoint, string data, string notSignature);
    }
}
