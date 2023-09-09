using System;
using ASE3040.Application.Common.Interfaces;
using ASE3040.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ASE3040.Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

            await initialiser.InitialiseAsync();

            //await initialiser.SeedAsync();
        }
    }

    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly IApplicationDbContext _context;

        public ApplicationDbContextInitialiser(
            ILogger<ApplicationDbContextInitialiser> logger,
            IApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            if (!_context.ToDoLists.Any())
            {
                var list = new ToDoList
                {
                    Title = "Default ToDo List",
                    Items =
                    {
                        new ToDoItem { Title = "To do item 1" },
                        new ToDoItem { Title = "To do item 2" },
                        new ToDoItem { Title = "To do item 3" }
                    }
                };

                _context.ToDoLists.Add(list);

                await _context.SaveChangesAsync(default);
            }

        }
    }
}

