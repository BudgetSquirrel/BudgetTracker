using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Infrastructure;
using BudgetSquirrel.Business.Tracking;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class GetRootBudgetQuery
  {
    private IUnitOfWork unitOfWork;
    private BudgetLoader budgetLoader;
    private Guid userId;

    public GetRootBudgetQuery(IUnitOfWork unitOfWork, BudgetLoader budgetLoader, Guid userId)
    {
      this.unitOfWork = unitOfWork;
      this.budgetLoader = budgetLoader;
      this.userId = userId;
    }

    /// <summary>
    /// Loads the full tree of funds and budgets
    /// </summary>
    public async Task<Fund> Run()
    {
      Fund root = await this.unitOfWork.GetRepository<Fund>()
                                                  .GetAll()
                                                  .Include(f => f.Duration)
                                                  .SingleAsync(b => b.UserId == this.userId &&
                                                                             b.ParentFund == null);
      DateTime currentTime = DateTime.Now;
      Budget currentRootBudget = await this.unitOfWork.GetRepository<Budget>()
                                                  .GetAll()
                                                  .Include(b => b.BudgetPeriod)
                                                  .SingleAsync(b => b.FundId == root.Id && 
                                                                            (b.BudgetPeriod.StartDate <= currentTime && b.BudgetPeriod.EndDate >= currentTime));
      
      currentRootBudget.Fund = root;
      root.HistoricalBudgets = new List<Budget>() { currentRootBudget };

      root.SubFunds = await this.budgetLoader.LoadFundTree(root, currentRootBudget.BudgetPeriod);
      return root;
    }
  }
}