using BudgetTracker.Business.BudgetPeriods;
using BudgetTracker.Business.Transactions;
using BudgetTracker.Business.Ports.Repositories;
using BudgetTracker.Business.Auth;
using BudgetTracker.Common.Exceptions;
using Newtonsoft.Json;
﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetTracker.Business.Budgeting
{
    public class Budget
    {
        /// <summary>
        /// Unique numeric identifier for this <see cref="Budget" />.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// English, user friendly identifier for this <see cref="Budget" />.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Allows the user to calculate the Set amount based on the Parent
        /// budgets Set amount. This will be used on creation time and update
        /// time to calculate the new value of this budgets SetAmount based on
        /// it's parent Budget's SetAmount.
        /// </summary>
        public double? PercentAmount { get; set; }

        /// <summary>
        /// The amount of money that is available in this budget. If this is
        /// null, then it is assumed that this has sub-budgets. This budget then
        /// can be assumed to have a calculated balance of the sum of all of
        /// it's direct sub-balances (which may also have calculated balances).
        /// </summary>
        public decimal? SetAmount { get; set; }

        /// <summary>
        /// The amount that is currently available in the fund attached to this
        /// budget for the budget planner to spend. Each time a transaction is
        /// applied to this budget, this balance will be modified to reflect that
        /// transaction.
        /// It is important to note that this number is agnostic of the budget
        /// amounts (PercentAmount/SetAmount). Those are planned amount that the
        /// user will put into the budget fund every budget period. This is the
        /// amount that stores that planned amount along with any rollover from
        /// previous months.
        /// For example, a user may put $50 into this budget every budget period,
        /// but after 3 budget periods, if they don't spend anything, this fund
        /// balance will have a value of $150 (3 budget periods worth of saving
        /// $50 each period). If they then go and log a transaction against this
        /// budget of $37, this fund balance will then only be $113 (150 - 137).
        /// </summary>
        public decimal FundBalance { get; set; }

        /// <summary>
        /// The duration the budget will be per cycle in months.
        /// </summary>
        public BudgetDurationBase Duration { get; set; }

        /// <summary>
        /// The last start date of the budget's cycle need to determine when the
        /// current budget will end and the next one will be begin
        /// </summary>
        public DateTime BudgetStart { get; set; }

        /// <summary>
        /// The parent budget's id, if null then this budget is considered the root budget
        /// </summary>
        public Guid? ParentBudgetId { get; set; }

        /// <summary>
        /// The parent budget of which this is a sub-budget.
        /// </summary>
        [JsonIgnore]
        public Budget ParentBudget { get; set; }

        /// <summary>
        /// The user that owns this budget.
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// Budgets that are children of this budget.
        /// </summary>
        public List<Budget> SubBudgets { get; set; }

        public bool IsPercentBasedBudget
        {
            get
            {
                return this.PercentAmount != null;
            }
        }

        public bool IsRootBudget
        {
            get
            {
                return this.ParentBudgetId == null;
            }
        }

        public bool IsParentBudgetLoaded
        {
            get
            {
                return !IsRootBudget && ParentBudget != null;
            }
        }

        /// <summary>
        /// Determines if the user owns this budget.
        /// </summary>
        public bool IsOwnedBy(User user)
        {
            return user.Id == Owner.Id;
        }

        public decimal CalculateBudgetSetAmount()
        {
            if (!IsRootBudget && !IsParentBudgetLoaded)
            {
                throw new Exception("Parent budget must be load for non-root budget.");
            }

            decimal newBudgetAmount = default(decimal);
            if (IsPercentBasedBudget)
            {
                newBudgetAmount = ParentBudget.SetAmount.Value * (decimal) PercentAmount.Value;
            }
            else
            {
                newBudgetAmount = SetAmount.Value;
            }
            return newBudgetAmount;
        }

        /// <summary>
        /// <p>
        /// Apply the given transaction to this budget. If this
        /// has a parent budget, this needs to be applied to the
        /// parent budget as well. Obviously, this is recursive.
        /// </p>
        /// <p>
        /// Applying the transaction means modifying the FundBalance
        /// on this budget by the amount specified in the transaction.
        /// </p>
        /// <p>
        /// If budgetRepository is not null, this will save the changes to the
        /// given budgetRepository and the changes in the repository will be
        /// applied to this object. Otherwise, if budgetRepository is null, then
        /// this will not try to save the changes to the repository.
        /// </p>
        /// </summary>
        public async Task ApplyTransaction(Transaction transaction, IBudgetRepository budgetRepository=null)
        {
            if (!IsOwnedBy(transaction.Owner))
            {
                throw new ValidationException("This transaction does not belong to the owner of this budget.");
            }
            FundBalance += transaction.Amount;
            if (!IsRootBudget)
            {
                await LoadParentBudget(budgetRepository);
                await ParentBudget.ApplyTransaction(transaction, budgetRepository);
            }
            if (budgetRepository != null)
            {
                Budget updatedBudget = await budgetRepository.UpdateBudget(this);
                Mirror(updatedBudget);
            }
        }

        /// <summary>
        /// <p>
        /// Gets the total amount from the transactions. If the result is positive,
        /// then that means the user earned more money in these transactions
        /// than the spent. The oposite is true if the result is negative.
        /// </p>
        /// </summary>
        public decimal CalculateTransactionsTotalNetValue(List<Transaction> transactions)
        {
            return transactions.Sum(t => t.Amount);
        }

        public async Task LoadParentBudget(IBudgetRepository budgetRepository)
        {
            if (!IsRootBudget && !IsParentBudgetLoaded)
            {
                ParentBudget = await budgetRepository.GetBudget(ParentBudgetId.Value);
            }
        }

        public async Task<Budget> GetRootBudget(IBudgetRepository budgetRepository)
        {
            if (IsRootBudget)
            {
                _rootBudget = this;
            }
            else
            {
                List<Budget> ownersRootBudgets = await budgetRepository.GetRootBudgets(Owner.Id.Value);
                _rootBudget = ownersRootBudgets.Single(b => b.Duration.Id == Duration.Id);
            }
            return _rootBudget;
        }
        private Budget _rootBudget;

        /// <summary>
        /// <p>
        /// Fetches all transactions that have been logged under this budget
        /// or one of it's sub budgets. This is recursive. This budgets sub
        /// budgets must be loaded recursively before calling this method.
        /// </p>
        /// </summary>
        public async Task<IEnumerable<Transaction>> GetTransactions(DateTime? fromDate, DateTime? toDate, ITransactionRepository transactionRepository)
        {
            List<Transaction> transactions = (await transactionRepository.FetchTransactions(this.Id, fromDate, toDate)).ToList();
            transactions = transactions.Select(t =>
            {
                t.Budget = this;
                return t;
            }).ToList();
            foreach (Budget subBudget in SubBudgets)
            {
                List<Transaction> transactionsForSubBudget = (await subBudget.GetTransactions(fromDate,
                                                                                toDate,
                                                                                transactionRepository)).ToList();
                transactions.AddRange(transactionsForSubBudget);
            }
            return transactions;
        }

        /// <summary>
        /// Takes all attributes of otherBudget and mirrors them
        /// in this budget. This is sort of like clone except it
        /// is cloned to an already instantiated budget object.
        /// </summary>
        public void Mirror(Budget otherBudget)
        {
            Id = otherBudget.Id;
            Name = otherBudget.Name;
            PercentAmount = otherBudget.PercentAmount;
            SetAmount = otherBudget.SetAmount;
            FundBalance = otherBudget.FundBalance;
            Duration = otherBudget.Duration;
            BudgetStart = otherBudget.BudgetStart;
            ParentBudgetId = otherBudget.ParentBudgetId;
            ParentBudget = otherBudget.ParentBudget;
            Owner = otherBudget.Owner;
            SubBudgets = otherBudget.SubBudgets;
        }

        public Budget Clone()
        {
            Budget clone = new Budget();
            clone.Mirror(this);
            return clone;
        }
    }
}
