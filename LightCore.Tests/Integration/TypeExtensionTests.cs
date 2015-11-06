using System;
using FluentAssertions;
using LightCore.TestTypes;


using LightCore.ExtensionMethods.System;
using Xunit;

namespace LightCore.Tests.Integration
{
    
    public class TypeExtensionTests
    {
        [Fact]
        public void FooFooImplIsAConcreteType()
        {
            typeof (Foo).IsConcreteType().Should().BeTrue();
        }

        [Fact]
        public void FooFooIsNotAConcreteType()
        {
            typeof (FooBase).IsConcreteType().Should().BeFalse();
        }

        [Fact]
        public void IFooBarIsNotAConcreteType()
        {
            typeof(IFoo).IsConcreteType().Should().BeFalse();
        }

        [Fact]
        public void StringIsNotAConcreteType()
        {
            typeof(string).IsConcreteType().Should().BeFalse();
        }

        [Fact]
        public void DateTimeIsNotAConcreteType()
        {
            typeof(DateTime).IsConcreteType().Should().BeFalse();
        }

        [Fact]
        public void CharIsNotAConcreteType()
        {
            typeof(char).IsConcreteType().Should().BeFalse();
        }

        [Fact]
        public void GuidIsNotAConcreteType()
        {
            typeof(Guid).IsConcreteType().Should().BeFalse();
        }

        [Fact]
        public void IntIsNotAConcreteType()
        {
            typeof(int).IsConcreteType().Should().BeFalse();
        }

        [Fact]
        public void NullableIntIsNotAConcreteType()
        {
            typeof(int?).IsConcreteType();
        }
    }
}