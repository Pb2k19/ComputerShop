using System.Security.Claims;
using System.Security.Principal;

namespace ComputerShop.Server.Helpers
{
    public static class CustomClaims
    {
        public const string Fingerprint = "http://computershop.pl/claims/fingerprint";
    }
    public static class IdentityExtensions
    {
        public static string GetFingerprint(this IIdentity identity)
        {
            ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst(CustomClaims.Fingerprint);
            return claim?.Value ?? string.Empty;
        }
    }
}
