using System;
using System.Threading.Tasks;
using budgettracker.business.Api.Converters;
using budgettracker.business.Api.Contracts.BudgetApi;
using budgettracker.business.Api.Interfaces;
using budgettracker.common.Models;
using budgettracker.data;

namespace budgettracker.business.Api
{
    public class BudgetApi : IBudgetApi
    {
        private readonly BudgetApiConverter _budgetApiConverter;

        public BudgetApi(BudgetApiConverter budgetApiConverter)
        {
            _budgetApiConverter = budgetApiConverter;
        }

        public async Task<BudgetResponseContract> CreateBudget(BudgetResquestContract budget)
        {
            Budget budgetValue = _budgetApiConverter.ToModel(budget);
            budgetValue.DurationStart = new DateTime();

            // TODO: Add UserId

            using (var budgetContext = new BudgetTrackerContext())
            {
                await budgetContext.Budgets.AddAsync(budgetValue);
            
                budgetContext.SaveChanges();
            }
        }
    }
}