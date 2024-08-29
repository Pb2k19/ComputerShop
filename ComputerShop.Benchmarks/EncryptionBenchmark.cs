using BenchmarkDotNet.Attributes;
using ComputerShop.Server.Cryptography.Encryption;
using System.Text;

namespace ComputerShop.Benchmarks;

[MemoryDiagnoser]
public class EncryptionBenchmark
{
    private const string
        TextToEncrypt = "So close, no matter how far",
        SymetricKey = "N6V3G0KGZp9ntOp+dlwD6e+Eo+HCkKO3Nl4zN3+cs4W2tZwCZ97MVjF16gldzsy",
        RSABenchPublicKey =
        """
        -----BEGIN PUBLIC KEY-----
        MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAnPvhqjeSz+InAciByR2g
        tBCXKSj3nPsoKtOP299VDSWTCthlQB1ZC5IRrP7TLoXISXS4rf2ofh6s2iEPfUZ7
        XN6V3G0KGZp9ntOp+dlwD6e+Eo+HCkKO3Nl4zN3+cs4W2tZwCZ97MVjF16gldzsy
        Nfx6s0w/SU8ayAbukHgWTubFigW5sOsaPCZu2I0MoPbpAK0H7tpMSEtG4T3car4Q
        TYQ8pmvjTvJPV1vjG8utekGRX9iKyBVXV4u4RnuBYbg3WEj3zr/4yjPmrvdBKn7f
        sB6Rd4cCJ3DqotiYIwzDfz2KzrChBRRpRjM8re6vM4nRxKYWDfdrgHzjjbl90Of6
        oQIDAQAB
        -----END PUBLIC KEY-----
        """;

    private readonly IEncryption
        aesEncryption = new AesGcmEncryption(),
        desEncryption = new DesCbcEncryption(),
        rsaEncryption = new RsaAlgorithm();

    [Benchmark(Baseline = true)]
    public string AesEncryption_Benchmark()
    {
        byte[] key = Encoding.UTF8.GetBytes(SymetricKey)[..aesEncryption.KeyLengthBytes];

        return aesEncryption.Encrypt(Encoding.UTF8.GetBytes(TextToEncrypt), key);
    }

    [Benchmark]
    public string DesEncryption_Benchmark()
    {
        byte[] key = Encoding.UTF8.GetBytes(SymetricKey)[..desEncryption.KeyLengthBytes];

        return desEncryption.Encrypt(Encoding.UTF8.GetBytes(TextToEncrypt), key);
    }

    [Benchmark]
    public string RsaEncryption_Benchmark()
    {
        byte[] key = Encoding.UTF8.GetBytes(RSABenchPublicKey);

        return rsaEncryption.Encrypt(Encoding.UTF8.GetBytes(TextToEncrypt), key);
    }
}
