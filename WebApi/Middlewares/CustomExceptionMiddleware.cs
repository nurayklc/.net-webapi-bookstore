using System.Xml;
using System.Net;
using Internal;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            try
            {
                string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
                Console.WriteLine(message);
                await _next(context);
                watch.Stop();
                var resMessage = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path 
                                    +  " responded " + context.Response.StatusCode
                                    + "  in " + watch.Elapse.TotalMilliseconds + " ms ";
                Console.WriteLine(resMessage);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        public Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "[Error]     HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error message "
                                + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms ";

            Console.WriteLine(message);

            var result = JsonConvert.SerializeObject(new {error = ex.Message}, Formatting.None);
            return context.Response.WriteAsync(result);
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