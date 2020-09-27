using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class EditRootBudgetCommand
  {
    private IUnitOfWork unitOfWork;
    private User editor;
    private Guid budgetId;
    private string newName;
    private decimal? newSetAmount;

    public EditRootBudgetCommand(
      IUnitOfWork unitOfWork,
      Guid id,
      User editor,
      string name,
      decimal? setAmount)
    {
      this.unitOfWork = unitOfWork;
      this.budgetId = id;
      this.editor = editor;
      this.newName = name;
      this.newSetAmount = setAmount;
    }

    public async Task Run()
    {
      IRepository<Budget> budgetRepository = this.unitOfWork.GetRepository<Budget>();
      Budget budgetToEdit = await budgetRepository.GetAll()
                                                  .Include(b => b.Fund)
                                                  .ThenInclude(c => c.Duration)
                                                  .SingleAsync(b => b.Id == this.budgetId);

      if (!budgetToEdit.Fund.IsOwnedBy(editor))
      {
        throw new InvalidOperationException("Unauthorized");
      }
      
      if (this.newName != null)
      {
        budgetToEdit.Fund.Name = this.newName;
      }
      if (this.newSetAmount.HasValue)
      {
        budgetToEdit.SetFixedAmount(this.newSetAmount.Value);
      }

      budgetRepository.Update(budgetToEdit);
      await this.unitOfWork.SaveChangesAsync();
    }
  }
}