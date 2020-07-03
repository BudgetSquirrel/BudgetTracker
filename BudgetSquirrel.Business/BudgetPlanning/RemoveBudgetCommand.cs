using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class RemoveBudgetCommand
  {
    private IUnitOfWork unitOfWork;
    private IAsyncQueryService asyncQueryService;
    private Guid budgetId;
    private User remover;

    public RemoveBudgetCommand(IUnitOfWork unitOfWork, IAsyncQueryService asyncQueryService, Guid budgetId, User remover)
    {
      this.unitOfWork = unitOfWork;
      this.asyncQueryService = asyncQueryService;
      this.budgetId = budgetId;
      this.remover = remover;
    }

    public async Task Run()
    {
      IRepository<Budget> budgetRepository = this.unitOfWork.GetRepository<Budget>();
      IQueryable<Budget> budgets = budgetRepository.GetAll();
      Budget budgetToRemove = await this.asyncQueryService.SingleOrDefaultAsync(budgets, b => b.Id == this.budgetId);

      if (!budgetToRemove.IsOwnedBy(this.remover))
      {
        throw new InvalidOperationException("Unauthorized");
      }

      budgetRepository.Remove(budgetToRemove);
      await this.unitOfWork.SaveChangesAsync();
    }
  }
}