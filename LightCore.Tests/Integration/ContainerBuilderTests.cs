﻿using LightCore.Lifecycle;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Integration
{
    [TestFixture]
    public class ContainerBuilderTests
    {
        [Test]
        public void ContainerBuilder_with_registered_groups_reconice_these()
        {
            var builder = new ContainerBuilder();

            builder.ActiveRegistrationGroups = "Test";

            builder.Register<IFoo, FooTwo>().WithGroup("Test");
            builder.Register<IFoo, Foo>().WithGroup("Test2");

            var container = builder.Build();

            Assert.That(container.Resolve<IFoo>(), Is.InstanceOf<FooTwo>());
        }

        [Test]
        public void ContainerBuilder_with_non_active_groups_throws_registrationNotFoundException()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFoo, FooTwo>().WithGroup( "Test" );
            builder.Register<IFoo, Foo>().WithGroup( "Test2" );

            var container = builder.Build();

            Assert.That(() => container.Resolve<IFoo>(), Throws.InstanceOf<RegistrationNotFoundException>());
        }

        [Test]
        public void ContainerBuilder_can_register_generic_type()
        {
            var builder = new ContainerBuilder();

            builder.Register(typeof (IRepository<>), typeof (Repository<>));
        }

        [Test]
        public void ContainerBuilder_throws_exception_on_interface_to_interface_registration()
        {
            var builder = new ContainerBuilder();

            Assert.Throws<InvalidRegistrationException>(() => builder.Register<IFoo>());
        }

        [Test]
        public void ContainerBuilder_can_register_types()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFoo, Foo>();
        }

        [Test]
        public void ContainerBuilder_throws_on_not_assignable_contract_to_implementation()
        {
            Assert.Throws<ContractNotImplementedByTypeException>(() =>
                                                                     {
                                                                         var builder = new ContainerBuilder();
                                                                         builder.Register(typeof (IFoo), typeof (Bar));

                                                                         var container = builder.Build();
                                                                     });
        }

        [Test]
        public void ContainerBuilder_can_register_activation_functions()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFoo>(c => new Foo());

            builder.Build();
        }

        [Test]
        public void ContainerBuilder_can_register_type_to_self()
        {
            var builder = new ContainerBuilder();

            builder.Register<Foo>();

            builder.Build();
        }
        
        [Test]
        public void ContainerBuilders_default_scope_is_transient()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var instanceOne = container.Resolve<IFoo>();
            var instanceTwo = container.Resolve<IFoo>();

            Assert.AreNotSame(instanceOne, instanceTwo);
        }

        [Test]
        public void CointainerBuilder_default_scope_can_be_altered_to_singleton()
        {
            var builder = new ContainerBuilder();

            builder.DefaultControlledBy<SingletonLifecycle>();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var instanceOne = container.Resolve<IFoo>();
            var instanceTwo = container.Resolve<IFoo>();

            Assert.AreSame(instanceOne, instanceTwo);
        }
    }
}