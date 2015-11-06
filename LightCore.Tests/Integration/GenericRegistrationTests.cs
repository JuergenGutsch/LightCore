using FluentAssertions;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Integration
{
    
    public class GenericRegistrationTest
    {
        [Fact]
        public void Generic_registration_can_registered_with_closed_type()
        {
            var builder = new ContainerBuilder();
            builder.Register<IRepository<Foo>, FooRepository>();

            var container = builder.Build();

            container.Resolve<IRepository<Foo>>().Should().NotBeNull();
        }

        [Fact]
        public void Generic_registration_can_registered_with_open_type()
        {
            var builder = new ContainerBuilder();

            builder.Register(typeof(IRepository<>), typeof(Repository<>));

            var container = builder.Build();

            for (var i = 0; i < 10; i++)
            {
                var fooRepository = container.Resolve<IRepository<Foo>>();
                var barRepository = container.Resolve<IRepository<Bar>>();

                fooRepository.Should().NotBeNull();
                barRepository.Should().NotBeNull();

                fooRepository.Should().BeOfType<Repository<Foo>>();
                barRepository.Should().BeOfType<Repository<Bar>>();
            }
        }
    }
}