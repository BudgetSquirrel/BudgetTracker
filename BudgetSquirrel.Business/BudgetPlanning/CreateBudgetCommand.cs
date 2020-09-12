using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class CreateBudgetCommand
  {
    private IUnitOfWork unitOfWork;
    private Guid parentBudgetId;
    private string name;
    private decimal setAmount;

    public CreateBudgetCommand(IUnitOfWork unitOfWork, Guid parentBudgetId, string name, decimal setAmount)
    {
      this.unitOfWork = unitOfWork;
      this.parentBudgetId = parentBudgetId;
      this.name = name;
      this.setAmount = setAmount;
    }

    public async Task<Guid> Run()
    {
      IRepository<Budget> budgetRepo = this.unitOfWork.GetRepository<Budget>();
      Budget parentBudget = await budgetRepo.GetAll().SingleAsync(b => b.Id == this.parentBudgetId);

      Budget budget = new Budget(parentBudget, this.name, 0);
      budget.SetFixedAmount(this.setAmount);

      this.unitOfWork.GetRepository<Budget>().Add(budget);
      await this.unitOfWork.SaveChangesAsync();
      return budget.Id;
    }
  }
}