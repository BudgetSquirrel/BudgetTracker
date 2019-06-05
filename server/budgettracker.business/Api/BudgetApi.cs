using budgettracker.business.Api.Contracts.Requests;
using budgettracker.business.Api.Contracts.Responses;
using budgettracker.business.Api.Interfaces;
using budgettracker.data.Repositories.Interfaces;
using budgettracker.common.Models;

using System;
using System.Threading.Tasks;

namespace budgettracker.business.Api
{
    public class BudgetApi : IBudgetApi
    {

        private readonly IBudgetRepository _budgetRepository;

        public BudgetApi(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<ApiResponse> CreateBudget(ApiRequest request)
        {
            // Authenticate the User here

            // Convert request buget to Common Budget model
            await _budgetRepository.CreateBudget(new Budget());
            
            // Return something else
            return new ApiResponse("Not Implemented");
        }
    }
}