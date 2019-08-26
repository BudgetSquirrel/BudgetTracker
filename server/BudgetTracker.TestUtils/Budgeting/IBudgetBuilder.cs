using System;

namespace BudgetTracker.TestUtils.Budgeting
{
    public interface IBudgetBuilder<E>
    {
        IBudgetBuilder SetName(string name);
        IBudgetBuilder SetPercentAmount(double? percentAmount);
        IBudgetBuilder SetParentBudget(Guid? parentId);
        IBudgetBuilder SetFixedAmount(decimal? setAmount);
        IBudgetBuilder SetDurationStartDayOfMonth(int? value);
        IBudgetBuilder SetDurationEndDayOfMonth(int? value);
        IBudgetBuilder SetDurationRolloverStartDateOnSmallMonths(bool? value);
        IBudgetBuilder SetDurationRolloverEndDateOnSmallMonths(bool? value);
        IBudgetBuilder SetDurationNumberDays(int? value);

        E Build();
    }
}
