using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerMonitor.Extensions;

namespace PowerMonitorTests
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void WhenStringIsNull_NullShouldBeReturned()
        {
            string text = null;

            Assert.AreEqual(text.Tail(5), null);
        }

        [TestMethod]
        public void WhenStringIsEmpty_EmptyStringShouldBeReturned()
        {
            var text = string.Empty;

            Assert.AreEqual(text.Tail(5), string.Empty);
        }


        [TestMethod]
        public void WhenStringIsShorterThanLength_StringShouldBeReturned()
        {
            var text = "1";

            Assert.AreEqual(text.Tail(5), text);
        }

        [TestMethod]
        public void WhenStringIsLongerThanLength_SubstringShouldBeReturned()
        {
            var text = "abcde12345";

            Assert.AreEqual(text.Tail(5), "12345");
        }
    }
}
