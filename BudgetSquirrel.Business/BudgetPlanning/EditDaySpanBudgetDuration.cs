using System;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class EditDaySpanBudgetDuration
  {
    private IUnitOfWork unitOfWork;
    private Guid budgetId;
    private User editor;
    private int numberDays;

    public EditDaySpanBudgetDuration(IUnitOfWork unitOfWork, Guid budgetId, User editor, int numberDays)
    {
      this.unitOfWork = unitOfWork;
      this.budgetId = budgetId;
      this.editor = editor;
      this.numberDays = numberDays;
    }
    
    public async Task Run()
    {
      IRepository<Budget> budgetRepository = this.unitOfWork.GetRepository<Budget>();
      IRepository<BudgetDurationBase> budgetDurationRepository = this.unitOfWork.GetRepository<BudgetDurationBase>();
      Budget budgetOfInterest = await budgetRepository.GetAll()
                                                      .Include(b => b.Duration)
                                                      .SingleOrDefaultAsync(b => b.Id == budgetId);

      if (!budgetOfInterest.IsOwnedBy(this.editor))
      {
        throw new InvalidOperationException("Unauthorized");
      }
      
      DaySpanDuration daySpanDuration;
      if (!(budgetOfInterest.Duration is DaySpanDuration))
      {
        daySpanDuration = new DaySpanDuration(budgetOfInterest.DurationId, this.numberDays);
        budgetOfInterest.Duration = daySpanDuration;
      }
      else
      {
        daySpanDuration = (DaySpanDuration) budgetOfInterest.Duration;
        daySpanDuration.NumberDays = this.numberDays;
      }

      budgetDurationRepository.Update(daySpanDuration);
      await this.unitOfWork.SaveChangesAsync();
    }
  }
}