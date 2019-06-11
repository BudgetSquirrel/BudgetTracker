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
        Task<ApiResponse> CreateBudget(ApiRequest request);
    }
}