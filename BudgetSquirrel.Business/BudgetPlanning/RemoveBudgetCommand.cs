using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class RemoveBudgetCommand
  {
    private IUnitOfWork unitOfWork;
    private Guid budgetId;
    private User remover;

    public RemoveBudgetCommand(IUnitOfWork unitOfWork, Guid budgetId, User remover)
    {
      this.unitOfWork = unitOfWork;
      this.budgetId = budgetId;
      this.remover = remover;
    }

    public async Task Run()
    {
      IRepository<Budget> budgetRepository = this.unitOfWork.GetRepository<Budget>();
      Budget budgetToRemove = await budgetRepository.GetAll().Include(b => b.Fund).SingleOrDefaultAsync(b => b.Id == this.budgetId);

      if (!budgetToRemove.Fund.IsOwnedBy(this.remover))
      {
        throw new InvalidOperationException("Unauthorized");
      }

      budgetRepository.Remove(budgetToRemove);
      await this.unitOfWork.SaveChangesAsync();
    }
  }
}