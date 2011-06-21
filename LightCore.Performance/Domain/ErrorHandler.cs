namespace LightCore.Performance.Domain
{
    public class ErrorHandler : IErrorHandler
    {
        public ErrorHandler(ILogger logger)
        {
            this.Logger = logger;
        }

        public ILogger Logger
        {
            get;
            set;
        }
    }
}