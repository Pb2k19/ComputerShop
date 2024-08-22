using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.DigitalSignature;

public class RsaSha256DigitalSignature : IDigitalSignature
{
    public SigningCredentials GetSigningCredentials(IConfiguration configuration)
    {
        string rsaPem = configuration["Settings:TokenPrivateRSA"];

        using RSA rsa = RSA.Create();
        rsa.ImportFromPem(rsaPem);

        return new(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
    }
}