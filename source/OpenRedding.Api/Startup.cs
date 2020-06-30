namespace OpenRedding.Api
{
    using System;
    using System.Text.Json;
    using Core.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using OpenRedding.Api.Middleware;
    using OpenRedding.Core.Configuration;
    using OpenRedding.Core.Infrastructure.Services;
    using OpenRedding.Infrastructure.Extensions;
    using OpenRedding.Infrastructure.Services;

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
            // Retrieve the connection string from environment source and cancel bootstrap if none is found
            var connectionString = Configuration["ConnectionString"];

            // Bind Azure configuration
            var configurationUrls = Configuration.GetSection("Urls");
            services.Configure<ApplicationSettings>(configurationUrls);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Database connection string is null");
            }

            // Adding API layer dependencies
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            // Service dependencies
            services.AddHttpClient<ISalaryTableSeeder, SalaryTableSeeder>(nameof(SalaryTableSeeder), options => options.Timeout = TimeSpan.FromSeconds(30));
            services.AddHttpClient<IZoningTableSeeder, ZoningTableSeeder>(nameof(ZoningTableSeeder), options => options.Timeout = TimeSpan.FromSeconds(30));

            // Project dependencies
            services.AddOpenReddingCore();
            services.AddOpenReddingInfrastructure(Configuration);

            services.AddCors();

            // Override built in model state validation
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseConduitErrorHandlerMiddleware();

            // TODO: Update this
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
