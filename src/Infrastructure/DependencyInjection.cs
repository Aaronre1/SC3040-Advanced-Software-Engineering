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

			services.AddDbContext<ApplicationDbContext>((sp, options) =>
			{
				if (environment.IsDevelopment())
				{
                    options.UseSqlite(connectionString);
				}
				else
				{
					options.UseSqlServer(connectionString);
				}
            });

			services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

			services.AddScoped<ApplicationDbContextInitialiser>();
			
			return services;
		}
	}
}

