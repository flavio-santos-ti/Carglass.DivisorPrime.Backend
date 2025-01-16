using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Carglass.DivisorPrime.Api.Middlewares;

[ExcludeFromCodeCoverage]
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log da requisição
        var request = context.Request;
        request.EnableBuffering();

        var builder = new StringBuilder();
        builder.AppendLine($"[Request] {request.Method} {request.Path}");
        builder.AppendLine($"Headers: {string.Join(", ", request.Headers.Select(h => $"{h.Key}: {h.Value}"))}");

        if (request.ContentLength > 0 && request.ContentType?.Contains("application/json") == true)
        {
            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;

            builder.AppendLine($"Body: {body}");
        }

        _logger.LogInformation(builder.ToString());

        // Chama o próximo middleware
        await _next(context);

        // Log da resposta
        var response = context.Response;
        _logger.LogInformation($"[Response] Status: {response.StatusCode}");
    }
}
