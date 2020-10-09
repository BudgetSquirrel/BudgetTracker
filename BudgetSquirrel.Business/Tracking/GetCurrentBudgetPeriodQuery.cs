using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetSquirrel.Business.BudgetPlanning;

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
        Budget currentRootBudget = await this.unitOfWork.GetRepository<Budget>()
                                                            .GetAll()
                                                            .Include(b => b.Fund)
                                                            .Include(b => b.BudgetPeriod)
                                                            .SingleAsync(b => b.Fund.ParentFundId == null &&
                                                                         b.Fund.UserId == this.userId &&
                                                                         b.BudgetPeriod.StartDate <= now &&
                                                                         b.BudgetPeriod.EndDate > now);
        return currentRootBudget.BudgetPeriod;
        }
    }
}