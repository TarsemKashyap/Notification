using Example.Notific.Context.Domain.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Notific.Context.Domain.Test.Infrastructure
{
    [TestClass]
    public class HttpPoster_TestClass
    {

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HttpPoster_Url_Null()
        {
            HttpPoster httpPost = new HttpPoster();

            var actual = httpPost.Post(null, "{a:\"test\",b:\"data\" }");           
        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(ArgumentNullException))]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Ignore]
        public void HttpPoster_Data_Null()
        {
            HttpPoster httpPost = new HttpPoster();

            string endPoint = ConfigurationManager.AppSettings["ReqApiUrl"].ToString() + "OKRequest";

            var actual = httpPost.Post(endPoint, null);           
        }

        [TestMethod]
        [TestCategory("Integration")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Ignore]
        public void HttpPoster_handle()
        {
            HttpPoster httpPost = new HttpPoster();

            string endPoint = ConfigurationManager.AppSettings["ReqApiUrl"].ToString() + "OKRequest";

            var actual = httpPost.Post(endPoint, "{a:\"test\",b:\"data\" }");

            Assert.AreEqual(actual.HttpStatusCode, 200, "The status code should OK (200)");
        }

        [TestMethod]
        [TestCategory("Integration")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Ignore]
        public void HttpPoster_BadUrl()
        {
            HttpPoster httpPost = new HttpPoster();

            string endPoint = ConfigurationManager.AppSettings["ReqApiUrl"].ToString() + "NotFoundRequest";

            var actual = httpPost.Post(endPoint, "{a:\"test\",b:\"data\" }");

            Assert.AreEqual(actual.HttpStatusCode, 404, "The status code should Url not found (404)");
        }

        [TestMethod]
        [TestCategory("Integration")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Ignore]
        public void HttpPoster_UrlTimeOut()
        {
            HttpPoster httpPost = new HttpPoster();

            string endPoint = ConfigurationManager.AppSettings["ReqApiUrl"].ToString() + "BadRequest";

            var actual = httpPost.Post(endPoint, "{a:\"test\",b:\"data\" }");

            Assert.AreEqual(actual.HttpStatusCode, 400, "The status code should Bad Request (400)");
        }
    }
}
