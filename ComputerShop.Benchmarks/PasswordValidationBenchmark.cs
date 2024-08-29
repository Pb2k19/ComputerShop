using BenchmarkDotNet.Attributes;
using ComputerShop.Server.Cryptography.Hash;
using System.Text;

namespace ComputerShop.Benchmarks;

[MemoryDiagnoser]
public class PasswordValidationBenchmark
{
    private const string
        PasswordForBenchmark = "Pa$$w0rd@Bench0",
        Sha1Hash = "wCNcW9e0xDKpyQvSzDhO9FFpzKQ",
        MD5Hash = "G-TuiyKN94gNq42vSo5YQQ",
        SHA256Hash = "Sz1vaf8myL6fjj3qvyQk2kTWMswTcMmorSaiVF7YN7U",
        SHA384Hash = "zSWegaTVY_BTR6cML-x08lgqrJPU-yqaK0_qlh4PNOhIlVulQKHK_VKthiOzaJRJ",
        SHA512Hash = "i2nEb199J6zhq1uWeeag0BePvY9h9Z6pGjWPAbg_ytohlzSC7CiI_TYKxwjRuWGv98EpKcj5N6-vtnOZtxhDTg",
        PBKDF2Hash = "PBKDF2$600000$iGBo0nZbtVMasDVYHzDZaC9EOk5UXgRq4MMSvLaoVvw$6_h4_GE83dqimePuSghloDLojxMgsHXrkVS0Y2Js5j8",
        Argon2idHash = "$argon2id$v=19$m=32768,t=4,p=1$VSrZ9sDGMLOKuYQY9ABzaw$Va/mTr4sWOSnnTDw35w7p2XQlnUFQd0IpYEWz+aDsR0";

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
    public bool PlainText_Benchmark()
    {
        return plainText.VerifyHash(PasswordForBenchmarkBytes, PasswordForBenchmark);
    }

    [Benchmark]
    public bool SHA1_Benchmark()
    {
        return sha1.VerifyHash(PasswordForBenchmarkBytes, Sha1Hash);
    }

    [Benchmark]
    public bool MD5_Benchmark()
    {
        return md5.VerifyHash(PasswordForBenchmarkBytes, MD5Hash);
    }

    [Benchmark]
    public bool SHA2_256_Benchmark()
    {
        return sha256.VerifyHash(PasswordForBenchmarkBytes, SHA256Hash);
    }

    [Benchmark]
    public bool SHA2_384_Benchmark()
    {
        return sha384.VerifyHash(PasswordForBenchmarkBytes, SHA384Hash);
    }

    [Benchmark]
    public bool SHA2_512_Benchmark()
    {
        return sha512.VerifyHash(PasswordForBenchmarkBytes, SHA512Hash);
    }

    [Benchmark(Baseline = true)]
    public bool PBKDF2_Benchmark()
    {
        return pbkdf2.VerifyHash(PasswordForBenchmarkBytes, PBKDF2Hash);
    }

    [Benchmark]
    public bool Argon2id_Benchmark()
    {
        return argon2id.VerifyHash(PasswordForBenchmarkBytes, Argon2idHash);
    }
}
