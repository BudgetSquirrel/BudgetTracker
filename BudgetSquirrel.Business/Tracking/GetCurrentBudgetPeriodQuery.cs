using System;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.Business.Tracking
{
    public class GetCurrentBudgetPeriodQuery
    {
        private IUnitOfWork unitOfWork;
        private Guid userId;

        public GetCurrentBudgetPeriodQuery(IUnitOfWork unitOfWork, Guid userId)
        {
            this.unitOfWork = unitOfWork;
            this.userId = userId;
        }

        public async Task<BudgetPeriod> Run()
        {
        DateTime now = DateTime.Now;
        IQuerySet<BudgetPeriod> budgetPeriods = this.unitOfWork.GetRepository<BudgetPeriod>()
                                                                .GetAll()
                                                                .Include(period => period.Budget);
        BudgetPeriod current = await budgetPeriods.SingleOrDefaultAsync(period => period.Budget.UserId == this.userId &&
                                                                                  period.StartDate <= now &&
                                                                                  period.EndDate > now);
        return current;
        }
    }
}