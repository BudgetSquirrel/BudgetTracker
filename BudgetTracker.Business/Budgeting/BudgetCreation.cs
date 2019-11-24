using BudgetTracker.Business.Auth;
using BudgetTracker.Common;
using BudgetTracker.Common.Exceptions;
using BudgetTracker.Business.Ports.Exceptions;
using BudgetTracker.Business.Ports.Repositories;
using System;
using System.Threading.Tasks;
using BudgetTracker.Business.Converters.BudgetConverters;

namespace BudgetTracker.Business.Budgeting
{
    public class BudgetCreation
    {
        private IBudgetRepository _budgetRepository;
        private BudgetValidator _budgetValidator;
        private BudgetMessageConverter _createBudgetApiConverter;

        public BudgetCreation(IBudgetRepository budgetRepository, BudgetValidator budgetValidator,
            BudgetMessageConverter createBudgetApiConverter)
        {
            _budgetRepository = budgetRepository;
            _budgetValidator = budgetValidator;
            _createBudgetApiConverter = createBudgetApiConverter;
        }

        public async Task<Budget> CreateBudgetForUser(CreateBudgetRequestMessage createInput, User user)
        {
            Budget budgetCreateObject = _createBudgetApiConverter.ToModel(createInput);
            budgetCreateObject.Owner = user;

            if (!budgetCreateObject.IsRootBudget)
            {
                budgetCreateObject.ParentBudget = await _budgetRepository.GetBudget(budgetCreateObject.ParentBudgetId.Value);
                if (user.Id != budgetCreateObject.ParentBudget.Owner.Id)
                {
                    throw new AuthorizationException("This user does not have access to the specified parent budget.");
                }
                budgetCreateObject.Duration = budgetCreateObject.ParentBudget.Duration;
            }

            budgetCreateObject.SetAmount = budgetCreateObject.CalculateBudgetSetAmount();
            budgetCreateObject = await _budgetRepository.CreateBudget(budgetCreateObject);

            return budgetCreateObject;
        }
    }
}
