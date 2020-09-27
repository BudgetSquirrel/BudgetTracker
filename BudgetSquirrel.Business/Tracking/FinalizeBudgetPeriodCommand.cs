using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.Business.Tracking
{
    public class FinalizeBudgetPeriodCommand
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Guid budgetId;
        private readonly User currentUser;

        public FinalizeBudgetPeriodCommand(IUnitOfWork unitOfWork, Guid budgetId, User currentUser)
        {
            this.unitOfWork = unitOfWork;
            this.budgetId = budgetId;
            this.currentUser = currentUser;
        }

        public async Task Run()
        {
            IRepository<BudgetPeriod> budgetPeriodRepository = this.unitOfWork.GetRepository<BudgetPeriod>();

            BudgetPeriod budgetPeriod = await budgetPeriodRepository.GetAll().SingleOrDefaultAsync(y => y.BudgetId == budgetId);

            GetRootBudgetQuery rootBudgetQuery = new GetRootBudgetQuery(unitOfWork, currentUser.Id);

            Fund rootFund = await rootBudgetQuery.Run();

            if (!rootFund.IsOwnedBy(this.currentUser))
            {
                throw new InvalidOperationException("Unauthorized");
            }        

            if (!rootFund.CurrentBudget.IsFullyAllocated)
            {
                throw new InvalidOperationException("Budget can't not be finalized please review your budgets again.");
            }

            budgetPeriod.SetFinalizedDate();
            budgetPeriodRepository.Update(budgetPeriod);

            await this.unitOfWork.SaveChangesAsync();
        }
    }
}