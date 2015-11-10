using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using LightCore.Activation;
using LightCore.Registration;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Activation.ArgumentCollector
{
    public class WhenCollectArgumentsIsCalled
    {
        private object[] GetArgumentsWith(ParameterInfo[] parameters, ArgumentContainer arguments,
            ArgumentContainer runtimeArguments, Func<Type, object> dependencyResolver)
        {
            return GetArgumentsWith(parameters, arguments, runtimeArguments, dependencyResolver, new Type[] {});
        }

        private object[] GetArgumentsWith(ParameterInfo[] parameters, ArgumentContainer arguments,
            ArgumentContainer runtimeArguments, Func<Type, object> dependencyResolver, params Type[] registeredTypes)
        {
            var resolutionContext = new ResolutionContext(
                null,
                RegistrationHelper.GetRegistrationContainerFor(registeredTypes),
                arguments,
                runtimeArguments);

            var argumentCollector = new LightCore.Activation.Components.ArgumentCollector();

            return argumentCollector.CollectArguments(dependencyResolver, parameters, resolutionContext);
        }

        [Fact]
        public void WithNoDependencyParameterAndNoArguments_EmptyArgumentsReturned()
        {
            var arguments = GetArgumentsWith(
                new ParameterInfo[] {},
                new ArgumentContainer(),
                new ArgumentContainer(),
                t => new object()
                );

            arguments.Should().BeEmpty();
        }

        [Fact]
        public void WithDependecyParametersAndNoArguments_ADependencyInstanceReturned()
        {
            var arguments = GetArgumentsWith(
                typeof (Foo).GetConstructor(new[] {typeof (IBar)}).GetParameters(),
                new ArgumentContainer(),
                new ArgumentContainer(),
                t => new Bar(),
                typeof (IBar));

            arguments.Length.Should().Be(1);
            arguments[0].Should().BeOfType<Bar>();
        }

        [Fact]
        public void WithStringAndBooleanArguments_AStringAndBooleanArgumentReturned()
        {
            var arguments = GetArgumentsWith(
                typeof (Foo).GetConstructor(new[]
                {
                    typeof (string),
                    typeof (bool)
                }).GetParameters(),
                new ArgumentContainer {AnonymousArguments = new object[] {"Peter", true}},
                new ArgumentContainer(),
                t => new object());


            arguments.Length.Should().Be(2);
            arguments[0].Should().Be("Peter");
            arguments[1].Should().Be(true);
        }

        [Fact]
        public void WithDependencyAndStringArguments_ADependecyInstanceAndStringArgumentReturned()
        {
            var arguments = GetArgumentsWith(
                typeof (Foo).GetConstructor(new[]
                {
                    typeof (IBar),
                    typeof (string)
                }).GetParameters(),
                new ArgumentContainer
                {
                    AnonymousArguments =
                        new object[] {"Peter"}
                },
                new ArgumentContainer(),
                t => new Bar(),
                typeof (IBar));


            arguments.Length.Should().Be(2);
            arguments[0].Should().BeOfType<Bar>();
            arguments[1].Should().Be("Peter");
        }

        [Fact]
        public void WithAnonymousAndNamedStringArgument_NamedArgumentIsPrioredAndReturned()
        {
            var arguments = GetArgumentsWith(
                typeof (Foo).GetConstructor(new[] {typeof (string)}).GetParameters(),
                new ArgumentContainer
                {
                    AnonymousArguments = new[] {"fail"},
                    NamedArguments = new Dictionary<string, object> {{"arg1", "success"}}
                },
                new ArgumentContainer(),
                null);

            arguments[0].Should().Be("success");
        }
    }
}