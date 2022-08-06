using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MiddleWare.MiddleWares;

namespace MiddleWare
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiddleWare", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddleWare v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.Run()

            /*
            app.Run kendinden sonra gelen middleware'ları çalıştırmaz.!
            */
            //app.Run(async context => Console.WriteLine("Middleware 1."));
            //app.Run(async context => Console.WriteLine("Middleware 2."));

            //app.use()
            /*
            app.use ile sıralı middware yazılabilir. async => asenkron çalışmadır. Geri dönüşü beklemenin gereksiz olduğu noktalarda asenkron çalışılarak cevap beklemeden devam edilebilir.
            fakat geri dönüş önemliyse (örneğin database işlemi gibi) await ile middleware bekletilir. cevaptan sonra satırlara devam eder.
            *//*
            app.Use(async(context, next)=>{
                Console.WriteLine("Middlware 1 başladı!");
                await next.Invoke();
                Console.WriteLine("Middleware 1 sonlandırılıyor." + context.Request);
            });
            
            app.Use(async(context, next)=>{
                Console.WriteLine("Middlware 2 başladı!");
                await next.Invoke();
                Console.WriteLine("Middleware 2 sonlandırılıyor.");
            });

            app.Use(async(context, next)=>{
                Console.WriteLine("Middlware 3 başladı!");
                await next.Invoke();
                Console.WriteLine("Middleware 3 sonlandırılıyor.");
            });*/

            app.UseHello();

            app.Use(async(context, next)=>{
                Console.WriteLine("Use Middlware başladı!");
                await next.Invoke();
            });

            //app.map()

            /*
            roota göre middleware'leri yönetmeyi yarar. Öğrneğin /example rootuna bir request geldiğinde bu map tetiklenir.
            */
            app.Map("/example",internalApp =>
            internalApp.Run(async context =>
            {
                Console.WriteLine("/example middleware tetiklendi");
                await context.Response.WriteAsync("/example middleware ttetiklendi.");
            }));

            // app.MapWhen()

            /*
            sadece path'e göre değil request'in içerisindeki bir değere göre tetiklenmesini istersek mapwhen metotu kullanılır.
            */
            app.MapWhen(x =>x.Request.Method=="GET", internalApp=>
            internalApp.Run(async context=>
                    {
                        Console.WriteLine("Mapwhen middleware tetiklendi");
                        await context.Response.WriteAsync("Mapwhen middleware tetiklendi");
                    }
                )
            );



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
