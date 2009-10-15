namespace LightCore.TestTypes
{
    public class FooRepository : IRepository<Foo>
    {
        public Foo LoadEntityRepresentation()
        {
            return new Foo();
        }
    }
}