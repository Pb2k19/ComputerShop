namespace ComputerShop.Server.Models
{
    public class Fingerprint
    {
        public string FingerprintBase64 { get; set; } = string.Empty;
        public string FingerprintHash { get; set; } = string.Empty;
        public bool QuickFingerprintCheck()
        {
            if(string.IsNullOrWhiteSpace(FingerprintHash) || 
               string.IsNullOrWhiteSpace(FingerprintBase64) ||
               FingerprintHash.Length < 60)
            {
                return false;
            }
            return true;
        }
    }
}
