using BenchmarkDotNet.Attributes;
using ComputerShop.Server.Cryptography.DigitalSignature;
using ComputerShop.Server.Cryptography.Encryption;
using ComputerShop.Server.Cryptography.Hash;
using ComputerShop.Server.DataAccess;
using ComputerShop.Server.Services.KeyService;
using ComputerShop.Server.Services.User;
using ComputerShop.Shared.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System.Text;

namespace ComputerShop.Benchmarks;

[MemoryDiagnoser(false)]
public class RegisterUserBenchmark
{
    private const string
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
        """,
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

    private readonly Register user = new() { Email = "test@email.com", Password = "Pa$$w0rdForB@nchm8rk_2a", ConfPassword = "Pa$$w0rdForB@nchm8rk_2a" };

    private readonly IConfiguration configuration = Substitute.For<IConfiguration>();

    private readonly IHttpContextAccessor contextAccessor = Substitute.For<HttpContextAccessor>();


    [Benchmark(Baseline = true)]
    public async Task UserService_NoEncryption_PlainTextPassword()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        UserData userData_NoEncryption = new(dbConnection);
        UserService userService_NoEncryption_PlainTextPassword = new(configuration, contextAccessor, userData_NoEncryption, new PlainTextNoHash(), new EcdsaDigitalSignature());

        await userService_NoEncryption_PlainTextPassword.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_NoEncryption_Md5Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        UserData userData_NoEncryption = new(dbConnection);
        UserService userService_NoEncryption_Md5Password = new(configuration, contextAccessor, userData_NoEncryption, new SimpleHashAlgorithm(SimpleHashAlgorithm.MD5Name), new EcdsaDigitalSignature());

        await userService_NoEncryption_Md5Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_NoEncryption_Sha1Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        UserData userData_NoEncryption = new(dbConnection);
        UserService userService_NoEncryption_Sha1Password = new UserService(configuration, contextAccessor, userData_NoEncryption, new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA1Name), new EcdsaDigitalSignature());

        await userService_NoEncryption_Sha1Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_NoEncryption_Sha256Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        UserData userData_NoEncryption = new(dbConnection);
        UserService userService_NoEncryption_Sha256Password = new UserService(configuration, contextAccessor, userData_NoEncryption, new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_256Name), new EcdsaDigitalSignature());


        await userService_NoEncryption_Sha256Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_NoEncryption_Sha512Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        UserData userData_NoEncryption = new(dbConnection);
        UserService userService_NoEncryption_Sha512Password = new UserService(configuration, contextAccessor, userData_NoEncryption, new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_512Name), new EcdsaDigitalSignature());

        await userService_NoEncryption_Sha512Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_NoEncryption_PBKDF2Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        UserData userData_NoEncryption = new(dbConnection);
        UserService userService_NoEncryption_PBKDF2Password = new UserService(configuration, contextAccessor, userData_NoEncryption, new PBKDF2HashAlgorithm(), new EcdsaDigitalSignature());

        await userService_NoEncryption_PBKDF2Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_NoEncryption_Argon2idPassword()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        UserData userData_NoEncryption = new(dbConnection);
        UserService userService_NoEncryption_Argon2idPassword = new UserService(configuration, contextAccessor, userData_NoEncryption, new Argon2idHashAlgorithm(), new EcdsaDigitalSignature());

        await userService_NoEncryption_Argon2idPassword.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_AesEncryption_PlainTextPassword()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_AesEncryption_PlainText = new(dbConnection, new AesGcmEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new PlainTextNoHash()));
        UserService userService_AesEncryption_PlainTextPassword = new(configuration, contextAccessor, userData_AesEncryption_PlainText, new PlainTextNoHash(), new EcdsaDigitalSignature());

        await userService_AesEncryption_PlainTextPassword.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_AesEncryption_Md5Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_AesEncryption_Md5 = new EncryptedUserData(dbConnection, new AesGcmEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new SimpleHashAlgorithm(SimpleHashAlgorithm.MD5Name)));
        UserService userService_AesEncryption_Md5Password = new UserService(configuration, contextAccessor, userData_AesEncryption_Md5, new SimpleHashAlgorithm(SimpleHashAlgorithm.MD5Name), new EcdsaDigitalSignature());

        await userService_AesEncryption_Md5Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_AesEncryption_Sha1Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_AesEncryption_Sha1 = new EncryptedUserData(dbConnection, new AesGcmEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA1Name)));
        UserService userService_AesEncryption_Sha1Password = new UserService(configuration, contextAccessor, userData_AesEncryption_Sha1, new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA1Name), new EcdsaDigitalSignature());

        await userService_AesEncryption_Sha1Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_AesEncryption_Sha256Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_AesEncryption_Sha256 = new EncryptedUserData(dbConnection, new AesGcmEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_256Name)));
        UserService userService_AesEncryption_Sha256Password = new UserService(configuration, contextAccessor, userData_AesEncryption_Sha256, new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_256Name), new EcdsaDigitalSignature());

        await userService_AesEncryption_Sha256Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_AesEncryption_Sha512Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_AesEncryption_Sha512 = new EncryptedUserData(dbConnection, new AesGcmEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_512Name)));
        UserService userService_AesEncryption_Sha512Password = new UserService(configuration, contextAccessor, userData_AesEncryption_Sha512, new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_512Name), new EcdsaDigitalSignature());

        await userService_AesEncryption_Sha512Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_AesEncryption_PBKDF2Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_AesEncryption_PBKDF2 = new EncryptedUserData(dbConnection, new AesGcmEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new PBKDF2HashAlgorithm()));
        UserService userService_AesEncryption_PBKDF2Password = new UserService(configuration, contextAccessor, userData_AesEncryption_PBKDF2, new PBKDF2HashAlgorithm(), new EcdsaDigitalSignature());

        await userService_AesEncryption_PBKDF2Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_AesEncryption_Argon2idPassword()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_AesEncryption_Argon2id = new EncryptedUserData(dbConnection, new AesGcmEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new Argon2idHashAlgorithm()));
        UserService userService_AesEncryption_Argon2idPassword = new UserService(configuration, contextAccessor, userData_AesEncryption_Argon2id, new Argon2idHashAlgorithm(), new EcdsaDigitalSignature());

        await userService_AesEncryption_Argon2idPassword.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_DesEncryption_PlainTextPassword()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_DesEncryption_PlainText = new EncryptedUserData(dbConnection, new DesCbcEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new PlainTextNoHash()));
        UserService userService_DesEncryption_PlainTextPassword = new UserService(configuration, contextAccessor, userData_DesEncryption_PlainText, new PlainTextNoHash(), new EcdsaDigitalSignature());

        await userService_DesEncryption_PlainTextPassword.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_DesEncryption_Md5Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_DesEncryption_Md5 = new EncryptedUserData(dbConnection, new DesCbcEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new SimpleHashAlgorithm(SimpleHashAlgorithm.MD5Name)));
        UserService userService_DesEncryption_Md5Password = new UserService(configuration, contextAccessor, userData_DesEncryption_Md5, new SimpleHashAlgorithm(SimpleHashAlgorithm.MD5Name), new EcdsaDigitalSignature()); ;

        await userService_DesEncryption_Md5Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_DesEncryption_Sha1Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_DesEncryption_Sha1 = new EncryptedUserData(dbConnection, new DesCbcEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA1Name)));
        UserService userService_DesEncryption_Sha1Password = new UserService(configuration, contextAccessor, userData_DesEncryption_Sha1, new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA1Name), new EcdsaDigitalSignature());

        await userService_DesEncryption_Sha1Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_DesEncryption_Sha256Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_DesEncryption_Sha256 = new EncryptedUserData(dbConnection, new DesCbcEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_256Name)));
        UserService userService_DesEncryption_Sha256Password = new UserService(configuration, contextAccessor, userData_DesEncryption_Sha256, new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_256Name), new EcdsaDigitalSignature());

        await userService_DesEncryption_Sha256Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_DesEncryption_Sha512Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_DesEncryption_Sha512 = new EncryptedUserData(dbConnection, new DesCbcEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_512Name)));
        UserService userService_DesEncryption_Sha512Password = new UserService(configuration, contextAccessor, userData_DesEncryption_Sha512, new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_512Name), new EcdsaDigitalSignature());

        await userService_DesEncryption_Sha512Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_DesEncryption_PBKDF2Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_DesEncryption_PBKDF2 = new EncryptedUserData(dbConnection, new DesCbcEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new PBKDF2HashAlgorithm()));
        UserService userService_DesEncryption_PBKDF2Password = new UserService(configuration, contextAccessor, userData_DesEncryption_PBKDF2, new PBKDF2HashAlgorithm(), new EcdsaDigitalSignature());

        await userService_DesEncryption_PBKDF2Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_DesEncryption_Argon2idPassword()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_DesEncryption_Argon2id = new EncryptedUserData(dbConnection, new DesCbcEncryption(), new SymetricKeyService(Encoding.UTF8.GetBytes("BenchmarkKeyTest123456789@$%@@"), new Argon2idHashAlgorithm()));
        UserService userService_DesEncryption_Argon2idPassword = new UserService(configuration, contextAccessor, userData_DesEncryption_Argon2id, new Argon2idHashAlgorithm(), new EcdsaDigitalSignature());

        await userService_DesEncryption_Argon2idPassword.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_RsaEncryption_PlainTextPassword()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_RsaEncryption = new(dbConnection, new RsaAlgorithm(), new AsymmetricKeyService(Encoding.UTF8.GetBytes(RSABenchPublicKey), Encoding.UTF8.GetBytes(RSABenchPublicKey)));
        UserService userService_RsaEncryption_PlainTextPassword = new(configuration, contextAccessor, userData_RsaEncryption, new PlainTextNoHash(), new EcdsaDigitalSignature());

        await userService_RsaEncryption_PlainTextPassword.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_RsaEncryption_Md5Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_RsaEncryption = new(dbConnection, new RsaAlgorithm(), new AsymmetricKeyService(Encoding.UTF8.GetBytes(RSABenchPublicKey), Encoding.UTF8.GetBytes(RSABenchPublicKey)));
        UserService userService_RsaEncryption_Md5Password = new(configuration, contextAccessor, userData_RsaEncryption, new SimpleHashAlgorithm(SimpleHashAlgorithm.MD5Name), new EcdsaDigitalSignature());

        await userService_RsaEncryption_Md5Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_RsaEncryption_Sha1Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_RsaEncryption = new(dbConnection, new RsaAlgorithm(), new AsymmetricKeyService(Encoding.UTF8.GetBytes(RSABenchPublicKey), Encoding.UTF8.GetBytes(RSABenchPublicKey)));
        UserService userService_RsaEncryption_Sha1Password = new(configuration, contextAccessor, userData_RsaEncryption, new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA1Name), new EcdsaDigitalSignature());

        await userService_RsaEncryption_Sha1Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_RsaEncryption_Sha256Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_RsaEncryption = new(dbConnection, new RsaAlgorithm(), new AsymmetricKeyService(Encoding.UTF8.GetBytes(RSABenchPublicKey), Encoding.UTF8.GetBytes(RSABenchPublicKey)));
        UserService userService_RsaEncryption_Sha256Password = new(configuration, contextAccessor, userData_RsaEncryption, new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_256Name), new EcdsaDigitalSignature());

        await userService_RsaEncryption_Sha256Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_RsaEncryption_Sha512Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_RsaEncryption = new(dbConnection, new RsaAlgorithm(), new AsymmetricKeyService(Encoding.UTF8.GetBytes(RSABenchPublicKey), Encoding.UTF8.GetBytes(RSABenchPublicKey)));
        UserService userService_RsaEncryption_Sha512Password = new(configuration, contextAccessor, userData_RsaEncryption, new SimpleHashAlgorithm(SimpleHashAlgorithm.SHA2_512Name), new EcdsaDigitalSignature());

        await userService_RsaEncryption_Sha512Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_RsaEncryption_PBKDF2Password()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_RsaEncryption = new(dbConnection, new RsaAlgorithm(), new AsymmetricKeyService(Encoding.UTF8.GetBytes(RSABenchPublicKey), Encoding.UTF8.GetBytes(RSABenchPublicKey)));
        UserService userService_RsaEncryption_PBKDF2Password = new(configuration, contextAccessor, userData_RsaEncryption, new PBKDF2HashAlgorithm(), new EcdsaDigitalSignature());

        await userService_RsaEncryption_PBKDF2Password.RegisterAsync(user);
    }

    [Benchmark]
    public async Task UserService_RsaEncryption_Argon2idPassword()
    {
        IDbConnection dbConnection = Substitute.For<IDbConnection>();
        EncryptedUserData userData_RsaEncryption = new(dbConnection, new RsaAlgorithm(), new AsymmetricKeyService(Encoding.UTF8.GetBytes(RSABenchPublicKey), Encoding.UTF8.GetBytes(RSABenchPublicKey)));
        UserService userService_RsaEncryption_Argon2idPassword = new(configuration, contextAccessor, userData_RsaEncryption, new Argon2idHashAlgorithm(), new EcdsaDigitalSignature());

        await userService_RsaEncryption_Argon2idPassword.RegisterAsync(user);
    }
}
