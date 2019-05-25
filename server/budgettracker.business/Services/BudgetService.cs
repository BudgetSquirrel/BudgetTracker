using System.Threading.Tasks;
using budgettracker.business.Api.Contracts.BudgetApi;
using budgettracker.business.Api.Converters;
using budgettracker.common.Models;
using budgettracker.data;

namespace budgettracker.business.Services
{
    public class BudgetService : IBudgetService
    {

        private readonly BudgetApiConverter _budgetApiConverter;

        public BudgetService(BudgetApiConverter budgetApiConverter)
        {
            _budgetApiConverter = budgetApiConverter;
        }

        public async Task CreateBudget(BudgetResquestContract budget)
        {
            Budget budgetValue = _budgetApiConverter.ToModel(budget);

            using (var budgetContext = new BudgetTrackerContext())
            {
                await budgetContext.Budgets.AddAsync(budgetValue);
                budgetContext.SaveChanges();
            }
        }
    }
}