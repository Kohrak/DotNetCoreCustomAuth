using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace DotNetCoreCustomAuth.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAuthentication("CustomTokenAuth").AddCustomTokenAuth();
            services.AddTokenAuthServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Custom Auth Api Example", Version = "v1" });
                c.AddSecurityDefinition(CustomTokenAuth.CustomTokenAuthHeader, new ApiKeyScheme()
                {
                    In = "Header",
                    Description = "Please include the api key provided in the documentation",
                    Name = CustomTokenAuth.CustomTokenAuthHeader
                });
                c.AddSecurityRequirement(
                    new Dictionary<string, IEnumerable<string>>()
                    {
                        { CustomTokenAuth.CustomTokenAuthHeader, Enumerable.Empty<string>() }
                    }
                );
                c.DescribeAllEnumsAsStrings();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Custom Auth Api Example";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Custom Auth Api Example V1");
                c.RoutePrefix = string.Empty;

            });
        }
    }
}
