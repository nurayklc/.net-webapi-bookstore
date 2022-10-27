using Internal;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
            Console.WriteLine(message);
            await _next(context);
            watch.Stop();
            var resMessage = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path 
                                +  " responded " + context.Response.StatusCode
                                + "  in " + watch.Elapse.TotalMilliseconds + " ms ";
            Console.WriteLine(resMessage);
        }

    }
    
    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }

    }
}