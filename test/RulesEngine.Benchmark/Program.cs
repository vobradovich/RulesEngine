using System;
using BenchmarkDotNet.Running;

namespace RulesEngine.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 20; i++)
            {
                //    Console.WriteLine(Guid.NewGuid().ToString());
                //    Console.WriteLine(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
                Console.WriteLine(Nanoid.New(12).ToUpperInvariant());
            }
            var summary = BenchmarkRunner.Run<RulesEngineCoreBenchmark>();
            Console.ReadKey();
        }
    }
}
