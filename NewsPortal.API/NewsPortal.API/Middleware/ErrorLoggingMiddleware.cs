using NewsPortal.Domain.Interfaces;
using System.Net;
using System.Text.Json;
using NewsPortal.Domain.Entities;

namespace NewsPortal.API.Middleware
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        public ErrorLoggingMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var loggingReposttory = scope.ServiceProvider.GetRequiredService<ILoggingRepository>();
                    await HandleExceptionAsync(context, ex, loggingReposttory);
                }
                    
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, ILoggingRepository _loggingRepository)
        {
            // Log the error to the database
            var routeData = context.GetRouteData();
            var controllerName = routeData?.Values["controller"]?.ToString();
            var ActionName = routeData?.Values["action"]?.ToString();
            var errorLog = new ErrorLogs
            {
                ExceptionMessage = exception.Message,
                StackTrace = exception.StackTrace,
                Source = exception.Source + ". Controller Name : " + controllerName + ". Method Name : " + ActionName
            };

            await _loggingRepository.LogErrorAsync(errorLog);

            // Prepare a custom response for the client
            var response = new
            {
                message = "An unexpected error occurred. Please contact support.",

            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

    }
}
