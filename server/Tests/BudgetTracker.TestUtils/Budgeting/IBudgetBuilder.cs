using BudgetTracker.Business.Auth;
using System;

namespace BudgetTracker.TestUtils.Budgeting
{
    public interface IBudgetBuilder<E>
    {
        IBudgetBuilder<E> SetName(string name);
        IBudgetBuilder<E> SetParentBudget(Guid? parentId, bool clearDurationValues = true);
        IBudgetBuilder<E> SetParentBudget(E parent, bool clearDurationValues = true);
        IBudgetBuilder<E> SetDurationStartDayOfMonth(int? value);
        IBudgetBuilder<E> SetDurationEndDayOfMonth(int? value);
        IBudgetBuilder<E> SetDurationRolloverStartDateOnSmallMonths(bool? value);
        IBudgetBuilder<E> SetDurationRolloverEndDateOnSmallMonths(bool? value);
        IBudgetBuilder<E> SetDurationNumberDays(int? value);
        IBudgetBuilder<E> SetOwner(User owner);

        /// <summary>
        /// Set the percent amount for this budget. If you set the percentAmount,
        /// this will take priority over the set amount in most cases. So you
        /// may need to call SetFixedAmount(null) on this builder to clear it.
        /// </summary>
        IBudgetBuilder<E> SetPercentAmount(double? percentAmount);

        /// <summary>
        /// Set the fixed amount for this budget. If you want this budget to
        /// be seen as a fixed amount budget, you will need to clear the percent
        /// amount by calling SetPercentAmount(null) on this builder. This is
        /// because percent amount is given priority over set amount in most
        /// cases of calculation.
        /// </summary>
        IBudgetBuilder<E> SetFixedAmount(decimal? setAmount);

        E Build();
    }
}
