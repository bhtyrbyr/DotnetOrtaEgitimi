using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Services;

namespace WebAPI.Middlewares
{
    public class CustomExceptionMiddlware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        public CustomExceptionMiddlware(RequestDelegate next, [FromServices] ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request]  HTTP " + context.Request.Method + " - " + context.Request.Path;
                _loggerService.Write(message);
                await _next(context);
                watch.Stop();
                message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.Milliseconds + " ms\n" + context.Items.Count;
                _loggerService.Write(message);
            }catch(System.Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "[Error]    HTTP " + context.Request.Method + " - " + context.Request.Path + " - " + context.Response.StatusCode + " Error Message " + ex.Message + " in " + watch.Elapsed.Milliseconds + "ms";
            _loggerService.Write(message);
            var result = JsonConvert.SerializeObject(new {error = ex.Message}, Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }
    public static class CustomExceptionMiddlwareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddlware>();
        }
    }
}