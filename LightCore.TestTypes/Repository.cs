namespace LightCore.TestTypes
{
    public class Repository<T> : IRepository<T> where T : new()
    {
        public Repository()
        {

        }

        public T GetData()
        {
            return new T();
        }
    }

    public class Repository<T, TKey> : IRepository<T, TKey> where T : new()
    {
        public Repository()
        {

        }

        public T GetData()
        {
            return new T();
        }
    }
}