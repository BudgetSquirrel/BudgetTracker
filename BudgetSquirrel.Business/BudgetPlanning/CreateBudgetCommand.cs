using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Infrastructure;
using BudgetSquirrel.Business.Tracking;

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

    /// <summary>
    /// Creates a new fund and the intial budget.
    /// </summary>
    public async Task<Guid> Run()
    {
      IRepository<Budget> budgetRepo = this.unitOfWork.GetRepository<Budget>();
      IRepository<Fund> fundRepo = this.unitOfWork.GetRepository<Fund>();
      Budget parentBudget = await budgetRepo.GetAll().Include(b => b.Fund).SingleAsync(b => b.Id == this.parentBudgetId);
      parentBudget.Fund.HistoricalBudgets = new List<Budget>() { parentBudget };

      Fund fund = new Fund(parentBudget.Fund, this.name, 0);
      Budget budget = new Budget(fund, parentBudget.BudgetPeriodId, this.setAmount);
      
      budgetRepo.Add(budget);
      await this.unitOfWork.SaveChangesAsync();
      return budget.Id;
    }
  }
}