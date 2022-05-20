using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality_WebApi.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MiddlewareLogging
    {
        private readonly RequestDelegate _next;

        private ILogger _logger;
        public MiddlewareLogging(RequestDelegate next , ILogger<MiddlewareLogging> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation(httpContext.Request.Path + "\t\t" + DateTime.Now);
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareLoggingExtensions
    {
        public static IApplicationBuilder UseMiddlewareLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddlewareLogging>();
        }
    }
}
