using budgettracker.business.Api.Contracts.BudgetApi;
using budgettracker.common.Models;

namespace budgettracker.business.Api.Converters
{
    public class BudgetApiConverter : IApiConverter<Budget, BudgetResquestContract, BudgetResponseContract>
    {
        public Budget ToModel(BudgetResquestContract requestContract)
        {
            return new Budget
            {
                Name = requestContract.Name,
                SetAmount = requestContract.SetAmount,
                Duration = requestContract.Duration,
                DurationStart = requestContract.DurationStart
            };
        }

        public Budget ToModel(BudgetResponseContract responseContract)
        {
            throw new System.NotImplementedException();
        }

        public BudgetResquestContract ToRequestContract(Budget model)
        {
            throw new System.NotImplementedException();
        }

        public BudgetResponseContract ToResponseContract(Budget model)
        {
            throw new System.NotImplementedException();
        }
    }
}