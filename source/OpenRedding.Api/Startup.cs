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

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string was null or empty");
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
            services.AddHttpClient<ISalaryTableSeeder, SalaryTableSeeder>(options => options.Timeout = TimeSpan.FromSeconds(30));

            // Project dependencies
            services.AddOpenReddingCore();
            services.AddOpenReddingPersistenceInfrastructure(connectionString);

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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}