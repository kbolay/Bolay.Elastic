using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bolay.Elastic;
using Bolay.Elastic.Interfaces;

namespace Kgb.Elastic.Tests
{
    [TestClass]
    public class UnitTest_ConnectionStringProvider
    {
        [TestMethod]
        public void PASS_CreateConnectionStringProvider()
        {
            IConnectionStringProvider provider = new ConnectionStringProvider("stuff");
            Assert.IsNotNull(provider);
            Assert.IsNotNull(provider.ConnectionString);
            Assert.AreEqual("stuff", provider.ConnectionString);
        }

        [TestMethod]
        public void FAIL_CreateConnectionStringProvider_EmptyString()
        {
            try
            {
                IConnectionStringProvider provider = new ConnectionStringProvider("");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(ArgumentNullException), ex.GetType());
            }
        }

        [TestMethod]
        public void FAIL_CreateConnectionStringProvider_Null()
        {
            try
            {
                IConnectionStringProvider provider = new ConnectionStringProvider(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(ArgumentNullException), ex.GetType());
            }
        }
    }
}
