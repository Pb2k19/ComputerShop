namespace ComputerShop.Shared.Models.User
{
    public class PaymentMethod_WorkInProgress
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsCashOnDelivery { get; set; }
        public float Cost { get; set; }
    }
}
