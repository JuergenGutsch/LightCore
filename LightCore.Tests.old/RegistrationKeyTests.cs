using System.Collections.Generic;

using LightCore.Registration;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests
{
    [TestFixture]
    public class RegistrationKeyTests
    {
        [Test]
        public void RegistrationKey_is_equal()
        {
            var key1 = new RegistrationKey(typeof(IFoo));
            var key2 = new RegistrationKey(typeof(IFoo));

            Assert.AreEqual(key1, key2);
            Assert.AreNotSame(key1, key2);
            Assert.IsTrue(key1 == key2);
            Assert.IsTrue(key1.Equals(key2));

            var dic = new Dictionary<RegistrationKey, object>();
            dic.Add(key1, "");

            Assert.IsTrue(dic.ContainsKey(key1));
            Assert.IsTrue(dic.ContainsKey(key2));
        }
    }
}