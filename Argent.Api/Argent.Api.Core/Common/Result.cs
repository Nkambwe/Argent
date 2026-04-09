
namespace Argent.Api.Core.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;
        public T? Data { get; private set; }
        public string? Error { get; private set; }
        public string? ErrorCode { get; private set; }

        private Result(bool isSuccess, T? data, string? error, string? errorCode)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
            ErrorCode = errorCode;
        }

        public static Result<T> Success(T data) =>
            new(true, data, null, null);

        public static Result<T> Failure(string error, string? errorCode = null) =>
            new(false, default, error, errorCode);

        public static Result<T> NotFound(string message = "Resource not found") =>
            Failure(message, "NOT_FOUND");

        public static Result<T> Unauthorized(string message = "Unauthorized") =>
            Failure(message, "UNAUTHORIZED");
    }
    
}
