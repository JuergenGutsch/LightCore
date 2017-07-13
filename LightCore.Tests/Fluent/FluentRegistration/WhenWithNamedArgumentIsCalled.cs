using System.Collections.Generic;
using FluentAssertions;
using LightCore.Registration;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Fluent.FluentRegistration
{
    public class WhenWithNamedArgumentIsCalled : FluentFixture
    {
        [Fact]
        public void WithNull_ArgumentCountStaysOnZero()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = GetRegistration(registrationItem);

            fluentRegistration.WithNamedArguments(null);

            registrationItem.Arguments.CountOfAllArguments.Should().Be(0);
        }

        [Fact]
        public void WithOneNamedFooArgument_TheArgumentIsTheSame()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = GetRegistration(registrationItem);
            var key = "test";
            var foo = new Foo();

            fluentRegistration.WithNamedArguments(new Dictionary<string, object> {{key, foo}});

            registrationItem.Arguments.NamedArguments.Count.Should().Be(1);
            registrationItem.Arguments.NamedArguments[key].Should().BeSameAs(foo);
        }

        [Fact]
        public void WithTwoNamedAnonymousTypeArguments_TheArgumentsAreCommited()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = GetRegistration(registrationItem);

            fluentRegistration.WithNamedArguments(new {Foo = "foo", Bar = new Bar()});

            registrationItem.Arguments.NamedArguments.Count.Should().Be(2);
            registrationItem.Arguments.NamedArguments["Foo"].Should().BeOfType<string>();
            registrationItem.Arguments.NamedArguments["Bar"].Should().BeOfType<Bar>();
        }
    }
}