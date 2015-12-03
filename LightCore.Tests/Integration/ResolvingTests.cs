using System;
using System.Threading.Tasks;
using FluentAssertions;
using LightCore.TestTypes;
using Xunit;


namespace LightCore.Tests.Integration
{
    public class Test
    {
        public Test(IContainer container, TestBar testBar)
        {
            this.Property = container;
            this.PropertyTwo = testBar;
        }

        public IContainer Property { get; set; }
        public TestBar PropertyTwo { get; set; }
    }

    public class TestBar
    {

    }


    /// <summary>
    /// Summary description for ResolvingTests
    /// </summary>

    public class ResolvingTests
    {
        [Fact]
        public void Lazy_RegistrationSource_Supports_ConcreteTypes_Without_PreInitialization_And_Depdendencies()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();
            var lazyFoo = container.Resolve<Lazy<Test>>().Value;

            lazyFoo.Should().NotBeNull();
        }

        [Fact]
        public void Factory_RegistrationSource_Supports_ConcreteTypes_Without_PreInitialization_And_Depdendencies()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();
            var fooFunc = container.Resolve<Func<Test>>();

            fooFunc.Should().NotBeNull();
            fooFunc.Should().BeOfType<Test>();
        }

        [Fact]
        public void RegistrationContainer_Is_ThreadSafe()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Register<IBar>(c => new Bar());

            containerBuilder.Register<Foo, Foo>();
            containerBuilder.Register<FooTestTwo, FooTestTwo>();

            var container = containerBuilder.Build();

            for (var i = 0; i < 10; i++)
            {
                var task1 = Task.Factory.StartNew(() => Assert.True(null != container.Resolve<Foo>()));
                var task2 = Task.Factory.StartNew(() => Assert.True(null != container.Resolve<FooTestTwo>()));

                Task.WaitAll(task1, task2);
            }
        }

        public class FooTestTwo
        {
            public FooTestTwo(IBar bar)
            {

            }
        }

        [Fact]
        public void Bar_as_argument_will_be_reference_equal_to_the_resolved_bar()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var bar = container.Resolve<IBar>();

            var foo = container.Resolve<IFoo>(bar);

            bar.Should().BeSameAs(foo.Bar);
        }

        [Fact]
        public void Bar_as_reference_argument_Should_Not_ReUse_Previous_Cached_Arguments()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var bar = container.Resolve<IBar>();

            var foo = container.Resolve<IFoo>(bar);
            var fooWithOtherBarInstance = container.Resolve<IFoo>(new Bar());

            bar.Should().NotBeSameAs(fooWithOtherBarInstance.Bar);
        }

        [Fact]
        public void A_Second_Resolve_Invocation_With_No_Arguments_Should_Not_ReUse_Previous_Cached_Reference_Arguments()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var foo = container.Resolve<IFoo>(new Bar());
            var fooTwo = container.Resolve<IFoo>();

            fooTwo.Bar.Should().NotBeNull();
        }

        [Fact]
        public void A_Second_Resolve_Invocation_With_No_Arguments_Should_Not_ReUse_Previous_Cached_Arguments()
        {
            var builder = new ContainerBuilder();
            builder.Register<TextHolder, TextHolder>();

            var container = builder.Build();

            var firstInstance = container.Resolve<TextHolder>("test");
            var secondInstance = container.Resolve<TextHolder>();

            secondInstance.Text.Should().BeNull();
        }

        public class TextHolder
        {
            public string Text
            {
                get;
                set;
            }

            public TextHolder()
            {

            }

            public TextHolder(string text)
            {
                Text = text;
            }
        }

        [Fact]
        public void Foo_is_resolved_with_default_constructor_if_no_bar_is_registered()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var instance = container.Resolve<IFoo>();
        }

        [Fact]
        public void Foo_is_resolved_with_the_bar_only_constructor_if_bar_is_registered()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var actual = container.Resolve<IFoo>();

            actual.Should().NotBeNull();
            actual.Bar.Should().NotBeNull();
        }

        [Fact]
        public void Foo_is_resolved_with_the_bar_with_arguments_constructor_if_bar_registered_and_arguments_supported()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>().WithArguments("Test", true);
            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var instance = container.Resolve<IFoo>();

            //Assert.IsNotNull(instance);
            //Assert.IsNotNull(instance.Bar);
            //Assert.IsNotNull((instance as Foo).Arg1);
            //Assert.AreEqual(true, (instance as Foo).Arg2);
        }

        [Fact]
        public void Foo_is_resolved_with_bool_argument_constructor_if_only_that_argument_is_supported()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>().WithArguments(true);

            var container = builder.Build();

            var actual = container.Resolve<IFoo>() as Foo;

            actual.Should().NotBeNull();
            actual.Arg2.Should().BeTrue();
        }

        [Fact]
        public void Can_resolve_dependencies_with_injected_container()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar>(c => new Bar());

            var container = builder.Build();

            var resolvedContainer = container.Resolve<IContainer>();
            var actual = resolvedContainer.Resolve<IBar>();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<Bar>();

            resolvedContainer.Should().NotBeNull();
            resolvedContainer.Should().BeAssignableTo<IContainer>();
        }

        [Fact]
        public void Container_resolves_registered_interface_registration()
        {
            var builder = new ContainerBuilder();

            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();
            var actual = container.Resolve<IFoo>();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<Foo>();
        }

        [Fact]
        public void Container_resolves_registed_abstract_class()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<FooBase, Foo>();

            var container = builder.Build();
            var actual = container.Resolve<FooBase>();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<Foo>();
        }

        [Fact]
        public void Container_resolves_registered_class_mapped_to_itself()
        {
            var builder = new ContainerBuilder();
            builder.Register<Bar>();

            var container = builder.Build();
            var actual = container.Resolve<Bar>();

            actual.Should().NotBeNull();
        }

        [Fact]
        public void Container_resolves_a_whole_object_tree()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();
            var actual = container.Resolve<IFoo>();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<Foo>();
            actual.Bar.Should().NotBeNull();
        }
    }
}