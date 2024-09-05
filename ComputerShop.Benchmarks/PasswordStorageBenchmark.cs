using BenchmarkDotNet.Attributes;
using ComputerShop.Server.Cryptography.Hash;
using System.Text;

namespace ComputerShop.Benchmarks;

[MemoryDiagnoser]
public class PasswordStorageBenchmark
{
    private const string PasswordForBenchmark = "Pa$$w0rd@Bench0";
    private readonly byte[] PasswordForBenchmarkBytes = Encoding.UTF8.GetBytes(PasswordForBenchmark);

    private readonly IHashAlgorithm
        plainText = new PlainTextNoHash(),
        sha1 = new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA1Name),
        md5 = new SimpleHashAlgorithm(SimpleHashAlgorithm.MD5Name),
        sha256 = new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_256Name),
        sha384 = new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_384Name),
        sha512 = new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_512Name),
        pbkdf2 = new PBKDF2HashAlgorithm(),
        argon2id = new Argon2idHashAlgorithm();

    [Benchmark]
    public string PlainText_Benchmark()
    {
        return PasswordForBenchmark;
    }

    [Benchmark]
    public string SHA1_Benchmark()
    {
        return sha1.PasswordStorage(PasswordForBenchmarkBytes);
    }

    [Benchmark]
    public string MD5_Benchmark()
    {
        return md5.PasswordStorage(PasswordForBenchmarkBytes);
    }

    [Benchmark]
    public string SHA2_256_Benchmark()
    {
        return sha256.PasswordStorage(PasswordForBenchmarkBytes);
    }

    [Benchmark]
    public string SHA2_384_Benchmark()
    {
        return sha256.PasswordStorage(PasswordForBenchmarkBytes);
    }

    [Benchmark]
    public string SHA2_512_Benchmark()
    {
        return sha512.PasswordStorage(PasswordForBenchmarkBytes);
    }

    [Benchmark(Baseline = true)]
    public string PBKDF2_Benchmark()
    {
        return pbkdf2.PasswordStorage(PasswordForBenchmarkBytes);
    }

    [Benchmark]
    public string Argon2id_Benchmark()
    {
        return argon2id.PasswordStorage(PasswordForBenchmarkBytes);
    }
}
