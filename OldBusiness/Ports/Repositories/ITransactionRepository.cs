using BudgetTracker.Business.Transactions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetTracker.Business.Ports.Repositories
{
    public interface ITransactionRepository
    {
        /// <summary>
        /// <p>
        /// Saves the new transaction to the database and return the newly created transaction
        /// model. This will throw exceptions if something fails.
        /// </p>
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/></param>
        Task<Transaction> CreateTransaction(Transaction transaction);

        /// <summary>
        /// <p>
        /// Fetches transactions from the database with the given arguments. If
        /// toDate is null, then it will default to DateTime.Now. If fromDate is
        /// null, it will default to 365 days in the past.
        /// </p>
        /// </summary>
        Task<IEnumerable<Transaction>> FetchTransactions(Guid budgetId, DateTime? fromDate=null, DateTime? toDate=null);
    }
}
