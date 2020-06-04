using System;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class CreateBudgetCommand
  {
    private IUnitOfWork unitOfWork;
    IRepository<Budget> budgetRepository;
    private Budget parentBudget;
    private string name;
    private decimal setAmount;

    public CreateBudgetCommand(IUnitOfWork unitOfWork, IRepository<Budget> budgetRepository, Budget parentBudget, string name, decimal setAmount)
    {
      this.unitOfWork = unitOfWork;
      this.budgetRepository = budgetRepository;
      this.parentBudget = parentBudget;
      this.name = name;
      this.setAmount = setAmount;
    }

    public async Task<Guid> Run()
    {
      Budget budget = new Budget(this.parentBudget, this.name, 0);
      this.budgetRepository.Add(budget);
      await this.unitOfWork.SaveChangesAsync();
      return budget.Id;
    }
  }
}