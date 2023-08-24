using System;
namespace ASE3040.Domain.Entities
{
	public class ToDoItem : BaseEntity
	{
		public int ListId { get; set; }

		public string? Title { get; set; }

		public string? Note { get; set; }

		public bool Done { get; set; }

	}
}

