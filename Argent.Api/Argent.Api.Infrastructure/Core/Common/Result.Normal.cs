namespace Argent.Api.Infrastructure.Core.Common {
    /// <summary>
    /// Non-generic result for operations that return no data (e.g. delete, update)
    /// </summary>
    public partial class Result {
        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;
        public string? Error { get; private set; }
        public string? ErrorCode { get; private set; }

        private Result(bool isSuccess, string? error, string? errorCode)
        {
            IsSuccess = isSuccess;
            Error = error;
            ErrorCode = errorCode;
        }

        public static Result Success() => new(true, null, null);
        public static Result Failure(string error, string? errorCode = null) => new(false, error, errorCode);
    }
}
