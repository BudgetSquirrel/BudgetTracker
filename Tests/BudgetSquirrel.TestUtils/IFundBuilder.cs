using System;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.TestUtils
{
    public interface IFundBuilder : IFundPropertiesBuilder
    {
        IFundBuilder SetParentFund(Fund parentFund);
        IFundBuilder SetOwner(Guid userId);
        IFundBuilder SetOwner(User user);
        IFundBuilder SetDurationEndDayOfMonth(int value);
        IFundBuilder SetDurationRolloverEndDateOnSmallMonths(bool value);
        IFundBuilder SetDurationNumberDays(int value);

        Fund Build();
    }
}
