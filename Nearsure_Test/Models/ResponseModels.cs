namespace ConwayGame.Models
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
    public class ServiceResponse<T>
    {
        public bool Successful => string.IsNullOrEmpty(Error);
        public string Error { get; set; } = string.Empty;
        public T Data { get; set; }
    }
    public class ServiceResponse
    {
        public bool Successful => string.IsNullOrEmpty(Error);
        public string Error { get; set; } = string.Empty;
    }
}
