using System;
using System.Collections.Generic;

using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.FactoryRegistrationSource
{
    [TestFixture]
    public class WhenDependencySelectorIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithNoFunctionType_TheSourceCannotHandle()
        {
            var registrationSource = this.GetFactoryRegistrationSource(typeof(object));

            Assert.That(registrationSource.SourceSupportsTypeSelector(typeof(IEnumerable<object>)), Is.False);
        }

        [Test]
        public void WithFunctionType_TheSourceCanHandle()
        {
            var registrationSource = this.GetFactoryRegistrationSource(typeof(IFoo));

            Assert.That(registrationSource.SourceSupportsTypeSelector(typeof(Func<IFoo>)), Is.True);
        }

        [Test]
        public void WithFunctionTypePlusArguments_TheSourceCanHandle()
        {
            var registrationSource = this.GetFactoryRegistrationSource(typeof(IFoo));

            Assert.That(registrationSource.SourceSupportsTypeSelector(typeof(Func<string, IFoo>)), Is.True);
        }

        [Test]
        public void WithFunctionTypePlusTwoArguments_TheSourceCanHandle()
        {
            var registrationSource = this.GetFactoryRegistrationSource(typeof(IFoo));

            Assert.That(registrationSource.SourceSupportsTypeSelector(typeof(Func<string, bool, IFoo>)), Is.True);
        }
    }
}