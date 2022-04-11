namespace ComputerShop.Server.Models
{
    public class Token
    {
        public string TokenValue { get; set; } = string.Empty;
        public string SecureFgpBase64 { get; set; } = string.Empty;
        public bool QuickTokenCheck()
        {
            if (string.IsNullOrWhiteSpace(TokenValue) ||
                string.IsNullOrWhiteSpace(SecureFgpBase64))
            {
                return false;
            }
            return true;
        }
    }
}
