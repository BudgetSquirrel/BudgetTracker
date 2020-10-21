using System;
using System.Collections.Generic;
using BudgetSquirrel.Business;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.TestUtils.Budgeting;

namespace BudgetSquirrel.TestUtils
{
    public class BudgetTreeBuilder
    {
        private BuilderFactoryFixture builderFactory;

        private IBudgetBuilder rootBudgetBuilder;

        private Budget rootBudget;

        private Func<IFundBuilder, IFundBuilder> rootFundOptions;

        private List<BudgetTreeBuilder> subBudgetBuilders = new List<BudgetTreeBuilder>();

        public BudgetTreeBuilder(BuilderFactoryFixture builderFactory)
        {
            this.builderFactory = builderFactory;
            this.rootBudgetBuilder = builderFactory.BudgetBuilder;
            rootFundOptions = (x) => x;
        }

        public BudgetTreeBuilder SetParentBudget(Budget parentBudget)
        {
            Func<IFundBuilder, IFundBuilder> originalFundOptions = ((Func<IFundBuilder, IFundBuilder>) this.rootFundOptions.Clone());
            this.rootFundOptions = fundBuilder => originalFundOptions(fundBuilder).SetParentFund(parentBudget.Fund);
            this.rootBudgetBuilder.SetBudgetPeriod(parentBudget.BudgetPeriod);
            return this;
        }

        public BudgetTreeBuilder SetOwner(User user)
        {
            Func<IFundBuilder, IFundBuilder> originalFundOptions = ((Func<IFundBuilder, IFundBuilder>) this.rootFundOptions.Clone());
            this.rootFundOptions = fundBuilder => originalFundOptions(fundBuilder).SetOwner(user);
            return this;
        }

        public BudgetTreeBuilder AddSubBudget(Func<BudgetTreeBuilder, BudgetTreeBuilder> setupBudgetTree)
        {
            BudgetTreeBuilder subTreeBuilder = this.builderFactory.GetService<BudgetTreeBuilder>();
            subTreeBuilder = setupBudgetTree(subTreeBuilder);
            subBudgetBuilders.Add(subTreeBuilder);

            return this;
        }

        public BudgetTreeBuilder SetBudgetPeriod(BudgetPeriod budgetPeriod)
        {
            this.rootBudgetBuilder.SetBudgetPeriod(budgetPeriod);
            return this;
        }

        public BudgetTreeBuilder SetFixedAmount(decimal? setAmount)
        {
            this.rootBudgetBuilder.SetFixedAmount(setAmount);
            return this;
        }

        public BudgetTreeBuilder SetFund(Func<IFundPropertiesBuilder, IFundPropertiesBuilder> fundOptions)
        {
            Func<IFundBuilder, IFundBuilder> originalFundOptions = ((Func<IFundBuilder, IFundBuilder>) this.rootFundOptions.Clone());
            this.rootFundOptions = options => (IFundBuilder) fundOptions(
                (IFundPropertiesBuilder) originalFundOptions(options));
            return this;
        }

        public BudgetTreeBuilder SetPercentAmount(double? percentAmount)
        {
            this.rootBudgetBuilder.SetPercentAmount(percentAmount);
            return this;
        }

        /// <summary>
        /// Returns the root budget of this budget tree builder along with a list of all
        /// of that root budgets sub-budgets as the second member of the returned tuple.
        /// </summary>
        public (Budget, IEnumerable<Budget>) BuildTree()
        {
            Budget rootBudget = this.rootBudgetBuilder.SetFund(builder => this.rootFundOptions(builder)).Build();

            List<Budget> subBudgets = new List<Budget>();
            foreach (BudgetTreeBuilder subBudgetBuilder in this.subBudgetBuilders)
            {
                (Budget subBudget, IEnumerable<Budget> subBudgetsDeep) = subBudgetBuilder.SetParentBudget(rootBudget).BuildTree();
                subBudgets.Add(subBudget);
                subBudgets.AddRange(subBudgetsDeep);
            }

            return (rootBudget, subBudgets);
        }
  }
}