using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.SimpleInjector.Test
{
    [TestClass]
    public class NotBootstrapper_TestClass
    {
        [TestMethod]
        [Ignore]
        public void NotBootstrapper_Bootstrap()
        {
            Container container = new Container();

            NotBootstrapper.Bootstrap(container, null, true);
        }
    }
}
