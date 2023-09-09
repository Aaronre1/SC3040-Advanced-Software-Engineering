using System;
using ASE3040.Application.Common.Interfaces;
using ASE3040.Infrastructure.Data;
using ASE3040.Infrastructure.Data.Interceptors;
using ASE3040.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASE3040.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var provider = configuration.GetValue("Provider", "SqlServer");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            switch (provider)
            {
                case "Sqlite":
                    services.AddDbContext<SqliteApplicationDbContext>((sp, options) =>
                    {
                        options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                        options.UseSqlite(connectionString);
                    });
                    services.AddScoped<IApplicationDbContext>(p => p.GetRequiredService<SqliteApplicationDbContext>());
                    break;
                case "SqlServer":
                    services.AddDbContext<ApplicationDbContext>((sp, options) =>
                    {
                        options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                        options.UseSqlServer(connectionString);
                    });
                    services.AddScoped<IApplicationDbContext>(p => p.GetRequiredService<ApplicationDbContext>());
                    break;
                case "InMemory":
                    services.AddDbContext<ApplicationDbContext>((sp, options) =>
                    {
                        options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                        options.UseInMemoryDatabase("app.db");
                    });
                    services.AddScoped<IApplicationDbContext>(p => p.GetRequiredService<ApplicationDbContext>());
                    break;
                default:
                    throw new Exception($"Unsupported provider: {provider}");
            }

            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}