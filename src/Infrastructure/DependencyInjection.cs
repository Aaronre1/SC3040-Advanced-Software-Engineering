using System;
using ASE3040.Application.Common.Interfaces;
using ASE3040.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASE3040.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			var provider = configuration.GetValue("Provider", "SqlServer");

			services.AddDbContext<ApplicationDbContext>((sp, options) =>
			{
				switch (provider)
				{
					case "Sqlite": options.UseSqlite(connectionString);
						break;
					case "SqlServer": options.UseSqlServer(connectionString);
						break;
					default:
						throw new Exception($"Unsupported provider: {provider}");
				}
            });

			services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

			services.AddScoped<ApplicationDbContextInitialiser>();
			
			return services;
		}
	}
}

