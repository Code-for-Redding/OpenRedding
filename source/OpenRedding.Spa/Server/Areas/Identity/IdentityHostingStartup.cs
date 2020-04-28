using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenRedding.Spa.Server.Data;
using OpenRedding.Spa.Server.Models;

[assembly: HostingStartup(typeof(OpenRedding.Spa.Server.Areas.Identity.IdentityHostingStartup))]
namespace OpenRedding.Spa.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}