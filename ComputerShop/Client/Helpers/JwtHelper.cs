using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;

namespace ComputerShop.Client.Helpers
{
    public class JwtHelper
    {
        public static byte[] GetBytesFromBase64WithoutPadding(string? input)
        {
            if(string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            input += (input.Length % 4) switch
            {
                2 => "==",
                3 => "=",
                _ => "",
            };
            return Base64UrlEncoder.DecodeBytes(input);
        }

        public static List<Claim> GetClaimsFromJwt(string jwt)
        {
            string payload = jwt.Split('.')[1];
            byte[] bytes = GetBytesFromBase64WithoutPadding(payload);
            Dictionary<string, object>? value = JsonSerializer.Deserialize<Dictionary<string, object>>(bytes);
            if (value == null)
                throw new Exception("Couldn't deserialize value");

            return value.Select(v => new Claim(v.Key, v.Value?.ToString() ?? string.Empty)).ToList();
        }
    }
}
