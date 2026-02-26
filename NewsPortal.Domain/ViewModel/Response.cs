public class Response<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public List<string> Errors { get; set; }

    public Response(int statusCode, string message, T data = default, List<string> errors = null, bool isSuccess = true)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
        Errors = errors;
        IsSuccess = isSuccess;
    }

    public static Response<T> Success(T data, string message = "Success", bool isSuccess = true)
    {
        return new Response<T>(200, message, data, null, isSuccess);
    }

    public static Response<T> Failure(List<string> errors, string message = "Failure")
    {
        return new Response<T>(400, message, default, errors);
    }
}
