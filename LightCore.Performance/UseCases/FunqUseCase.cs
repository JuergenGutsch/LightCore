using LightCore.Performance.Domain;

namespace LightCore.Performance.UseCases
{
    public class FunqUseCase : UseCase
    {
        private readonly Funq.Container _container;

        public FunqUseCase()
        {
            this._container = new Funq.Container();

            this._container.DefaultReuse = Funq.ReuseScope.None;

            this._container.Register<IWebApp>(
                c => new WebApp(
                         c.Resolve<IAuthenticator>(),
                         c.Resolve<IStockQuote>()));

            this._container.Register<IAuthenticator>(
                c => new Authenticator(
                         c.Resolve<ILogger>(),
                         c.Resolve<IErrorHandler>(),
                         c.Resolve<IDatabase>()));

            this._container.Register<IStockQuote>(
                c => new StockQuote(
                         c.Resolve<ILogger>(),
                         c.Resolve<IErrorHandler>(),
                         c.Resolve<IDatabase>()));

            this._container.Register<ILogger>(
                c => new Logger());

            this._container.Register<IErrorHandler>(
                c => new ErrorHandler(
                         c.Resolve<ILogger>()));

            this._container.Register<IDatabase>(
                c => new Database(
                         c.Resolve<ILogger>(),
                         c.Resolve<IErrorHandler>()));
        }

        public override void Run()
        {
            var webApp = this._container.Resolve<IWebApp>();
            webApp.Run();
        }
    }
}