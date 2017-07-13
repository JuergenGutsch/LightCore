using System.Collections.Generic;
using FluentAssertions;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Registration.RegistrationSource.OpenGenericRegistrationSource
{
    public class WhenDependencySelectorIsCalled : RegistrationSourceFixture
    {
        [Fact]
        public void WithNull_TheSourceCannotHandle()
        {
            var registrationSource = GetOpenGenericRegistrationSource(typeof (object), typeof (object));

            var actual = registrationSource.SourceSupportsTypeSelector(typeof (object));

            actual.Should().BeFalse();
        }

        [Fact]
        public void WithNoOpenGenericType_TheSourceCannotHandle()
        {
            var registrationSource = GetOpenGenericRegistrationSource(typeof (object), typeof (object));

            var actual = registrationSource.SourceSupportsTypeSelector(typeof (List<object>));

            actual.Should().BeFalse();
        }

        [Fact]
        public void WithOpenGenericTypeRegistered_TheSourceCanHandle()
        {
            var registrationSource = GetOpenGenericRegistrationSource(typeof (IList<>), typeof (List<>));

            var actual = registrationSource.SourceSupportsTypeSelector(typeof (IList<object>));

            actual.Should().BeTrue();
        }

        [Fact]
        public void WithOpenGenericTypeAndTwoTypeArguments_TheSourceCanHandle()
        {
            var registrationSource = GetOpenGenericRegistrationSource(typeof (IRepository<,>), typeof (Repository<>));

            var actual = registrationSource.SourceSupportsTypeSelector(typeof (IRepository<Foo, int>));

            actual.Should().BeTrue();
        }
    }
}