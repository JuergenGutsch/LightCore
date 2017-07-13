using System;
using FluentAssertions;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Registration.RegistrationSource.LazyRegistrationSource
{
    public class WhenDependencySelectorIsCalled : RegistrationSourceFixture
    {
        [Fact]
        public void WithNoLazyType_TheSourceCannotHandle()
        {
            var registrationSource = GetLazyRegistrationSource(typeof (object));

            var actual = registrationSource.SourceSupportsTypeSelector(typeof (Func<IFoo>));

            actual.Should().BeFalse();
        }

        [Fact]
        public void WithLazyType_TheSourceCanHandle()
        {
            var registrationSource = GetLazyRegistrationSource(typeof (IFoo));

            var actual = registrationSource.SourceSupportsTypeSelector(typeof (Lazy<IFoo>));

            actual.Should().BeTrue();
        }

        [Fact]
        public void WithWithLazyTypeButNoRegisteredDependency_TheSourceCannotHandle()
        {
            var registrationSource = GetLazyRegistrationSource(typeof (string));

            var actual = registrationSource.SourceSupportsTypeSelector(typeof (Lazy<IFoo>));

            actual.Should().BeFalse();
        }
    }
}