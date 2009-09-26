namespace PeterBucher.AutoFunc.Performance.Domain
{
    public interface IStockQuote
    {
        ILogger Logger { get; set; }
        IErrorHandler ErrorHandler { get; set; }
        IDatabase Database { get; set; }
    }
}