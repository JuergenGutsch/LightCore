using Autofac;

using LightCore.Performance.Domain;

namespace LightCore.Performance.UseCases
{
    public class AutofacUseCase : UseCase
    {
        private readonly Autofac.IContainer _container;

        public AutofacUseCase()
        {
            var builder = new Autofac.ContainerBuilder();
            builder.RegisterType<WebApp>()
                .As<IWebApp>();

            builder.RegisterType<Authenticator>()
                .As<IAuthenticator>();

            builder.RegisterType<StockQuote>()
                .As<IStockQuote>();

            builder.RegisterType<Logger>()
                .As<ILogger>();

            builder.RegisterType<ErrorHandler>()
                .As<IErrorHandler>();

            builder.RegisterType<Database>()
                .As<IDatabase>();

            this._container = builder.Build();
        }

        public override void Run()
        {
            var webApp = this._container.Resolve<IWebApp>();
            webApp.Run();
        }
    }
}