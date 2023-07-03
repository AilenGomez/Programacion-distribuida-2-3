using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PuertaDeEntrada.Application;
using PuertaDeEntrada.Application.Common.Interfaces;
using PuertaDeEntrada.Application.Common.Utils;
using PuertaDeEntrada.Infrastructure;
using PuertaDeEntrada.Infrastructure.HealthChecks;
using PuertaDeEntrada.WebUI.Common;
using PuertaDeEntrada.WebUI.Extensions;
using System.Collections.Generic;

namespace PuertaDeEntrada.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        private readonly string _policyName = "CorsPolicy";
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);

            services.AddApplication();

            services.AddCors(opt =>
            {
                opt.AddPolicy(_policyName, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddResponseCaching();

            services.AddHttpContextAccessor();

            services.AddHealthChecks().AddSqlServer(VariablesUtil.GetConnectionString(Configuration))
                .AddCheck<VentaEntradaHC>(nameof(VentaEntradaHC));


            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IApplicationDbContext>())
                .AddJsonOptions(opt => opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PuertaDeEntrada" });
            });

            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

                //app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(_policyName);

            app.UseResponseCaching();

            app.UseCustomExceptionHandler();


            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            // Swagger UI with traefik stripprefix middleware support. Source code:
            // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1253#issuecomment-1019382999
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, request) =>
                {
                    const string prefixHeader = "X-Forwarded-Prefix";
                    if (!request.Headers.ContainsKey(prefixHeader))
                        return;
                    var serverUrl = request.Headers[prefixHeader];
                    swaggerDoc.Servers = new List<OpenApiServer>()
                    {
                        new() { Description = "Microservice", Url = serverUrl }
                    };
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "v1");
                c.RoutePrefix = "swagger";
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapControllers();
            });
            app.UseRouting()
                .Build();
        }
    }
}
