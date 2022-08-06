using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MiddleWare.MiddleWares
{
    public class HelloMiddleWare
    {
        private readonly RequestDelegate _next;
        public HelloMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("Hello middleware.");
            await _next.Invoke(context);
            Console.WriteLine("Bye middleware.");
        }
    }

    public static class HelloMiddleWareExtension
    {
        public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloMiddleWare>();
        }
    }
}