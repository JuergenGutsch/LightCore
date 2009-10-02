using PeterBucher.AutoFunc.Builder;
using PeterBucher.AutoFunc.Performance.Domain;

namespace PeterBucher.AutoFunc.Performance.UseCases
{
    public class AutoFuncUseCase : UseCase
    {
        private readonly IContainer _container;

        public AutoFuncUseCase()
        {
            var builder = new ContainerBuilder();
            builder.Register<IWebApp, WebApp>();
            builder.Register<IAuthenticator, Authenticator>();
            builder.Register<IStockQuote, StockQuote>();
            builder.Register<ILogger, Logger>();
            builder.Register<IErrorHandler, ErrorHandler>();
            builder.Register<IDatabase, Database>();

            this._container = builder.Build();
        }

        public override void Run()
        {
            var webApp = this._container.Resolve<IWebApp>();
            webApp.Run();
        }
    }
}