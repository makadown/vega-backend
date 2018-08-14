using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using vega_backend.Persistence;
using AutoMapper;
using vega_backend.Core;

namespace vega_backend
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
            /* Importante para evitar errores 
               "No 'Access-Control-Allow-Origin' header is present on the requested resource"
               en el front */
           services.AddCors();
           services.AddAutoMapper();
           services.AddScoped<IVehicleRepository, VehicleRepository>();
           services.AddScoped<IUnitOfWork,UnitOfWork>();
          
            /*
            Esto es para usar las apis con los Models y evitar el error de 
            Reference Loop 
             */
             services.AddMvc().AddJsonOptions(
                      options => 
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
              );

            /* Tomado de 
https://developer.okta.com/blog/2018/04/26/build-crud-app-aspnetcore-angular#configure-the-database-connection-on-startup
             */
            services.AddDbContext<VegaDbContext>( options =>
                                    options.UseSqlServer(Configuration.GetConnectionString("VegaContext")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } /*
            else
            {
                app.UseHsts();
            } */

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

            app.UseDefaultFiles();
            app.UseStaticFiles();
            /* Me caga el Https Redirection */
            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
