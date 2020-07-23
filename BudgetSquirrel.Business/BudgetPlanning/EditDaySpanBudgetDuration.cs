using System;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;

namespace BudgetSquirrel.Business.BudgetPlanning
{
  public class EditDaySpanBudgetDuration
  {
    private IUnitOfWork unitOfWork;
    private IAsyncQueryService asyncQueryService;
    private Guid budgetId;
    private User editor;
    private int numberDays;

    public EditDaySpanBudgetDuration(IUnitOfWork unitOfWork, IAsyncQueryService asyncQueryService, Guid budgetId, User editor, int numberDays)
    {
      this.unitOfWork = unitOfWork;
      this.asyncQueryService = asyncQueryService;
      this.budgetId = budgetId;
      this.editor = editor;
      this.numberDays = numberDays;
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
      
      DaySpanDuration daySpanDuration;
      if (!(budgetOfInterest.Duration is DaySpanDuration))
      {
        budgetDurationRepository.Remove(budgetOfInterest.Duration);
        budgetOfInterest.DurationId = default(Guid); // Need to figure a better way to switch sub types.

        await this.unitOfWork.SaveChangesAsync();

        daySpanDuration = new DaySpanDuration(this.numberDays);
        budgetDurationRepository.Add(daySpanDuration);
        budgetOfInterest.Duration = daySpanDuration;
      }

      DaySpanDuration monthlyBookEndedDuration = (DaySpanDuration) budgetOfInterest.Duration;
      monthlyBookEndedDuration.NumberDays = this.numberDays;

      await this.unitOfWork.SaveChangesAsync();
    }
  }
}