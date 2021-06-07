using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRServer.Hubs;

namespace OfficeWarServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(builder => builder
                .WithOrigins("http://aviads10-pc:3000" , "http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<GameHub>("/gameHub");
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
