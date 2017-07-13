using System;
using System.Collections.Generic;
using System.Diagnostics;
using LightCore.Performance.UseCases;

namespace LightCore.Performance
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var iterations = 100000;

            Console.WriteLine("Running {0} iterations for each use case.", iterations);

            var padding = 50;

            var useCases = new List<UseCase>
            {
                new PlainUseCase(),
                new FunqUseCase(),
                new LightCoreUseCase(),
                new UnityUseCase(),
                new LightCoreDelegateUseCase(),
                new NinjectUseCase(),
                new AutofacUseCase()
            };

            useCases.ForEach(u => PerformUseCase(u, padding, iterations));

            Console.Read();
        }

        private static void PerformUseCase(UseCase useCase, int padding, int iterations)
        {
            Console.WriteLine(Pad(padding, useCase.GetType().Name + ": {0}ms"), Measure(useCase.Run, iterations));
        }

        private static string Pad(int count, string value)
        {
            return value + new string(' ', count - value.Length);
        }

        private static long Measure(Action action, int iterations)
        {
            GC.Collect();

            var watch = Stopwatch.StartNew();

            for (var i = 0; i < iterations; i++)
            {
                action();
            }

            return watch.ElapsedMilliseconds;
        }
    }
}