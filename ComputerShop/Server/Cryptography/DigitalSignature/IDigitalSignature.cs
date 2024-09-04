using Microsoft.IdentityModel.Tokens;

namespace ComputerShop.Server.Cryptography.DigitalSignature
{
    public interface IDigitalSignature
    {
        SecurityKey GetSecurityKey(IConfiguration configuration);
        SecurityKey GetSecurityKey(string pem);
        SigningCredentials GetSigningCredentials(IConfiguration configuration);
        SigningCredentials GetSigningCredentials(string pem);
    }
}