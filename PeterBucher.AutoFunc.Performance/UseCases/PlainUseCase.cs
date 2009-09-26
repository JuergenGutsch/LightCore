using PeterBucher.AutoFunc.Performance.Domain;

namespace PeterBucher.AutoFunc.Performance.UseCases
{
    public class PlainUseCase : UseCase
    {
        public override void Run()
        {
            var app = new WebApp(
                new Authenticator(
                    new Logger(),
                    new ErrorHandler(
                        new Logger()
                        ),
                    new Database(
                        new Logger(),
                        new ErrorHandler(
                            new Logger()
                            )
                        )
                    ),
                new StockQuote(
                    new Logger(),
                    new ErrorHandler(
                        new Logger()),
                    new Database(
                        new Logger(),
                        new ErrorHandler(
                            new Logger()))));

            app.Run();
        }
    }
}