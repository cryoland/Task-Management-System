using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TMS.WebUI.Common
{
    public class UrlRewriterMiddleware
    {
        private readonly RequestDelegate _next;

        public UrlRewriterMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            if (context.Request.Path == "/Account/Login")
            {
                context.Response.Redirect($"/Identity/Account/Login{context.Request.QueryString}");
            }
            else if (context.Request.Path == "/Account/AccessDenied")
            {
                context.Response.Redirect($"/Identity/Account/AccessDenied{context.Request.QueryString}");
            }
            else
            {
                await _next(context);
            }
        }
    }

    public static class UrlRewriterMiddlewareExtensions
    {
        public static IApplicationBuilder UseUrlRewriterMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UrlRewriterMiddleware>();
        }
    }
}
