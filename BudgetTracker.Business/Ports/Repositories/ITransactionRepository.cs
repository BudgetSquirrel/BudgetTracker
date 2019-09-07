using BudgetTracker.Business.Transaction;
using System;
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
    }
}
