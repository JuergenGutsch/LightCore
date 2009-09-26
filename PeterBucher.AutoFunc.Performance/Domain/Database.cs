namespace PeterBucher.AutoFunc.Performance.Domain
{
    public class Database : IDatabase
    {
        public Database(ILogger logger, IErrorHandler errorHandler)
        {
            this.Logger = logger;
            this.ErrorHandler = errorHandler;
        }

        public ILogger Logger
        {
            get;
            set;
        }

        public IErrorHandler ErrorHandler
        {
            get;
            set;
        }
    }
}