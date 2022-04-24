namespace ComputerShop.Shared.Models.User
{
    public class DeliveryDetails
    {
        public string Email { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }

        public bool QuickValidate()
        {
            if(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(City) ||
               string.IsNullOrWhiteSpace(HouseNumber) || string.IsNullOrWhiteSpace(PhoneNumber) ||
               string.IsNullOrWhiteSpace(PostCode) || string.IsNullOrWhiteSpace(Street))
                return false;
            return true;
        }
    }
}
