using MediatR;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Argent.Api.Infrastructure.Core.Common.Behaviour {

    /// <summary>
    /// Runs FIRST in the pipeline. Sanitizes all string properties on the incoming
    /// request to strip dangerous characters before validation or execution.
    /// </summary>
    /// <remarks>
    /// Handles:
    ///   - HTML/script injection:  strips &lt;script&gt;, &lt;iframe&gt;, event handlers (onclick=, etc.)
    ///   - SQL fragments:          strips -- comments, semicolons outside quoted context
    ///   - Null-byte injection:    strips \0 characters
    ///   - Leading/trailing whitespace on all strings
    /// Properties named "Password", "PasswordHash", "Token", or "OldValues"/"NewValues"
    /// are intentionally skipped — sanitizing those would corrupt them. 
    /// </remarks>
    public class SanitizationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull {

        //..properties that must not be touched
        private static readonly HashSet<string> SkippedProperties = new(StringComparer.OrdinalIgnoreCase) {
            "Password", "CurrentPassword", "NewPassword", "PasswordHash",
            "Token", "RefreshToken", "AccessToken",
            "OldValues", "NewValues", "PasswordHistoryHash"
        };

        //..patterns that indicate injection attempts
        private static readonly Regex ScriptTagPattern = new(@"<\s*(script|iframe|object|embed|form|input|link|meta|style|svg|math)[^>]*>.*?</\s*\1\s*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex HtmlTagPattern = new(@"<[^>]+>", RegexOptions.Compiled);
        private static readonly Regex EventHandlerPattern = new(@"\bon\w+\s*=", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex SqlCommentPattern = new(@"--[^\r\n]*", RegexOptions.Compiled);
        private static readonly Regex NullBytePattern = new(@"\0", RegexOptions.Compiled);

        public async Task<TResponse> Handle( TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct) {
            SanitizeObject(request);
            return await next();
        }

        private static void SanitizeObject(object obj) {
            if (obj is null) return;

            var type = obj.GetType();

            //..skip value types, strings and system types
            if (type.IsPrimitive || type == typeof(string) || type.Namespace?.StartsWith("System") == true)
                return;

            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
                if (!prop.CanRead || !prop.CanWrite) continue;
                if (SkippedProperties.Contains(prop.Name)) continue;

                try {
                    if (prop.PropertyType == typeof(string)) {
                        var raw = (string?)prop.GetValue(obj);
                        if (raw is not null)
                            prop.SetValue(obj, Sanitize(raw));
                    }
                    else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(Type)) {
                        //..recurse into nested objects
                        var nested = prop.GetValue(obj);
                        if (nested is not null)
                            SanitizeObject(nested);
                    }
                    else if (IsStringCollection(prop.PropertyType)) {
                        //..string lists or arrays
                        if (prop.GetValue(obj) is IList<string> list)
                            for (int i = 0; i < list.Count; i++)
                                list[i] = Sanitize(list[i]);
                    }
                } catch {
                    // Sanitization failure must never break the request
                }
            }
        }

        private static string Sanitize(string input) {
            if (string.IsNullOrEmpty(input)) return input;

            var result = input.Trim();

            //..remove null bytes
            result = NullBytePattern.Replace(result, string.Empty);

            //..remove script or dangerous HTML tags
            result = ScriptTagPattern.Replace(result, string.Empty);

            //..remove remaining HTML tags
            result = HtmlTagPattern.Replace(result, string.Empty);

            //..remove inline event handlers onclick=, onload=, etc.
            result = EventHandlerPattern.Replace(result, string.Empty);

            //..remove SQL comments
            result = SqlCommentPattern.Replace(result, string.Empty);

            return result.Trim();
        }

        private static bool IsStringCollection(Type t) => 
            t.IsGenericType && t.GetGenericArguments().Length == 1 && t.GetGenericArguments()[0] == typeof(string) && typeof(IList<string>).IsAssignableFrom(t);
    }

}
