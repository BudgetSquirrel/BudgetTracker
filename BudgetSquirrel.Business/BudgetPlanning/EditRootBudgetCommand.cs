using System;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class EditRootBudgetCommand
  {
    private IAsyncQueryService asyncQueryService;
    private IQueryable<Budget> budgets;
    private Guid budgetId;
    private string newName;
    private decimal? newSetAmount;

    public EditRootBudgetCommand(IAsyncQueryService asyncQueryService, IQueryable<Budget> budgets, Guid id, string name, decimal? setAmount)
    {
      this.budgets = budgets;
    }

    public async Task Run()
    {
      Budget budgetToEdit = await this.asyncQueryService.SingleOrDefaultAsync(this.budgets, b => b.Id == this.budgetId);
      
      if (this.newName == null)
      {
        budgetToEdit.Name = this.newName;
      }
      if (this.newSetAmount.HasValue)
      {
        budgetToEdit.SetFixedAmount(this.newSetAmount.Value);
      }

      await this.asyncQueryService.SaveChangesAsync();
    }
  }
}