namespace CareFinder.Server.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = "success";
        public bool Success { get; set; } = true;

    }
}
