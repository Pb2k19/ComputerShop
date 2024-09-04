using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.DigitalSignature;

public class EcdsaDigitalSignature : IDigitalSignature
{
    public SigningCredentials GetSigningCredentials(IConfiguration configuration)
    {
        return new(GetSecurityKey(configuration), SecurityAlgorithms.EcdsaSha256Signature);
    }

    public SigningCredentials GetSigningCredentials(string pem)
    {
        return new(GetSecurityKey(pem), SecurityAlgorithms.EcdsaSha256Signature);
    }

    public SecurityKey GetSecurityKey(IConfiguration configuration)
    {
        string eccPem = configuration["Settings:TokenPrivateEC"];

        return GetSecurityKey(eccPem);
    }

    public SecurityKey GetSecurityKey(string pem)
    {
        ECDsa key = ECDsa.Create();
        key.ImportFromPem(pem);

        return new ECDsaSecurityKey(key);
    }
}
