using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberOrdering.Extensions
{
    /// <summary>
    /// ExceptionMiddlewareExtensions
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Add Exception Handler extension
        /// </summary>
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
