namespace LightCore.Performance.Domain
{
    public class WebApp : IWebApp
    {
        public WebApp(IAuthenticator authenticator, IStockQuote stockQuote)
        {
            this.Authenticator = authenticator;
            this.StockQuote = stockQuote;
        }

        public IAuthenticator Authenticator
        { 
            get;
            set;
        }

        public IStockQuote StockQuote
        {
            get;
            set;
        }

        public void Run()
        {
            
        }
    }
}