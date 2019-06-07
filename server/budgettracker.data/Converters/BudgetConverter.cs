using budgettracker.common.Models;
using budgettracker.data.Models;

namespace budgettracker.data.Converters 
{
    public class BudgetConverter : IConverter<Budget, BudgetModel>
    {
        public Budget ToBusinessModel(BudgetModel dataModel)
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
    }
}