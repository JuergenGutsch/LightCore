using System;
using System.Collections.Generic;
using System.Reflection;

using LightCore.Registration;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Activation.ArgumentCollector
{
    [TestFixture]
    public class WhenCollectArgumentsIsCalled
    {
        private object[] GetArgumentsWith(ParameterInfo[] parameters, ArgumentContainer arguments, ArgumentContainer runtimeArguments, Func<Type, object> dependencyResolver)
        {
            return this.GetArgumentsWith(parameters, arguments, runtimeArguments, dependencyResolver, new Type[] { });
        }

        private object[] GetArgumentsWith(ParameterInfo[] parameters, ArgumentContainer arguments, ArgumentContainer runtimeArguments, Func<Type, object> dependencyResolver, params Type[] registeredTypes)
        {
            var resolutionContext = new LightCore.Activation.ResolutionContext(
                null,
                RegistrationHelper.GetRegistrationContainerFor(registeredTypes),
                arguments,
                runtimeArguments);

            var argumentCollector = new LightCore.Activation.Components.ArgumentCollector();

            return argumentCollector.CollectArguments(dependencyResolver, parameters, resolutionContext);
        }

        [Test]
        public void WithNoDependencyParameterAndNoArguments_EmptyArgumentsReturned()
        {
            object[] arguments = this.GetArgumentsWith(
                new ParameterInfo[] { },
                new ArgumentContainer(),
                new ArgumentContainer(),
                t => new object()
                );

            Assert.AreEqual(new object[] { }, arguments);
        }

        [Test]
        public void WithDependecyParametersAndNoArguments_ADependencyInstanceReturned()
        {
            object[] arguments = this.GetArgumentsWith(
                typeof(Foo).GetConstructor(new[] { typeof(IBar) }).GetParameters(),
                new ArgumentContainer(),
                new ArgumentContainer(),
                t => new Bar(),
                typeof(IBar));

            Assert.AreEqual(1, arguments.Length);
            Assert.IsInstanceOf<Bar>(arguments[0]);
        }

        [Test]
        public void WithStringAndBooleanArguments_AStringAndBooleanArgumentReturned()
        {
            object[] arguments = this.GetArgumentsWith(
                typeof(Foo).GetConstructor(new[]
                                                {
                                                    typeof (string),
                                                    typeof (bool)
                                                }).GetParameters(),
                new ArgumentContainer { AnonymousArguments = new object[] { "Peter", true } },
                new ArgumentContainer(),
                t => new object());

            Assert.AreEqual(2, arguments.Length);

            Assert.AreEqual("Peter", arguments[0]);
            Assert.AreEqual(true, arguments[1]);
        }

        [Test]
        public void WithDependencyAndStringArguments_ADependecyInstanceAndStringArgumentReturned()
        {
            object[] arguments = this.GetArgumentsWith(
                typeof(Foo).GetConstructor(new[]
                                                {
                                                    typeof (IBar),
                                                    typeof (string)
                                                }).GetParameters(),
                new ArgumentContainer
                    {
                        AnonymousArguments =
                            new object[] { "Peter" }
                    },
                new ArgumentContainer(),
                t => new Bar(),
                typeof(IBar));

            Assert.AreEqual(2, arguments.Length);

            Assert.IsInstanceOf<Bar>(arguments[0]);
            Assert.AreEqual("Peter", arguments[1]);
        }

        [Test]
        public void WithAnonymousAndNamedStringArgument_NamedArgumentIsPrioredAndReturned()
        {
            object[] arguments = this.GetArgumentsWith(
                typeof(Foo).GetConstructor(new[] { typeof(string) }).GetParameters(),
                new ArgumentContainer
                    {
                        AnonymousArguments = new[] { "fail" },
                        NamedArguments = new Dictionary<string, object> { { "arg1", "success" } }
                    },
                new ArgumentContainer(),
                null);

            Assert.AreEqual("success", arguments[0]);
        }
    }
}