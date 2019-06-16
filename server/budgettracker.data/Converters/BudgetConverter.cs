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

        public static List<Budget> ToBusinessModels(List<BudgetModel> dataModels)
        {
            List<Budget> businessConversions = new List<Budget>();
            foreach (BudgetModel data in dataModels)
            {
                Budget budget = new BudgetConverter().ToBusinessModel(data);
                businessConversions.Add(budget);
            }
            return businessConversions;
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

        public static List<BudgetModel> ToDataModels(List<Budget> businessModels)
        {
            List<BudgetModel> dataConversions = new List<BudgetModel>();
            foreach (Budget budgetObect in businessModels)
            {
                BudgetModel data = new BudgetConverter().ToDataModel(budgetObect);
                dataConversions.Add(data);
            }
            return dataConversions;
        }
    }
}
