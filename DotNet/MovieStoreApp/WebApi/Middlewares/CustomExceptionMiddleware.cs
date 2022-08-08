using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddlware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerServices _loggerService;

        public CustomExceptionMiddlware(RequestDelegate next, [FromServices] ILoggerServices loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[REQUEST] HTTP " + context.Request.Method + " - " + context.Request.Path + " - " + context.Request.RouteValues;
                _loggerService.write(message);
                await _next(context);
                watch.Stop();
                message = "[RESPONSE] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.Milliseconds + " ms\n" + context.Items.Count;
                _loggerService.write(message);
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
            _loggerService.write(message);
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