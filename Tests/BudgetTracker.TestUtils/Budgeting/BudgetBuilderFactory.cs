using BudgetTracker.Business.Budgeting;
using System;

namespace BudgetTracker.TestUtils.Budgeting
{
    public class BudgetBuilderFactory<E>
    {
        public BudgetBuilderFactory()
        {
            if (typeof(E) != typeof(Budget))
            {
                throw new NotImplementedException("BudgetBuilderFactory cannot return a builder of type " + typeof(E).ToString());
            }
        }

        public IBudgetBuilder<E> GetBuilder()
        {
            return (IBudgetBuilder<E>) (new BudgetBuilder());
        }
    }
}
