namespace Top5.Api.Helper
{
    public class ApiResponse<T>
    {
        public string message { get; set; }
        public T? data { get; set; }
        public ApiResponse(string message, T? data)
        {
            this.message = message;
            this.data = data;
        }
    }
}
