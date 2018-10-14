using DotNetPerformance.Shared.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace DotNetPerformance.Shared.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            //Check if there was an exeption. If not continue the pipeline
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogLevel logLevel;

                // Write correct http response error
                var response = context.Response;

                if (ex is NullReferenceException)
                {
                    logLevel = LogLevel.Error;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                }
                else if (ex is UnauthorizedAccessException)
                {
                    logLevel = LogLevel.Error;
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                }
                else if (ex is ValidationException)
                {
                    logLevel = LogLevel.Error;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.ContentType = "application/json";
                    await response.WriteAsync(JsonConvert.SerializeObject(((ValidationException)ex).Message));
                }
                else
                {
                    // Other exeptions are caused by wrong code
                    // Example ArgumentNullExecption => Some business code needed a certain argument but it was null
                    // This is caused by unsufficient checking in the controller. 
                    // Otherwise the ModelState should already have catched this issue.
                    logLevel = LogLevel.Critical;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                // Log exeption
                _logger.LogException(logLevel, ex);
            }
        }
    }
}
