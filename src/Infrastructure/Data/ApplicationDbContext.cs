﻿using System.Reflection;
using ASE3040.Application.Common.Interfaces;
using ASE3040.Domain.Common;
using ASE3040.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASE3040.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(){}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<ToDoList> ToDoLists { get; set; }

		public DbSet<ToDoItem> ToDoItems { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}

