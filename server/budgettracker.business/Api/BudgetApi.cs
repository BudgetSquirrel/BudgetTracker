using budgettracker.business.Api.Contracts.Requests;
using budgettracker.business.Api.Contracts.Responses;
using budgettracker.business.Api.Interfaces;
using budgettracker.data.Repositories.Interfaces;
using budgettracker.common.Models;

using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;

using Microsoft.Extensions.Configuration;

using System;
using System.Threading.Tasks;

namespace budgettracker.business.Api
{
    public class BudgetApi : IBudgetApi, ApiBase<User>
    {

        private readonly IBudgetRepository _budgetRepository;

        public BudgetApi(IBudgetRepository budgetRepository, IConfiguration appConfig)
            : base(budgetRepository, new Rfc2898Encryptor(),
                    ConfigurationReader.FromAppConfiguration(appConfig))
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