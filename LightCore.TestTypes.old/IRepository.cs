namespace LightCore.TestTypes
{
    public interface IRepository<TEntity> : IRepository<TEntity, int>
    {

    }

    public interface IRepository<TEntity, TKey>
    {
        TEntity GetData();
    }
}