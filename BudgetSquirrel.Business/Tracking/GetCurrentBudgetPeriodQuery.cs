using System;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetSquirrel.Business.Tracking
{
    public class GetCurrentBudgetPeriodQuery
    {
        private IUnitOfWork unitOfWork;
        private IAsyncQueryService asyncQueryService;
        private Guid userId;

        public GetCurrentBudgetPeriodQuery(IUnitOfWork unitOfWork, IAsyncQueryService asyncQueryService, Guid userId)
        {
            this.unitOfWork = unitOfWork;
            this.asyncQueryService = asyncQueryService;
            this.userId = userId;
        }

        public async Task<BudgetPeriod> Run()
        {
        DateTime now = DateTime.Now;
        IQueryable<BudgetPeriod> budgetPeriods = this.unitOfWork.GetRepository<BudgetPeriod>().GetAll();
        budgetPeriods = this.asyncQueryService.Include(budgetPeriods, period => period.Budget);
        BudgetPeriod current = await this.asyncQueryService.SingleOrDefaultAsync(budgetPeriods, period => period.Budget.UserId == this.userId &&
                                                                                                          period.StartDate <= now &&
                                                                                                          period.EndDate > now);
        return current;
        }
    }
}