namespace LightCore.TestTypes
{
    public class Repository<T> : IRepository<T>
    {
        public Repository()
        {

        }

        public T GetData()
        {
            return default(T);
        }
    }

    public class Repository<T, TKey> : IRepository<T, TKey>
    {
        public Repository()
        {

        }

        public T GetData()
        {
            return default(T);
        }
    }
}