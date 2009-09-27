using System;
using System.Collections.Generic;
using System.Diagnostics;

using PeterBucher.AutoFunc.Performance.UseCases;

namespace PeterBucher.AutoFunc.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            int iterations = 1000;

            Console.WriteLine("Running {0} iterations for each use case.", iterations);

            int padding = 50;

            var useCases = new List<UseCase>
                               {
                                   new PlainUseCase(),
                                   new FunqUseCase(),
                                   new AutoFuncUseCase(),
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

            for (int i = 0; i < iterations; i++)
            {
                action();
            }

            return watch.ElapsedMilliseconds;
        }
    }
}