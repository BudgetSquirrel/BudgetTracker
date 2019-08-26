using BudgetTracker.Business.Budgeting;
using System;

namespace BudgetTracker.TestUtils.Budgeting
{
    public class BudgetBuilderFactory<E>
    {
        public BudgetBuilderFactory()
        {
            if (typeof(E) != typeof(CreateBudgetRequestMessage) ||
                typeof(E) != typeof(Budget))
            {
                throw Exception("BudgetBuilderFactory cannot return a builder of type " + typeof(E).ToString());
            }
        }

        public IBudgetBuilder<E> GetBuilder()
        {
            if (typeof(E) == typeof(CreateBudgetRequestMessage))
                return new CreateBudgetRequestMessageBuilder();
            else if (typeof(E) == typeof(Budget))
                return new BudgetBuilder();
        }
    }
}
