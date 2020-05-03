using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class GetRootBudgetQuery
  {
    private IQueryable<Budget> allBudgets;
    private IAsyncQueryService asyncQueryService;
    private Guid userId;

    public GetRootBudgetQuery(IQueryable<Budget> allBudgets, IAsyncQueryService asyncQueryService, Guid userId)
    {
      this.allBudgets = allBudgets;
      this.asyncQueryService = asyncQueryService;
      this.userId = userId;
    }

    public Task<Budget> Run()
    {
      return this.asyncQueryService.SingleOrDefaultAsync(this.allBudgets, b => b.UserId == this.userId &&
                                                                               b.ParentBudget == null);
    }
  }
}