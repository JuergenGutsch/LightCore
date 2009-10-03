namespace LightCore.Performance.Domain
{
    public class StockQuote : IStockQuote
    {
        public StockQuote(ILogger logger, IErrorHandler errorHandler, IDatabase database)
        {
            this.Logger = logger;
            this.ErrorHandler = errorHandler;
            this.Database = database;
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

        public IDatabase Database
        {
            get;
            set;
        }
    }
}