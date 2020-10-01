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
    private Guid userId;

    public GetRootBudgetQuery(IUnitOfWork unitOfWork, Guid userId)
    {
      this.unitOfWork = unitOfWork;
      this.userId = userId;
    }

    /// <summary>
    /// Loads the full tree of funds and budgets
    /// </summary>
    public async Task<Fund> Run()
    {
      Fund root = await this.unitOfWork.GetRepository<Fund>()
                                                  .GetAll()
                                                  .SingleOrDefaultAsync(b => b.UserId == this.userId &&
                                                                             b.ParentFund == null);
      DateTime currentTime = DateTime.Now;
      Budget currentRootBudget = await this.unitOfWork.GetRepository<Budget>()
                                                  .GetAll()
                                                  .Include(b => b.BudgetPeriod)
                                                  .SingleOrDefaultAsync(b => b.FundId == root.Id && 
                                                                            (b.BudgetPeriod.StartDate <= currentTime && b.BudgetPeriod.EndDate >= currentTime));

      root.SubFunds = await LoadFundTree(root, currentRootBudget.BudgetPeriod);
      return root;
    }

    private async Task<IEnumerable<Fund>> LoadFundTree(Fund root, BudgetPeriod budgetPeriod)
    {
      IEnumerable<Fund> loadedSubFunds = await this.unitOfWork.GetRepository<Fund>()
                                                  .GetAll()                                          
                                                  .Where(b => b.ParentFundId == root.Id)
                                                  .ToListAsync();

      IEnumerable<Guid> loadedSubFundsIds = loadedSubFunds.Select(x => x.Id);

      IEnumerable<Budget> budgets = await this.unitOfWork.GetRepository<Budget>()
                                                         .GetAll()
                                                         .Include(b => b.BudgetPeriod)
                                                         .Where(b => loadedSubFundsIds.Contains(b.FundId))
                                                         .Where(b => b.BudgetPeriod.StartDate == budgetPeriod.StartDate &&
                                                                     b.BudgetPeriod.EndDate == budgetPeriod.EndDate)
                                                         .ToListAsync();

      // Put the budgets on their corresponding fund. This is equivelant to:
      // loadedSubFunds = loadedSubFunds inner join budgets on budget.FundId = fund.Id
      // There should only be one budget per fund, otherwise, this will bug out.
      loadedSubFunds = loadedSubFunds.Join(
        budgets,
        f => f.Id,
        b => b.FundId,
        (f, b) => {
          f.Budgets = new List<Budget>() { b };
          return f;
        });

      foreach (Fund subFunds in loadedSubFunds)
      {
        subFunds.SubFunds = await LoadFundTree(subFunds, budgetPeriod);
      }
      return loadedSubFunds;
    }
  }
}