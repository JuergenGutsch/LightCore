namespace PeterBucher.AutoFunc.Performance.Domain
{
    public interface IWebApp
    {
        IAuthenticator Authenticator { get; set; }
        IStockQuote StockQuote { get; }
        void Run();
    }
}