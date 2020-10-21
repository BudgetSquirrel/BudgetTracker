using System;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;

namespace BudgetSquirrel.TestUtils
{
    public interface IFundPropertiesBuilder
    {
        IFundBuilder SetName(string name);
        IFundBuilder SetFundBalance(decimal fundBalance);
    }
}
