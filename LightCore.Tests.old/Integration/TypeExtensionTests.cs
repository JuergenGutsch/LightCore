using System;

using LightCore.TestTypes;

using NUnit.Framework;

using LightCore.ExtensionMethods.System;

namespace LightCore.Tests.Integration
{
    [TestFixture]
    public class TypeExtensionTests
    {
        [Test]
        public void FooFooImplIsAConcreteType()
        {
            Assert.IsTrue(typeof(Foo).IsConcreteType());
        }

        [Test]
        public void FooFooIsNotAConcreteType()
        {
            Assert.IsFalse(typeof(FooBase).IsConcreteType());
        }

        [Test]
        public void IFooBarIsNotAConcreteType()
        {
            Assert.IsFalse(typeof(IFoo).IsConcreteType());
        }

        [Test]
        public void StringIsNotAConcreteType()
        {
            Assert.IsFalse(typeof(string).IsConcreteType());
        }

        [Test]
        public void DateTimeIsNotAConcreteType()
        {
            Assert.IsFalse(typeof(DateTime).IsConcreteType());
        }

        [Test]
        public void CharIsNotAConcreteType()
        {
            Assert.IsFalse(typeof(char).IsConcreteType());
        }

        [Test]
        public void GuidIsNotAConcreteType()
        {
            Assert.IsFalse(typeof(Guid).IsConcreteType());
        }

        [Test]
        public void IntIsNotAConcreteType()
        {
            Assert.IsFalse(typeof(int).IsConcreteType());
        }

        [Test]
        public void NullableIntIsNotAConcreteType()
        {
            Assert.IsFalse(typeof(int?).IsConcreteType());
        }
    }
}