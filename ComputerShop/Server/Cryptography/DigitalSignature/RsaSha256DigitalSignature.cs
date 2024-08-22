using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.DigitalSignature;

public class RsaSha256DigitalSignature : IDigitalSignature
{
    public SigningCredentials GetSigningCredentials(IConfiguration configuration)
    {
        return new(GetSecurityKey(configuration), SecurityAlgorithms.RsaSha256);
    }

    public SecurityKey GetSecurityKey(IConfiguration configuration)
    {
        string rsaPem = configuration["Settings:TokenPrivateRSA"];

        using RSA rsa = RSA.Create();
        rsa.ImportFromPem(rsaPem);

        return new RsaSecurityKey(rsa);
    }
}