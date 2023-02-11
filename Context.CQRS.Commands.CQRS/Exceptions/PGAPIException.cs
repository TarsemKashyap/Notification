using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Exceptions
{
    public class PGAPIException : Exception
    {
        #region Ctors

        public PGAPIException()
            : base("Unexpecting exception resolving PG API")
        {
        }

        #endregion
    }
}
