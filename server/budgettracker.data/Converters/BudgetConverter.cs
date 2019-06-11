using budgettracker.common.Models;
using budgettracker.data.Models;

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
    }
}