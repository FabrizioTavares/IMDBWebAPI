namespace Service.Utils.Response
{
    public class Result
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        
        public Result(bool success, int statusCode, string? message = default)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
        }

    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public Result(T? data, bool success, int statusCode, string? message = default) : base(success, statusCode, message)
        {
            Data = data;
        }

    }

}
