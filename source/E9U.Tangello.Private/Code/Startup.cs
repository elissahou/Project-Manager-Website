using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using E9U.Tangello.Data;


namespace E9U.Tangello.Private
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMainContext()
                //.AddSingleton<IRepository, HardCodedRepository>()
                //.AddSingleton<IRepository, FileBasedRepository>()
                //.AddSingleton<IRepository, DatabaseRepository>() // Cannot consume scoped MainDbContext in singleton DatabaseRepository.
                .AddScoped<IRepository, DatabaseRepository>()
                //.AddTransient<IRepository, DatabaseRepository>()
                ;

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Failed to route request. Terminal middleware app.Run() reached.");
            });
        }
    }
}
