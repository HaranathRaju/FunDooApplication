using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ModelLayer.Exceptions;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace FunDooNotes.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate requestDelegate,
            ILogger<ExceptionMiddleware> logger)
        {
            this.requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, "AppException caught");

                if (!context.Response.HasStarted)
                {
                    context.Response.Clear(); 

                    context.Response.StatusCode = ex.Statuscode;
                    context.Response.ContentType = "application/json";

                    var response = new
                    {
                        success = false,
                        message = ex.Message
                    };

                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(response));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception caught");

                if (!context.Response.HasStarted)
                {
                    context.Response.Clear(); 

                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var response = new
                    {
                        success = false,
                        message = "Internal Server Error"
                    };

                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(response));
                }
            }
        }
    }
}
