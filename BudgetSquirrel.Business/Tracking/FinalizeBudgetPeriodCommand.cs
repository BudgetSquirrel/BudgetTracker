using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Business.Auth;

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

            var budgetPeriods = budgetPeriodRepository.GetAll();

            BudgetPeriod budgetPeriod = budgetPeriods.Where(x => x.BudgetId == budgetId).FirstOrDefault();

            if (!budgetPeriod.Budget.IsOwnedBy(this.currentUser))
            {
                throw new InvalidOperationException("Unauthorized");
            }        

            if (!CanBudgetBeFinalized(budgetPeriod))
            {
                throw new InvalidOperationException("Budget can't not be finalized be review your budget again.");
            }

            budgetPeriod.SetFinalizedDate();
            budgetPeriodRepository.Update(budgetPeriod);

            await this.unitOfWork.SaveChangesAsync();
        }

        private bool CanBudgetBeFinalized(BudgetPeriod budgetPeriod)
        {
            var rootBudgetSetAmount = budgetPeriod.Budget.SetAmount;

            var amountToMatch = budgetPeriod.Budget.GetAllSetAmountRecursive();

            return rootBudgetSetAmount == amountToMatch;
        }
    }
}