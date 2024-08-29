using BenchmarkDotNet.Attributes;

namespace ComputerShop.Benchmarks;

[MemoryDiagnoser]
public class DecryptionBenchmark
{
    [Benchmark]
    public void AesDecryption_Benchmark()
    {

    }

    [Benchmark]
    public void DesDecryption_Benchmark()
    {

    }

    [Benchmark]
    public void RsaDecryption_Benchmark()
    {

    }
}
