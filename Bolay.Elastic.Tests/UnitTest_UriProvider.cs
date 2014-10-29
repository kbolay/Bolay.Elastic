using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic;
using Bolay.Elastic.Interfaces;

namespace Kgb.Elastic.Tests
{
    [TestClass]
    public class UnitTest_UriProvider
    {
        [TestMethod]
        public void PASS_CreateUriProvider_String()
        {
            IUriProvider uriProvider = new ElasticUriProvider("http://www.google.com");
            Assert.IsNotNull(uriProvider);
            Assert.IsNotNull(uriProvider.Uri);
        }

        [TestMethod]
        public void PASS_CreateUriProvider_Uri()
        {
            IUriProvider uriProvider = new ElasticUriProvider(new Uri("http://www.google.com"));
            Assert.IsNotNull(uriProvider);
            Assert.IsNotNull(uriProvider.Uri);
        }

        [TestMethod]
        public void FAIL_CreateUriProvider_BadUri()
        {
            try
            {
                IUriProvider uriProvider = new ElasticUriProvider("asdf;lkjasdf");
                Assert.Fail();
            }
            catch (UriFormatException ex)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void FAIL_CreateUriProvider_EmptyString()
        {
            try
            {
                IUriProvider uriProvider = new ElasticUriProvider("");
                Assert.Fail();
            }
            catch (UriFormatException ex)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void FAIL_CreateUriProvider_Null()
        {
            try
            {
                IUriProvider uriProvider = new ElasticUriProvider(new Uri(null));
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
