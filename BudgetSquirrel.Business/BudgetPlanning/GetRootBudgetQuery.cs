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
    private FundLoader budgetLoader;
    private Guid userId;

    public GetRootBudgetQuery(IUnitOfWork unitOfWork, FundLoader budgetLoader, Guid userId)
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
      IQuerySet<Budget> rootBudgets = this.unitOfWork.GetRepository<Budget>()
                                                  .GetAll()
                                                  .Include(b => b.BudgetPeriod)
                                                  .Where(b => b.FundId == root.Id);
      Budget currentRootBudget = await BudgetPeriodQueryUtils.GetForDate(rootBudgets, DateTime.Now);
      
      currentRootBudget.Fund = root;
      root.HistoricalBudgets = new List<Budget>() { currentRootBudget };

      root = await this.budgetLoader.LoadFundTree(root, currentRootBudget.BudgetPeriod);
      return root;
    }
  }
}