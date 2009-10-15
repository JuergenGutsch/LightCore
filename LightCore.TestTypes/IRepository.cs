namespace LightCore.TestTypes
{
    public interface IRepository<TEntity>
    {
        TEntity GetData();
    }
}