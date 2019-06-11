using budgettracker.common.Models;
using budgettracker.data.Models;
using System;
using System.Collections.Generic;

namespace budgettracker.data.Converters
{
    public class BudgetConverter : IConverter<Budget, BudgetModel>
    {
        public Budget ToBusinessModel(BudgetModel dataModel)
        {
            return new Budget()
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                SetAmount = dataModel.SetAmount,
                Duration = dataModel.Duration,
                BudgetStart = dataModel.BudgetStart,
                ParentBudgetId = dataModel.ParentBudgetId
            };
        }

        public List<Budget> ToBusinessModels(List<BudgetModel> dataModels)
        {
            throw new System.NotImplementedException();
        }

        public BudgetModel ToDataModel(Budget businessObject)
        {
            return new BudgetModel()
            {
                Id = businessObject.Id,
                Name = businessObject.Name,
                SetAmount = businessObject.SetAmount,
                Duration = businessObject.Duration,
                BudgetStart = businessObject.BudgetStart,
                ParentBudgetId = businessObject.ParentBudgetId
            };
        }

        public List<BudgetModel> ToDataModels(List<Budget> businessModels)
        {
            throw new System.NotImplementedException();
        }
    }
}
