namespace ASE3040.Domain.Entities
{
    public class ToDoList : BaseAuditableEntity
	{
		public string? Title { get; set; }

		public IList<ToDoItem> Items { get; private set; } = new List<ToDoItem>();
		
		public decimal TripBudget { get; set; }

		public IList<BudgetItem> BudgetList { get; private set; } = new List<BudgetItem>();

		public decimal CalculateTotalBudget()
		{
			var itemBudget = Items.Sum(i => i.Budget.Budget);
			var otherBudget = BudgetList.Sum(i => i.Budget);
			return itemBudget + otherBudget;
		}
	}
}

