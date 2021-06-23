using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Transportadora.Api.Exceptions.Exceptions
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = exception.Message;

            if (exception is DbUpdateException dbUpdateException)
            {
                message = dbUpdateException.InnerException.Message;
            }

            if (exception is InvalidOperationException invalidOperationException)
            {
                message = invalidOperationException.Message;
            }

            if (exception is TransportadoraException TransportadoraException)
            {
                context.Response.StatusCode = TransportadoraException.HttpStatus;
            }

            var json = new
            {
                context.Response.StatusCode,
                message
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(json));
        }
    }
}