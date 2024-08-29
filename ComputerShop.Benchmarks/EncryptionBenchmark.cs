using BenchmarkDotNet.Attributes;

namespace ComputerShop.Benchmarks;

[MemoryDiagnoser]
public class EncryptionBenchmark
{
    [Benchmark]
    public void AesEncryption_Benchmark()
    {

    }

    [Benchmark]
    public void DesEncryption_Benchmark()
    {

    }

    [Benchmark]
    public void RsaEncryption_Benchmark()
    {

    }
}
