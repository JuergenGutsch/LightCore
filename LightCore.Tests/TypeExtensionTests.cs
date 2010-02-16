using NUnit.Framework;

using LightCore.ExtensionMethods.System;

namespace LightCore.Tests
{
    [TestFixture]
    public class TypeExtensionTest
    {
        private abstract class FooFoo
        {

        }

        private class FooFooImpl : FooFoo
        {

        }

        private interface IFooBar
        {

        }

        [Test]
        public void FooFooImplIsAConcreteType()
        {
            Assert.IsTrue(typeof(FooFooImpl).IsConcreteType());
        }

        [Test]
        public void FooFooIsNotAConcreteType()
        {
            Assert.IsFalse(typeof(FooFoo).IsConcreteType());
        }

        [Test]
        public void IFooBarIsNotAConcreteType()
        {
            Assert.IsFalse(typeof(IFooBar).IsConcreteType());
        }
    }
}