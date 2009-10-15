namespace LightCore.TestTypes
{
    public class FooTwoRepository : IRepository<Foo>
    {
        public Foo LoadEntityRepresentation()
        {
            return new Foo();
        }
    }
}