using System;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class EditMonthlyBookendedBudgetDuration
  {
    private IUnitOfWork unitOfWork;
    private Guid budgetId;
    private User editor;
    private int endDayOfMonth;
    private bool rolloverEndDateOnShortMonths;

    public EditMonthlyBookendedBudgetDuration(IUnitOfWork unitOfWork, Guid budgetId, User editor, int endDayOfMonth, bool rolloverEndDateOnShortMonths)
    {
      this.unitOfWork = unitOfWork;
      this.budgetId = budgetId;
      this.editor = editor;
      this.endDayOfMonth = endDayOfMonth;
      this.rolloverEndDateOnShortMonths = rolloverEndDateOnShortMonths;
    }
    
    public async Task Run()
    {
      IRepository<Budget> budgetRepository = this.unitOfWork.GetRepository<Budget>();
      IRepository<BudgetDurationBase> budgetDurationRepository = this.unitOfWork.GetRepository<BudgetDurationBase>();
      Budget budgetOfInterest = await budgetRepository.GetAll()
                                                      .Include(b => b.Fund)
                                                      .ThenInclude(c => c.Duration)
                                                      .SingleOrDefaultAsync(b => b.Id == budgetId);

      if (!budgetOfInterest.Fund.IsOwnedBy(this.editor))
      {
        throw new InvalidOperationException("Unauthorized");
      }
      if (this.endDayOfMonth < 1 || this.endDayOfMonth > 31)
      {
        throw new ArgumentException("End day of month for monthly bookended durations must be between 1 and 31 (inclusive)");
      }

      MonthlyBookEndedDuration monthlyBookEndedDuration;
      if (!(budgetOfInterest.Fund.Duration is MonthlyBookEndedDuration))
      {
        monthlyBookEndedDuration = new MonthlyBookEndedDuration(budgetOfInterest.Fund.DurationId, this.endDayOfMonth, this.rolloverEndDateOnShortMonths);
        budgetOfInterest.Fund.Duration = monthlyBookEndedDuration;
      }
      else
      {
        monthlyBookEndedDuration = (MonthlyBookEndedDuration) budgetOfInterest.Fund.Duration;
        monthlyBookEndedDuration.EndDayOfMonth = this.endDayOfMonth;
        monthlyBookEndedDuration.RolloverEndDateOnSmallMonths = this.rolloverEndDateOnShortMonths;
      }

      budgetDurationRepository.Update(monthlyBookEndedDuration);
      await this.unitOfWork.SaveChangesAsync();
    }
  }
}