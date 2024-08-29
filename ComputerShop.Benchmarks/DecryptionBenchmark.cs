using BenchmarkDotNet.Attributes;
using ComputerShop.Server.Cryptography.Encryption;
using System.Text;

namespace ComputerShop.Benchmarks;

[MemoryDiagnoser]
public class DecryptionBenchmark
{
    private const string
    ExpectedDecryptedText = "So close, no matter how far",
    AesEncrypted = "AesGcm$C1GENRguIHL_qBa3$smAOD0abdl1phFcB4lW7FA$4vdqH1fWwkryLc-yFi5DTiqCB-5kGTWfMQ0M",
    DesEncrypted = "DesCbc$BQ8vqqmClO8$zYCRqA1m1Hpfq-kaj7anKdk_CLlzCKHzIxY3c7Dr0Ro",
    RsaEncrypted = "FXCrTlNPkg5M2wvyjueHbN1O9g8JjNf7VEz1zf2WhBM7haYRKHp8C-Sc3_cqNKDPlXnKnmx6y1oO9bbJDDyiXmV2R0bC5b9G5oU0VqvEfO3k0ljRYWZR72LKEkDnGzTsyyDx7OhJy89ZgIciQKUEn1BUoGsLeeb7HaJXnGLp1uE-0CXp8fkLSvyaQDJiQYl0tKoDqxejVr02cfH39oPm0j1FjY1nnxjQRze3XhBejyhK5eFtAzidgxgyF9sTFcjjjYL_SBVsAm1VOrxQoQcZyU15jMm3n0mhIwiFgXeq6GEjogIQ4tuEibd9j8OEK4TIYJM0rYG4yzHo1D9F3BUfCg",
    SymetricKey = "N6V3G0KGZp9ntOp+dlwD6e+Eo+HCkKO3Nl4zN3+cs4W2tZwCZ97MVjF16gldzsy",
    RSABenchPrivateKey =
    """
        -----BEGIN PRIVATE KEY-----
        MIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCc++GqN5LP4icB
        yIHJHaC0EJcpKPec+ygq04/b31UNJZMK2GVAHVkLkhGs/tMuhchJdLit/ah+Hqza
        IQ99Rntc3pXcbQoZmn2e06n52XAPp74Sj4cKQo7c2XjM3f5yzhba1nAJn3sxWMXX
        qCV3OzI1/HqzTD9JTxrIBu6QeBZO5sWKBbmw6xo8Jm7YjQyg9ukArQfu2kxIS0bh
        PdxqvhBNhDyma+NO8k9XW+Mby616QZFf2IrIFVdXi7hGe4FhuDdYSPfOv/jKM+au
        90Eqft+wHpF3hwIncOqi2JgjDMN/PYrOsKEFFGlGMzyt7q8zidHEphYN92uAfOON
        uX3Q5/qhAgMBAAECggEAR1M4Il8SoMo3f9tqgIaqG/YHwCdBbb09cEvkanlrysfe
        KcjNJBbRfOzWhBvZtqMJL7rNKBx+gCMO1SK9R2WNKlJlk5ulQaHMXqv76C2veSV1
        OjilGffIsjZK6QYaFG5nuvFAQVcJIZAcf9IBh81JEHdRBoLnpDbBS82+ebxeImWI
        Q4v9IpS1F23RDy4iPEBOzo9+irA48PFyPz4qa7Lif+Yt6SB1OZYqadZ4mRbvNylh
        O+v5l0E130qmT6E6Rt1fDEgM9zZ2lj+ucyc4teoShH818WuTwrQLY8yFbeg+wGo2
        ql9gRWTVDYzoQIr4FOSovRNxiaHY7QxtZ8lH7Npf4wKBgQDTOJH2We2VbaMlCoLi
        m6SPEBJf0uuTqqZsyxRwETxgyQ7FV8qQRBBEosfua0YhsXBWXABZajOSGTr5K+OC
        GpHKfAnD3dzVFBbEqngKKX0fz8k3xoJC+qs68KwpEATbLns1qPu8VoEMRldnCF9j
        Kg6suqTJtOvdPga4iDfXmHXa/wKBgQC+Q77skn+as/trbeKJBMD+rx6bwv6d2cib
        VkGM5obfV5vrYuq1aLqJbkhNvrlpU0cAsBHeR5YYYDTD802pI9OTybkU4owk8GFY
        3JhSBSGSxQAXMrxWitVklzECkT9hd+qL0lVH0gJFHe7Guw48f5pL0eJTPyABC0+F
        8Plq6iJKXwKBgF12L6gJXMZ9D+6I+ydYZXVkUC0UgGcD4MZNCgsYpVXSQXXzBc/W
        PjiQqtUFpK+t+x5LcWAfTRh9j4nH8NCV/yLQxeVkW53xWK3HHHyqpRIZeFj0vpjy
        oCfhbNxymSN/KsewsUtCH7IVwgD2RHb1mi62G8qhAqkQFBs9MzBz5tpzAoGAH6ut
        MznrjbfZ6PcAl2g4K+zAfpMFyQbAcsL35FlXKAQwSw1LHmlRa6D03iQaPuMC1aV5
        Q/PMk0AoaFmumrIA/P++FDDlvRxaR+1oWXbLMOAj9LiYSxuPC9By8wd0cmgAncek
        NHFLuW+TGHd4li9zYp2MO+ktDZ9xXE1RZy+UB6cCgYArnzlv3u70Udq7cJvEv7Ps
        KUjazX7Jx9RElS7H6/O0cPNX2BR0SgetsVFjJlgCJRxyzesLTFOd+cLTLfjM8X20
        8QUli+aDgirILuDrJ+dFJ5ZoRrHcJBTL8vHQCBJYgjCa/QnXZOO78RXf5PHyP99p
        qcypLdTzHAfX2DR1LZlomA==
        -----END PRIVATE KEY-----
        """;

    private readonly Random random = new(51);

    private readonly IEncryption
        aesEncryption = new AesGcmEncryption(),
        desEncryption = new DesCbcEncryption(),
        rsaEncryption = new RsaAlgorithm();

    [Benchmark(Baseline = true)]
    public byte[] AesDecryption_Benchmark()
    {
        byte[] key = Encoding.UTF8.GetBytes(SymetricKey)[..aesEncryption.KeyLengthBytes];

        byte[] decrypted = aesEncryption.Decrypt(AesEncrypted, key);
#if DEBUG
        if (!ExpectedDecryptedText.Equals(Encoding.UTF8.GetString(decrypted)))
            throw new Exception("Incorrect result");
#endif

        return decrypted;
    }

    [Benchmark]
    public byte[] DesDecryption_Benchmark()
    {
        byte[] key = Encoding.UTF8.GetBytes(SymetricKey)[..desEncryption.KeyLengthBytes];

        byte[] decrypted = desEncryption.Decrypt(DesEncrypted, key);
#if DEBUG
        if (!ExpectedDecryptedText.Equals(Encoding.UTF8.GetString(decrypted)))
            throw new Exception("Incorrect result");
#endif

        return decrypted;
    }

    [Benchmark]
    public byte[] RsaDecryption_Benchmark()
    {
        byte[] key = Encoding.UTF8.GetBytes(RSABenchPrivateKey);

        byte[] decrypted = rsaEncryption.Decrypt(RsaEncrypted, key);
#if DEBUG
        if (!ExpectedDecryptedText.Equals(Encoding.UTF8.GetString(decrypted)))
            throw new Exception("Incorrect result");
#endif

        return decrypted;
    }
}
