using System;
using BudgetSquirrel.Business;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.TestUtils
{
    public interface IBudgetPeriodBuilder
    {
        IBudgetPeriodBuilder ForRootBudget(Budget rootBudget);

        IBudgetPeriodBuilder SetStartDate(DateTime startDate);

        BudgetPeriod Build();
    }
}