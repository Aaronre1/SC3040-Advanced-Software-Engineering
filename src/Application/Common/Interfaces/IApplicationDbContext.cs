using ASE3040.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASE3040.Application.Common.Interfaces
{
	public interface IApplicationDbContext
	{
		DbSet<ToDoList> ToDoLists { get; set; }

		DbSet<ToDoItem> ToDoItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}

