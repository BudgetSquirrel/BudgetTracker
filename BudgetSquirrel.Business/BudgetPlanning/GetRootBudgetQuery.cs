using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Infrastructure;

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

    public async Task<Budget> Run()
    {
      Budget root = await this.unitOfWork.GetRepository<Budget>()
                                                  .GetAll()
                                                  .Include(b => b.Duration)
                                                  .SingleOrDefaultAsync(b => b.UserId == this.userId &&
                                                                             b.ParentBudget == null);
      root.SubBudgets = await LoadBudgetTree(root);
      return root;
    }

    private async Task<IEnumerable<Budget>> LoadBudgetTree(Budget root)
    {
      IEnumerable<Budget> loadedSubBudgets = await this.unitOfWork.GetRepository<Budget>()
                                                  .GetAll()
                                                  .Include(b => b.Duration)
                                                  .Where(b => b.ParentBudgetId == root.Id)
                                                  .ToListAsync();

      foreach (Budget subBudget in loadedSubBudgets)
      {
        subBudget.SubBudgets = await LoadBudgetTree(subBudget);
      }
      return loadedSubBudgets;
    }
  }
}