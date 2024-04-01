using Ardalis.GuardClauses;
using Newtonsoft.Json;
using WebAPI.Middleware.Responses;

namespace WebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger("ExceptionsLogger");
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (FormatException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (OperationCanceledException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex}, Date: {DateTime.UtcNow}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            BaseResponse response;
            int statusCode;
            switch (exception)
            {
                case OperationCanceledException:
                    statusCode = 400;
                    response = new ExceptionResponse
                    {
                        ExceptionMessage = exception.Message,
                        StatusCode = statusCode,
                        Message = "Operation was cancelled"
                    };
                    break;
                case FormatException:
                    statusCode = 400;
                    response = new ExceptionResponse
                    {
                        ExceptionMessage = exception.Message,
                        StatusCode = statusCode,
                        Message = "Wrong picture data format"
                    };
                    break;
                case NotFoundException:
                    statusCode = 404;
                    response = new ExceptionResponse
                    {
                        ExceptionMessage = exception.Message,
                        Message = "Entity was not found",
                        StatusCode = statusCode
                    };
                    break;
                default:
                    statusCode = 500;
                    response = new ExceptionResponse
                    {
                        Message = "There is an exception occured",
                        StatusCode = statusCode
                    };
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
