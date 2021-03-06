﻿using System;
using FluentAssertions;
using LightCore.Activation;
using LightCore.Activation.Activators;
using Xunit;
using Moq;
using LightCore.Registration;
using LightCore.TestTypes;

namespace LightCore.Tests.Activation.Activators.DelegateActivator
{
    public class WhenActivateInstanceIsCalled
    {
        private IActivator GetActivator(Func<IContainer, object> activatorFunction)
        {
            return new LightCore.Activation.Activators.DelegateActivator(activatorFunction);
        }

        [Fact]
        public void WithFunctionAndEmptyResolutionContext_ObjectReturned()
        {
            Func<IContainer, object> activatorFunction = c => new object();
            var delegateActivator = GetActivator(activatorFunction);

            var result = delegateActivator.ActivateInstance(new ResolutionContext());

            result.Should().BeOfType<object>();
        }

        [Fact]
        public void WithFunctionAndFullBlownResolutionContext_IFooWithDependendenciesReturned()

        {
            Func<IContainer, object> activatorFunction = c => new Foo(c.Resolve<IBar>());
            var delegateActivator = this.GetActivator(activatorFunction);

            var containerMock = new Mock<IContainer>();
            containerMock
                .Setup(c => c.Resolve<IBar>())
                .Returns(new Bar());

            var result = (IFoo)delegateActivator.ActivateInstance(
                new ResolutionContext(containerMock.Object, new RegistrationContainer()));

            result.Should().NotBeNull();
            result.Bar.Should().NotBeNull();
        }
    }

    //internal interface IFoo
    //{
    //    IBar Bar { get; }
    //}

    //internal class Bar : IBar
    //{
    //}

    //internal interface IBar
    //{
    //}

    //internal class Foo : IFoo
    //{
    //    private IBar _bar;

    //    public Foo(IBar bar)
    //    {
    //        _bar = bar;
    //    }

    //    public IBar Bar
    //    {
    //        get => _bar;
    //    }
    //}
}