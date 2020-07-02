using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class GetRootBudgetQuery
  {
    private IUnitOfWork unitOfWork;
    private IAsyncQueryService asyncQueryService;
    private Guid userId;

    public GetRootBudgetQuery(IUnitOfWork unitOfWork, IAsyncQueryService asyncQueryService, Guid userId)
    {
      this.unitOfWork = unitOfWork;
      this.asyncQueryService = asyncQueryService;
      this.userId = userId;
    }

    public async Task<Budget> Run()
    {
      IQueryable<Budget> budgets = this.unitOfWork.GetRepository<Budget>().GetAll();
      IQueryable<Budget> rootBudgetAsQueryable = this.asyncQueryService.Include(budgets, b => b.Duration);
      Budget root = await this.asyncQueryService.SingleOrDefaultAsync(rootBudgetAsQueryable, b => b.UserId == this.userId &&
                                                                                     b.ParentBudget == null);
      root.SubBudgets = await LoadBudgetTree(root);
      return root;
    }

    private async Task<IEnumerable<Budget>> LoadBudgetTree(Budget root)
    {
      IQueryable<Budget> subBudgets = this.unitOfWork.GetRepository<Budget>().GetAll()
                                                               .Where(b => b.ParentBudgetId == root.Id);
      List<Budget> loadedBudgets = await this.asyncQueryService.ToListAsync(subBudgets);

      foreach (Budget subBudget in loadedBudgets)
      {
        subBudget.SubBudgets = await LoadBudgetTree(subBudget);
      }
      return loadedBudgets;
    }
  }
}