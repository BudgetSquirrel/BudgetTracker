using System;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class EditMonthlyBookendedBudgetDuration
  {
    private IUnitOfWork unitOfWork;
    private IAsyncQueryService asyncQueryService;
    private Guid budgetId;
    private User editor;
    private int endDayOfMonth;
    private bool rolloverEndDateOnShortMonths;

    public EditMonthlyBookendedBudgetDuration(IUnitOfWork unitOfWork, IAsyncQueryService asyncQueryService, Guid budgetId, User editor, int endDayOfMonth, bool rolloverEndDateOnShortMonths)
    {
      this.unitOfWork = unitOfWork;
      this.asyncQueryService = asyncQueryService;
      this.budgetId = budgetId;
      this.editor = editor;
      this.endDayOfMonth = endDayOfMonth;
      this.rolloverEndDateOnShortMonths = rolloverEndDateOnShortMonths;
    }
    
    public async Task Run()
    {
      IRepository<Budget> budgetRepository = this.unitOfWork.GetRepository<Budget>();
      IRepository<BudgetDurationBase> budgetDurationRepository = this.unitOfWork.GetRepository<BudgetDurationBase>();
      Budget budgetOfInterest = await this.asyncQueryService.SingleOrDefaultAsync(
        this.asyncQueryService.Include(
          budgetRepository.GetAll(), b => b.Duration),
        b => b.Id == budgetId);

      if (!budgetOfInterest.IsOwnedBy(this.editor))
      {
        throw new InvalidOperationException("Unauthorized");
      }
      if (this.endDayOfMonth < 1 || this.endDayOfMonth > 31)
      {
        throw new ArgumentException("End day of month for monthly bookended durations must be between 1 and 31 (inclusive)");
      }

      MonthlyBookEndedDuration monthlyBookEndedDuration;
      if (!(budgetOfInterest.Duration is MonthlyBookEndedDuration))
      {
        budgetDurationRepository.Remove(budgetOfInterest.Duration);
        await this.unitOfWork.SaveChangesAsync();

        monthlyBookEndedDuration = new MonthlyBookEndedDuration(this.endDayOfMonth, this.rolloverEndDateOnShortMonths);
        budgetDurationRepository.Add(monthlyBookEndedDuration);
        budgetOfInterest.DurationId = monthlyBookEndedDuration.Id;
      }
      else
      {
        monthlyBookEndedDuration = (MonthlyBookEndedDuration) budgetOfInterest.Duration;
        monthlyBookEndedDuration.EndDayOfMonth = this.endDayOfMonth;
        monthlyBookEndedDuration.RolloverEndDateOnSmallMonths = this.rolloverEndDateOnShortMonths;
      }

      await this.unitOfWork.SaveChangesAsync();
    }
  }
}