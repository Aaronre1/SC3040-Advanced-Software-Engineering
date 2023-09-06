using ASE3040.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ASE3040.Application.Common.Interfaces
{
	public interface IApplicationDbContext
	{
		DbSet<ToDoList> ToDoLists { get; set; }

		DbSet<ToDoItem> ToDoItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
        DatabaseFacade Database { get; }
	}
}

