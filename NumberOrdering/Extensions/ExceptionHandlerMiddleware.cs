using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NumberOrdering.Extensions
{
    /// <summary>
    /// ExceptionHandlerMiddleware
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// ExceptionHandlerMiddleware
        /// </summary>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoked when an error  or exception is raised
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Handles the exception message and return as response in formatted manner
        /// </summary>
        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
