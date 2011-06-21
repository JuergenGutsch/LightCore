using LightCore.Lifecycle;
using LightCore.Performance.Domain;

namespace LightCore.Performance.UseCases
{
    public class LightCoreDelegateUseCase : UseCase
    {
        private readonly IContainer _container;

        public LightCoreDelegateUseCase()
        {
            var builder = new ContainerBuilder();

            builder.Register<IWebApp>(
                c => new WebApp(
                         c.Resolve<IAuthenticator>(),
                         c.Resolve<IStockQuote>()));

            builder.Register<IAuthenticator>(
                c => new Authenticator(
                         c.Resolve<ILogger>(),
                         c.Resolve<IErrorHandler>(),
                         c.Resolve<IDatabase>()));

            builder.Register<IStockQuote>(
                c => new StockQuote(
                         c.Resolve<ILogger>(),
                         c.Resolve<IErrorHandler>(),
                         c.Resolve<IDatabase>()));

            builder.Register<ILogger>(
                c => new Logger());

            builder.Register<IErrorHandler>(
                c => new ErrorHandler(
                         c.Resolve<ILogger>()));

            builder.Register<IDatabase>(
                c => new Database(
                         c.Resolve<ILogger>(),
                         c.Resolve<IErrorHandler>()));

            this._container = builder.Build();
        }

        public override void Run()
        {
            var webApp = this._container.Resolve<IWebApp>();
            webApp.Run();
        }
    }
}