namespace Service.Utils.Response
{
    public class Result
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }
        
        public Result(bool success, int statusCode, string? errorMessage = default)
        {
            Success = success;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public Result(T? data, bool success, int statusCode, string? errorMessage) : base(success, statusCode, errorMessage)
        {
            Data = data;
        }

    }

}
