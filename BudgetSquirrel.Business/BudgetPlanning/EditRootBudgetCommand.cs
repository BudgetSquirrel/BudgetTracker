using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class EditRootBudgetCommand
  {
    private IAsyncQueryService asyncQueryService;
    private IQueryable<Budget> budgets;
    private User editor;
    private Guid budgetId;
    private string newName;
    private decimal? newSetAmount;

    public EditRootBudgetCommand(IAsyncQueryService asyncQueryService, IQueryable<Budget> budgets, Guid id, User editor, string name, decimal? setAmount)
    {
      this.asyncQueryService = asyncQueryService;
      this.budgets = budgets;
      this.budgetId = id;
      this.editor = editor;
      this.newName = name;
      this.newSetAmount = setAmount;
    }

    public async Task Run()
    {
      Budget budgetToEdit = await this.asyncQueryService.SingleOrDefaultAsync(this.budgets, b => b.Id == this.budgetId);

      if (!budgetToEdit.IsOwnedBy(editor))
      {
        throw new InvalidOperationException("Unauthorized");
      }
      
      if (this.newName != null)
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