namespace OpenRedding.Api
{
    using System;
    using Core.Extensions;
    using Core.Salaries.Commands.SeedSalaryTable;
    using Data.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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
            var connectionString = Configuration["ConnectionString"];

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string was null or empty");
            }

            services.AddControllers();
            services.AddHttpClient<SalaryTableSeeder>(options => options.Timeout = TimeSpan.FromSeconds(30));
            services.AddOpenReddingPersistence(connectionString);
            services.AddOpenReddingCore();

            // var transparentCaliforniaSettings = Configuration.GetSection("TransparentCalifornia").Get<TransparentCaliforniaSettings>();
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}