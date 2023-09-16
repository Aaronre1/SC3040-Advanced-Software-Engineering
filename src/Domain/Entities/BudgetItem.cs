namespace ASE3040.Domain.Entities;

public class BudgetItem
{
    public string? Title { get; set; }
    public decimal Budget { get; set; }
    public decimal ActualExpense { get; set; }
}


// List (Trip to ...)
    // Item 1 (Lunch) 
        // Budget 1 - $10
    // Item 2 (attraction 1)
        // Budget 2 - $20
        
    // Other Budget Items
        // Budget Train $50
        // Budget Hotel $500
        
    // Spending Budget $10,000
    // Actual Expenses $5,000