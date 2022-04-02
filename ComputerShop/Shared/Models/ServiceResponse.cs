namespace ComputerShop.Shared.Models
{
    public class SimpleServiceResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = true;
    }
    public class ServiceResponse<T> : SimpleServiceResponse
    {
        public T Data { get; set; }

        public SimpleServiceResponse GetSimpleServiceResponse()
        {
            return new SimpleServiceResponse { Message = Message, Success = Success };
        }
    }
}
