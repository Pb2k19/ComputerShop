using BenchmarkDotNet.Attributes;
using ComputerShop.Server.Cryptography.DigitalSignature;
using ComputerShop.Server.Helpers;
using ComputerShop.Server.Models;
using ComputerShop.Shared.Models.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ComputerShop.Benchmarks;

[MemoryDiagnoser]
public class TokenDigitalSignatureBenchmark
{
    private readonly IDigitalSignature
        rsa = new RsaSha256DigitalSignature(),
        ecdsa = new EcdsaDigitalSignature();

    string
        rsaPrivKey =
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
        ecdsaPrivKey =
        """
        -----BEGIN EC PRIVATE KEY-----
        MHcCAQEEIDZyNTeL4+Exr6wbsbOvx7THQlEgCPKrtPBB09qIjCDXoAoGCCqGSM49
        AwEHoUQDQgAE7UjzvYgDJ1OOE6FbPlKNJ5mb8pEFgJK9uGT46h1dkX+g1EYXn3GB
        Zs4/o93LwRmGGtb87uwnFC6LykD8VpdlHw==
        -----END EC PRIVATE KEY-----
        """;


    [Benchmark(Baseline = true)]
    public Token RSA_Benchmark()
    {
        return CreateToken(rsa, rsaPrivKey);
    }

    [Benchmark]
    public Token ECDSA_Benchmark()
    {
        return CreateToken(ecdsa, ecdsaPrivKey);
    }

    private Token CreateToken(IDigitalSignature digitalSignature, string pem)
    {
        Token token = new();
        RegisteredUser user = new()
        {
            CreationDate = new(2024, 05, 04, 11, 40, 0),
            Email = "test@email.com",
            Id = "123456789",
            Role = "user"
        };

        List<Claim> claims =
        [
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(CustomClaims.Fingerprint, "fingerprint.FingerprintHash"),
        ];

        SigningCredentials credentials = digitalSignature.GetSigningCredentials(pem);

        JwtSecurityToken jwtSecurityToken = new(
            expires: DateTime.UtcNow.AddHours(24),
            claims: claims,
            signingCredentials: credentials);

        token.TokenValue = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        token.SecureFgpBase64 = "ZmluZ2VycHJpbnQuRmluZ2VycHJpbnRIYXNo";

        return token;
    }
}
