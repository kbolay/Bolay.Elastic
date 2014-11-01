using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic.Api;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kgb.Elastic.Tests
{
    [TestClass]
    public class UnitTest_httpLayer
    {
        [TestMethod]
        public void PASS_Get_Google()
        {
            string uri = "http://www.google.com";
            HttpResponse response = new HttpLayer().Get(new HttpRequest(uri));

            Assert.IsNotNull(response);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Body);
            Assert.IsTrue(response.Body.ToLower().Contains("google"));
        }

        [TestMethod]
        public void PASS_Get_WithHeaders()
        {
            string uri = "http://www.google.com";
            HttpRequest request = new HttpRequest(uri);
            request.AddToHeaders("accept-language", "fr;q=1.0");
            HttpResponse response = new HttpLayer().Get(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Body);
            Assert.IsTrue(response.Body.ToLower().Contains("google"));
        }
    }
}
