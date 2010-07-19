using LightCore.Performance.Domain;

using Microsoft.Practices.Unity;

namespace LightCore.Performance.UseCases
{
    public class UnityUseCase : UseCase
    {
        private readonly UnityContainer _container;

        public UnityUseCase()
        {
            this._container = new UnityContainer();

            this._container.RegisterType<IWebApp, WebApp>();
            this._container.RegisterType<IAuthenticator, Authenticator>();
            this._container.RegisterType<IStockQuote, StockQuote>();
            this._container.RegisterType<ILogger, Logger>();
            this._container.RegisterType<IErrorHandler, ErrorHandler>();
            this._container.RegisterType<IDatabase, Database>();
        }

        public override void Run()
        {
            var webApp = this._container.Resolve<IWebApp>();
            webApp.Run();
        }
    }
}