using budgettracker.business.Api.Contracts.Responses;
using budgettracker.business.Api.Contracts.Requests;

using System.Threading.Tasks;

namespace budgettracker.business.Api.Interfaces
{
    public interface IBudgetApi
    {
        /// <summary>
        /// <p>
        /// Creates a new budget in the database. Will return the givne budget
        /// if created sucessfully otherwise will throw an exception.
        /// </p>
        /// </summary>
        /// <param name="request"> <see cref="ApiRequest"/> </param>
        /// <returns> Returns: <see cref="ApiResponse" /> </returns>
        Task<ApiResponse> CreateBudget(ApiRequest request);

        /// <summary>
        /// <p>
        /// Will update the the given budget to the required values and returns the updated budget or
        /// an error message if an exception is thrown.
        /// </p>
        /// </summary>
        /// <param name="request"> <see cref="ApiRequest"/> </param>
        /// <returns> Returns: <see cref="ApiResponse" /> </returns>
        Task<ApiResponse> UpdateBudget(ApiRequest request);

        /// <summary>
        /// <p>
        /// Deletes all Budgets that match the given ids. All ids that do not
        /// match a Budget record or couldn't be deleted will be returned in an
        /// error message. All budgets that can be deleted will be deleted
        /// before the error message is returned.
        /// </p>
        /// </summary>
        Task<ApiResponse> DeleteBudgets(ApiRequest request);

        /// <summary>
        /// <p>
        /// Will retrieve the a single budget by it's id.
        /// </p>
        /// </summary>
        Task<ApiResponse> GetBudget(ApiRequest request);
    }
}
