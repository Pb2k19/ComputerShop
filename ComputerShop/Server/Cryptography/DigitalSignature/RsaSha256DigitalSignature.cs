using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.DigitalSignature;

public class RsaSha256DigitalSignature : IDigitalSignature
{
    public SigningCredentials GetSigningCredentials(IConfiguration configuration)
    {
        return new(GetSecurityKey(configuration), SecurityAlgorithms.RsaSha256);
    }

    public SigningCredentials GetSigningCredentials(string pem)
    {
        return new(GetSecurityKey(pem), SecurityAlgorithms.RsaSha256);
    }

    public SecurityKey GetSecurityKey(IConfiguration configuration)
    {
        string rsaPem = configuration["Settings:TokenPrivateRSA"];

        return GetSecurityKey(rsaPem);
    }

    public SecurityKey GetSecurityKey(string pem)
    {
        using RSA rsa = RSA.Create();
        rsa.ImportFromPem(pem);

        return new RsaSecurityKey(rsa);
    }
}