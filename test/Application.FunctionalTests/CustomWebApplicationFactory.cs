using ASE3040.Application.Common.Interfaces;
using ASE3040.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace Application.FunctionalTests;

using static Testing;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            IConfigurationRoot integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IUser>()
                .AddTransient(_ => Mock.Of<IUser>(s => s.UserId == GetUserId()));

            services.RemoveAll<DbContextOptions<ApplicationDbContext>>()
                .AddDbContext<ApplicationDbContext>((sp, options) =>
                {
                    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                    options.UseInMemoryDatabase("test.db");
                });
        });

        builder.UseEnvironment("Testing");
    }
}