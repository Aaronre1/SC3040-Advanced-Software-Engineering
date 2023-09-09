namespace ASE3040.Domain.Entities
{
    public class ToDoList : BaseAuditableEntity
	{
		public string? Title { get; set; }

		public IList<ToDoItem> Items { get; private set; } = new List<ToDoItem>();
	}
}

