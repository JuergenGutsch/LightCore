namespace LightCore.TestTypes
{
    public class FooRepository : IRepository<Foo>
    {
        public Foo GetData()
        {
            return new Foo();
        }
    }
}