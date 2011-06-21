namespace LightCore.Performance.Domain
{
    public interface IDatabase
    {
        ILogger Logger { get; set; }
        IErrorHandler ErrorHandler { get; set; }
    }
}