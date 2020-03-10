using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSwag.AspNetCore;
using NJsonSchema;
using Newtonsoft.Json;
using LandonApi.Filters;
using LandonApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LandonApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // TODO: Swap out for real db in production
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //utilizing InMemory DB
            services.AddDbContext<HotelApiDbContext>(options =>
            {
                options.UseInMemoryDatabase("landondb");
            });

            services
                .AddMvc(options =>
                {
                    options.Filters.Add<JsonExceptionFilters>();
                    options.Filters.Add<RequireHttpsOrClose>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddRouting(options => options.LowercaseUrls = true);

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", policy =>
                 {
                     policy.AllowAnyOrigin(); //Should be used for Dev purpose only
                                              //policy.WithOrigins("http://example.com");
                 });
            });

            services.Configure<HotelInfo>(Configuration.GetSection("Info"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerUi3WithApiExplorer(options =>
                {
                    options.GeneratorSettings
                        .DefaultPropertyNameHandling
                    = NJsonSchema.PropertyNameHandling.CamelCase;
                });

            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowAllOrigin"); //use cors policy name in the pipeline
            //app.UseHttpsRedirection(); // implemented filter to reject http 
            app.UseMvc();
        }
    }
}
