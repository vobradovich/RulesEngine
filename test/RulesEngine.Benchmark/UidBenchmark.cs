using System;
using BenchmarkDotNet.Attributes;

namespace RulesEngine.Benchmark
{
    public class UidBenchmark
    {
        private byte[] data;

        [Params(100)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
        }

        [Benchmark]
        public Guid SystemGuid() => Guid.NewGuid();

        [Benchmark]
        public byte[] SystemGuidToByteArray() => Guid.NewGuid().ToByteArray();

        [Benchmark]
        public string SystemGuidToString() => Guid.NewGuid().ToString();

        [Benchmark]
        public string SystemGuidToBase64() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        //[Benchmark]
        //public string NanoidString() => Nanoid.Nanoid.Generate();

        [Benchmark]
        public byte[] Nanoid2GetBytes() => Nanoid2.GetBytes();

        [Benchmark]
        public string Nanoid2ToBase64() => Convert.ToBase64String(Nanoid2.GetBytes(24));

        [Benchmark]
        public string Nanoid2Get() => Nanoid2.Get(24);

        [Benchmark]
        public string Nanoid2GetGuid() => Nanoid.New(Guid.NewGuid().ToByteArray());

        [Benchmark]
        public string NanoidGet() => Nanoid.New(24);
    }
}
