using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Middleware
{
    /// <summary>
    /// user-defined Strict Token Middleware
    /// </summary>
    public class SessionManagementMiddleware
    {
        private readonly RequestDelegate _next;
        public SessionManagementMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Session.Keys.Contains("name"))
                context.Session.SetString("name", "User");
            await _next(context);
        }
    }

    public static partial class MiddlewareExtension
    {
        /// <summary>
        /// set user-defined Strict Token Middleware
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseSessionManagement(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SessionManagementMiddleware>();
        }
    }
}
