using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Contract.CQRS.Exceptions
{
    public class TPFWAPIException : Exception
    {
        #region Ctors

        public TPFWAPIException()
            : base("Unexpecting exception resolving TP Framework")
        {
        }

        #endregion
    }
}
