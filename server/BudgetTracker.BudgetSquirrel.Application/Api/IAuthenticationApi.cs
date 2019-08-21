using BudgetTracker.Business.Api.Messages;
using System.Threading.Tasks;

namespace BudgetTracker.Business.Api
{
    public interface IAuthenticationApi
    {
        /// <summary>
        /// <p>
        /// Allows a user to register a new account.
        /// </p>
        /// </summary>
        Task<ApiResponse> Register(ApiRequest request);

        /// <summary>
        /// <p>
        /// Authenticates the user, returning it in the response if authorized.
        /// </p>
        /// </summary>
        Task<ApiResponse> AuthenticateUser(ApiRequest request);

        Task<ApiResponse> DeleteUser(ApiRequest request);
    }
}
