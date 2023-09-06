﻿using System;
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

			switch (provider)
			{
				case "Sqlite":
					services.AddDbContext<SqliteApplicationDbContext>(options =>
					{
						options.UseSqlite(connectionString);
					});
					services.AddScoped<IApplicationDbContext>(p => p.GetRequiredService<SqliteApplicationDbContext>());
					break;
				case "SqlServer":
					services.AddDbContext<ApplicationDbContext>(options =>
					{
						options.UseSqlServer(connectionString);
					});
					services.AddScoped<IApplicationDbContext>(p => p.GetRequiredService<ApplicationDbContext>());
					break;
				default:
					throw new Exception($"Unsupported provider: {provider}");
			}
			
			services.AddScoped<ApplicationDbContextInitialiser>();

			// services.AddAuthentication().AddMicrosoftAccount(options =>
			// {
			// 	options.ClientId = configuration["MICROSOFT_PROVIDER_AUTHENTICATION_CLIENT_ID"];
			// 	options.ClientSecret = configuration["MICROSOFT_PROVIDER_AUTHENTICATION_SECRET"];
			// });
			
			return services;
		}
	}
}

