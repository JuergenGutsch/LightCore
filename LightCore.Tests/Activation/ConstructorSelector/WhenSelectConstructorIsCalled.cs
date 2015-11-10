using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using LightCore.Activation;
using LightCore.Registration;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Activation.ConstructorSelector
{
    public class WhenSelectConstructorIsCalled
    {
        private ConstructorInfo Select(IEnumerable<ConstructorInfo> constructors, ArgumentContainer arguments)
        {
            return Select(constructors, arguments, new Type[] {});
        }

        private ConstructorInfo Select(IEnumerable<ConstructorInfo> constructors, params Type[] registeredTypes)
        {
            return Select(constructors, new ArgumentContainer(), registeredTypes);
        }

        private ConstructorInfo Select(IEnumerable<ConstructorInfo> constructors, ArgumentContainer arguments,
            params Type[] registeredTypes)
        {
            var selector = new LightCore.Activation.Components.ConstructorSelector();

            var resolutionContext =
                new ResolutionContext(
                    null,
                    RegistrationHelper.GetRegistrationContainerFor(registeredTypes),
                    arguments,
                    new ArgumentContainer());

            return selector
                .SelectConstructor(constructors, resolutionContext);
        }

        [Fact]
        public void WithFooAndNoneRuntimeArguments_DefaultConstructorWasUsed()
        {
            var finalConstructor = Select(
                typeof (Foo).GetConstructors());

            finalConstructor.Should().NotBeNull();
            finalConstructor.Should().BeSameAs(typeof (Foo).GetConstructor(Type.EmptyTypes));
        }

        [Fact]
        public void WithFooAndNoneArguments_DefaultConstructorWasUsed()
        {
            var finalConstructor = Select(
                typeof (Foo).GetConstructors());

            finalConstructor.Should().NotBeNull();
            finalConstructor.Should().BeSameAs(typeof (Foo).GetConstructor(Type.EmptyTypes));
        }

        [Fact]
        public void WithFooAndIBarAsArgument_IBarConstructorWasUsed()
        {
            var finalConstructor = Select(
                typeof (Foo).GetConstructors(),
                typeof (IBar));

            finalConstructor.Should().BeSameAs(typeof (Foo).GetConstructor(new[] {typeof (IBar)}));
        }

        [Fact]
        public void WithFooAndIBarAndAStringAsArguments_IBarAndStringConstructorWasUsed()
        {
            var finalConstructor = Select(
                typeof (Foo).GetConstructors(),
                new ArgumentContainer
                {
                    AnonymousArguments = new object[] {"Peter"}
                },
                typeof (IBar));

            finalConstructor.Should().BeSameAs(typeof (Foo).GetConstructor(new[] {typeof (IBar), typeof (string)}));
        }

        [Fact]
        public void WithFooAndABoolAsArgument_BoolConstructorWasUsed()
        {
            var finalConstructor = Select(
                typeof (Foo).GetConstructors(),
                new ArgumentContainer
                {
                    AnonymousArguments = new object[] {true}
                });

            finalConstructor.Should().BeSameAs(typeof (Foo).GetConstructor(new[] {typeof (bool)}));
        }
    }
}