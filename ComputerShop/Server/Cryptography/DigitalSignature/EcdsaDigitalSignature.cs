using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerShop.Server.Cryptography.DigitalSignature;

public class EcdsaDigitalSignature
{
    public SigningCredentials GetSigningCredentials(IConfiguration configuration)
    {
        string eccPem = configuration["Settings:TokenPrivateEC"];

        ECDsa key = ECDsa.Create();
        key.ImportFromPem(eccPem);

        return new(new ECDsaSecurityKey(key), SecurityAlgorithms.EcdsaSha256Signature);
    }
}
