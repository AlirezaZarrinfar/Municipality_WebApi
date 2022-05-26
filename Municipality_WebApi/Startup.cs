using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Municipality_WebApi.Application.Services.BillsPaymentService;
using Municipality_WebApi.Application.Services.BillsService;
using Municipality_WebApi.Application.Services.CustomerService;
using Municipality_WebApi.Application.UnitOfWork;
using Municipality_WebApi.Middleware;
//using Municipality_WebApi.Middleware;
using Municipality_WebApi.Persistance.Connection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality_WebApi
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
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = "localhost:6379";
            //    options.InstanceName = "";
            //});
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            services.AddScoped(p => redis.GetDatabase());
            services.AddScoped<IUnitofwork, Unitofwork>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Municipality_WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Municipality_WebApi v1"));
            }
            app.UseHttpsRedirection();
            app.UseMiddleware<MiddlewareLogging>();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
