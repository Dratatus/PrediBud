namespace Backend.Middlewares
{
    using System.Net;
    using System.Text.Json;
    using Microsoft.Extensions.Logging;
    using Backend.services.Email;

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (ex is ApiException apiException)
                {
                    await HandleExceptionAsync(context, apiException);
                }
                else
                {
                    _logger.LogError(ex, "Unexpected error occurred in the application.");

                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                        await emailService.SendErrorReportAsync(ex);
                    }

                    await HandleExceptionAsync(context, ex);
                }
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            int statusCode;
            string message;

            if (exception is ApiException apiException)
            {
                statusCode = apiException.StatusCode;
                message = apiException.Message;
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = "An unexpected error occurred.";
            }

            response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new
            {
                error = message,
                statusCode = statusCode,
                details = exception is ApiException ? null : exception.Message
            });

            return response.WriteAsync(result);
        }
    }
}
