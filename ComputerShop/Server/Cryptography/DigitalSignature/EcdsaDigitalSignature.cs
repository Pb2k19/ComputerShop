using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.DigitalSignature;

public class EcdsaDigitalSignature : IDigitalSignature
{
    public SigningCredentials GetSigningCredentials(IConfiguration configuration)
    {
        return new(GetSecurityKey(configuration), SecurityAlgorithms.EcdsaSha256Signature);
    }

    public SecurityKey GetSecurityKey(IConfiguration configuration)
    {
        string eccPem = configuration["Settings:TokenPrivateEC"];

        ECDsa key = ECDsa.Create();
        key.ImportFromPem(eccPem);

        return new ECDsaSecurityKey(key);
    }
}
