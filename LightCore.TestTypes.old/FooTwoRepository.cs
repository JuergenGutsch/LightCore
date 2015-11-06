namespace LightCore.TestTypes
{
    public class FooTwoRepository : IRepository<Foo>
    {
        public Foo GetData()
        {
            return new Foo();
        }
    }
}