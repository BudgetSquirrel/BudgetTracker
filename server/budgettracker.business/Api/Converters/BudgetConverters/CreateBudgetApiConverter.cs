using budgettracker.common.Models;
using budgettracker.business.Api.Contracts.BudgetApi.CreateBudget;
using System;

namespace budgettracker.business.Api.Converters.BudgetConverters
{
    public class CreateBudgetApiConverter : IApiConverter<Budget, CreateBudgetRequestContract, CreateBudgetResponseContract>
    {
        public Budget ToModel(CreateBudgetRequestContract requestContract)
        {
            return new Budget()
            {
                Name = requestContract.Name,
                SetAmount = requestContract.SetAmount,
                Duration = requestContract.Duration,
                ParentBudgetId = requestContract.ParentBudgetId
            };
        }

        public Budget ToModel(CreateBudgetResponseContract responseContract)
        {
            throw new System.NotImplementedException();
        }

        public CreateBudgetRequestContract ToRequestContract(Budget model)
        {
            throw new System.NotImplementedException();
        }

        public CreateBudgetResponseContract ToResponseContract(Budget model)
        {
            return new CreateBudgetResponseContract()
            {
                Id = model.Id,
                Name = model.Name,
                SetAmount = model.SetAmount,
                Duration = model.Duration,
                BudgetStart = model.BudgetStart,
                ParentBudgetId = model.ParentBudgetId
            };
        }
    }
}