using BenchmarkDotNet.Attributes;

namespace ComputerShop.Benchmarks;

[MemoryDiagnoser]
public class DigitalSignatureBenchmark
{
    [Benchmark]
    public void RSA_Benchmark()
    {

    }

    [Benchmark]
    public void ECDSA_Benchmark()
    {

    }
}
