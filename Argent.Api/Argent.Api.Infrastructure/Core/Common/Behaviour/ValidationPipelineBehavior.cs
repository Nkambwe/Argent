using FluentValidation;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Common.Behaviour {
    /// <summary>
    /// Runs all registered FluentValidation validators for the request before
    /// the handler executes. Returns a failed Result<T> on validation errors
    /// instead of throwing exceptions, keeping the error path consistent.
    /// </summary>
    public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken ct) {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, ct)));

            var failures = results
                .SelectMany(r => r.Errors)
                .Where(f => f is not null)
                .ToList();

            if (failures.Count == 0)
                return await next();

            // Build a comma-separated error message
            var errorMessage = string.Join("; ", failures.Select(f => f.ErrorMessage));

            // Return as Result<T> failure if TResponse is Result<T> or Result
            var responseType = typeof(TResponse);

            if (responseType.IsGenericType &&
                responseType.GetGenericTypeDefinition() == typeof(Result<>)) {
                var innerType = responseType.GetGenericArguments()[0];
                var failureMethod = typeof(Result<>)
                    .MakeGenericType(innerType)
                    .GetMethod(nameof(Result<object>.Failure), [typeof(string), typeof(string)])!;

                return (TResponse)failureMethod.Invoke(null, [errorMessage, "VALIDATION_ERROR"])!;
            }

            if (responseType == typeof(Result))
                return (TResponse)(object)Result.Failure(errorMessage, "VALIDATION_ERROR");

            // For any other response type, throw so it surfaces via ExceptionMiddleware
            throw new ValidationException(failures);
        }
    }
}
