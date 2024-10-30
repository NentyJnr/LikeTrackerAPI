using LikeTrackerAPI.Commons.Responses;
using Newtonsoft.Json;
using System.Net;

namespace LikeTrackerAPI.Commons.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;


        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;

        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "Application/json";
            var response = context.Response;

            var message = string.Empty;

            var errorResponse = new ErrorResponse
            {
                ResponseDescription = "An error ocurred",
                ResponseCode = ResponseCodes.EXCEPTION

            };
            CaseSwirching(exception, response, errorResponse);
            var result = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(result);
        }

        private void CaseSwirching(Exception exception, HttpResponse response, ErrorResponse errorResponse)
        {
            switch (exception)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid token"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.ResponseDescription = "request is Forbiden";
                        errorResponse.ResponseCode = ResponseCodes.INVALID_TOKEN;
                        _logger.LogError(ex, "Invalid token");
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.ResponseDescription = "Bad request";
                    errorResponse.ResponseCode = ResponseCodes.BAD_REQUEST;
                    _logger.LogError(ex, "Bad request");
                    break;
                case KeyNotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.ResponseDescription = "Not found";
                    errorResponse.ResponseCode = ResponseCodes.NOT_FOUND;
                    _logger.LogError(ex, "Not found");
                    break;


                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.ResponseDescription = "An error ocurred";
                    errorResponse.ResponseCode = ResponseCodes.EXCEPTION;
                    _logger.LogError("An error occurred");
                    break;
            }
            _logger.LogError(exception.Message);
            _logger.LogError($"An Error occurred {JsonConvert.SerializeObject(exception)}");
        }
    }
}
