namespace Top5.Api.Helper
{
    public class ApiResponse<T>
    {
        public string message { get; set; }
        public T? date { get; set; }
        public ApiResponse(string message, T? data)
        {
            this.message = message;
            this.date = data;
        }
    }
}
