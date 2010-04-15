using System.Collections.Generic;

using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.RegistrationSource
{
    [TestFixture]
    public class WhenDependencySelectorIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithAnything_TheSourceCannotHandle()
        {
            var registrationSource = this.GetRegistrationSource();

            Assert.That(registrationSource.DependencySelector(typeof(object)), Is.False);
            Assert.That(registrationSource.DependencySelector(typeof(IFoo)), Is.False);
            Assert.That(registrationSource.DependencySelector(typeof(Foo)), Is.False);
            Assert.That(registrationSource.DependencySelector(typeof(IEnumerable<object>)), Is.False);
        }
    }
}