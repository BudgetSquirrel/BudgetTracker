using System;

namespace BudgetSquirrel.Business.BudgetPlanning
{
    public abstract class BudgetDurationBase
    {
        public Guid Id { get; private set; }

        public BudgetDurationBase(Guid id)
        {
            Id = id;
        }

        public BudgetDurationBase() {}
    }
}
