using System;

namespace BudgetTracker.TestUtils.Budgeting
{
    public interface IBudgetBuilder<E>
    {
        IBudgetBuilder<E> SetName(string name);
        IBudgetBuilder<E> SetPercentAmount(double? percentAmount);
        IBudgetBuilder<E> SetParentBudget(Guid? parentId, bool clearDurationValues = true);
        IBudgetBuilder<E> SetFixedAmount(decimal? setAmount);
        IBudgetBuilder<E> SetDurationStartDayOfMonth(int? value);
        IBudgetBuilder<E> SetDurationEndDayOfMonth(int? value);
        IBudgetBuilder<E> SetDurationRolloverStartDateOnSmallMonths(bool? value);
        IBudgetBuilder<E> SetDurationRolloverEndDateOnSmallMonths(bool? value);
        IBudgetBuilder<E> SetDurationNumberDays(int? value);

        E Build();
    }
}
