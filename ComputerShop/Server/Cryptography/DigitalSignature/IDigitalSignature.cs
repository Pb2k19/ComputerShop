using Microsoft.IdentityModel.Tokens;

namespace ComputerShop.Server.Cryptography.DigitalSignature
{
    public interface IDigitalSignature
    {
        SigningCredentials GetSigningCredentials(IConfiguration configuration);
    }
}