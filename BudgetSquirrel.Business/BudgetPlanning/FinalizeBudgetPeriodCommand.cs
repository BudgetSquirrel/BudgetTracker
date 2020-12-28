using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Infrastructure;

namespace BudgetSquirrel.Business.BudgetPlanning
{
    public class FinalizeBudgetPeriodCommand
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly FundLoader budgetLoader;
        private readonly Guid budgetId;
        private readonly User currentUser;

        public FinalizeBudgetPeriodCommand(IUnitOfWork unitOfWork, FundLoader budgetLoader, Guid budgetId, User currentUser)
        {
            this.unitOfWork = unitOfWork;
            this.budgetLoader = budgetLoader;
            this.budgetId = budgetId;
            this.currentUser = currentUser;
        }

        public async Task Run()
        {
            IRepository<Budget> budgetRepository = this.unitOfWork.GetRepository<Budget>();

            Budget budget = await budgetRepository.GetAll().SingleOrDefaultAsync(b => b.Id == this.budgetId);

            // TODO: We need a way to get the full tree by budget id rather than user id. If we ever
            // allow mutliple trees per user, this might not get the right tree.
            GetRootBudgetQuery rootBudgetQuery = new GetRootBudgetQuery(unitOfWork, this.budgetLoader, currentUser.Id);
            Fund rootFund = await rootBudgetQuery.Run();

            if (!rootFund.IsOwnedBy(this.currentUser))
            {
                throw new InvalidOperationException("Unauthorized");
            }        

            if (!rootFund.CurrentBudget.IsFullyAllocated)
            {
                throw new InvalidOperationException("NOT_FULLY_ALLOCATED");
            }

            // TODO: I'm not sure how I feel about setting the finalized date on the root budget only
            // and leaving the sub budgets un-finalized. Maybe it is better to put this on the budget
            // period model. Or maybe we make a Plan model that represents the entire tree? Or maybe
            // we finalize all the sub-budgets individually?
            budget.SetFinalizedDate();
            budgetRepository.Update(budget);

            await this.unitOfWork.SaveChangesAsync();

            // TODO: Now go update the budgets with their new planned amount using the CreateTransactionCommand.
        }
    }
}