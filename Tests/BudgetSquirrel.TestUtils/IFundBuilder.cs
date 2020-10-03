using System;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.TestUtils
{
    public interface IFundBuilder
    {
        IFundBuilder SetParentFund(Fund parentFund);
        IFundBuilder SetOwner(Guid userId);
        IFundBuilder SetName(string name);
        IFundBuilder SetDurationEndDayOfMonth(int value);
        IFundBuilder SetDurationRolloverEndDateOnSmallMonths(bool value);
        IFundBuilder SetDurationNumberDays(int value);
        IFundBuilder SetFundBalance(decimal fundBalance);

        Fund Build();
    }
}
