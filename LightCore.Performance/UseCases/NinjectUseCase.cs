using LightCore.Performance.Domain;

using Ninject;

namespace LightCore.Performance.UseCases
{
    public class NinjectUseCase : UseCase
    {
        private readonly IKernel _kernel;

        public NinjectUseCase()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IWebApp>().To<WebApp>();
            kernel.Bind<IAuthenticator>().To<Authenticator>();
            kernel.Bind<IStockQuote>().To<StockQuote>();
            kernel.Bind<ILogger>().To<Logger>();
            kernel.Bind<IErrorHandler>().To<ErrorHandler>();
            kernel.Bind<IDatabase>().To<Database>();

            _kernel = kernel;
        }

        public override void Run()
        {
            var webApp = this._kernel.Get<IWebApp>();
            webApp.Run();
        }
    }
}