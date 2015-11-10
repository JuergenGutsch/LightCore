using System;
using System.Collections.Generic;
using FluentAssertions;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Registration.RegistrationSource.FactoryRegistrationSource
{
    public class WhenDependencySelectorIsCalled : RegistrationSourceFixture
    {
        [Fact]
        public void WithNoFunctionType_TheSourceCannotHandle()
        {
            var registrationSource = GetFactoryRegistrationSource(typeof (object));

            var actual = registrationSource.SourceSupportsTypeSelector(typeof (IEnumerable<object>));

            actual.Should().BeFalse();
        }

        [Fact]
        public void WithFunctionType_TheSourceCanHandle()
        {
            var registrationSource = GetFactoryRegistrationSource(typeof (IFoo));

            var actual = registrationSource.SourceSupportsTypeSelector(typeof (Func<IFoo>));

            actual.Should().BeTrue();
        }

        [Fact]
        public void WithFunctionTypePlusArguments_TheSourceCanHandle()
        {
            var registrationSource = GetFactoryRegistrationSource(typeof (IFoo));

            var actual = registrationSource.SourceSupportsTypeSelector(typeof (Func<string, IFoo>));

            actual.Should().BeTrue();
        }

        [Fact]
        public void WithFunctionTypePlusTwoArguments_TheSourceCanHandle()
        {
            var registrationSource = GetFactoryRegistrationSource(typeof (IFoo));

            var actual = registrationSource.SourceSupportsTypeSelector(typeof (Func<string, bool, IFoo>));

            actual.Should().BeTrue();
        }
    }
}