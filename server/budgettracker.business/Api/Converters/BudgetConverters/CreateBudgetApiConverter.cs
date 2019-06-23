using budgettracker.common.Exceptions;
using budgettracker.common.Models;
using budgettracker.business.Api.Contracts.BudgetApi.CreateBudget;
using budgettracker.common.Models.BudgetDurations;
using budgettracker.business.Api.Contracts.BudgetApi.BudgetDurations;

using System;
using System.Collections.Generic;

namespace budgettracker.business.Api.Converters.BudgetConverters
{
    public class CreateBudgetApiConverter
    {
        public static Budget ToModel(CreateBudgetRequestContract requestContract)
        {
            return new Budget()
            {
                Name = requestContract.Name,
                SetAmount = requestContract.SetAmount,
                Duration = GeneralBudgetApiConverter.GetBudgetDuration(requestContract.Duration),
                ParentBudgetId = requestContract.ParentBudgetId,
                BudgetStart = requestContract.BudgetStart ?? new DateTime()
            };
        }

        public static Budget ToModel(CreateBudgetResponseContract responseContract)
        {
            throw new System.NotImplementedException();
        }

        public static CreateBudgetRequestContract ToRequestContract(Budget model)
        {
            throw new System.NotImplementedException();
        }

        public static CreateBudgetResponseContract ToResponseContract(Budget model)
        {
            return new CreateBudgetResponseContract()
            {
                Id = model.Id,
                Name = model.Name,
                SetAmount = model.SetAmount,
                Duration = GeneralBudgetApiConverter.GetBudgetDuration(model.Duration),
                BudgetStart = model.BudgetStart,
                ParentBudgetId = model.ParentBudgetId
            };
        }
    }
}
