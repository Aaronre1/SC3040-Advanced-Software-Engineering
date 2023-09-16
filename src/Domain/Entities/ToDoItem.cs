using System;
namespace ASE3040.Domain.Entities
{
	public class ToDoItem : BaseAuditableEntity
	{
		public int ListId { get; set; }

		public string? Title { get; set; }

		public string? Note { get; set; }

		public bool Done { get; set; }

		public ToDoList List { get; set; } = null!;

		public BudgetItem Budget { get; set; } = null!;

		public DateTime DateTime { get; set; }
	}
}

