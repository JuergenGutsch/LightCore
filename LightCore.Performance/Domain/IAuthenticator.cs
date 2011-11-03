namespace LightCore.Performance.Domain
{
    public interface IAuthenticator
    {
        ILogger Logger { get; set; }
        IErrorHandler ErrorHandler { get; set; }
        IDatabase Database { get; set; }
    }
}