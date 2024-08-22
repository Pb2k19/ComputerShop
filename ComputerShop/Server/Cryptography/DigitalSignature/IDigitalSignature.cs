using Microsoft.IdentityModel.Tokens;

namespace ComputerShop.Server.Cryptography.DigitalSignature
{
    public interface IDigitalSignature
    {
        SecurityKey GetSecurityKey(IConfiguration configuration);
        SigningCredentials GetSigningCredentials(IConfiguration configuration);
    }
}