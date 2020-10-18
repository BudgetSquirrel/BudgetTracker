using System;
using BudgetSquirrel.Business;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.TestUtils
{
    public interface IBudgetPeriodBuilder
    {
        IBudgetPeriodBuilder ForDuration(BudgetDurationBase duration);

        IBudgetPeriodBuilder SetStartDate(DateTime startDate);

        BudgetPeriod Build();
    }
}