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
using budgettracker.data.Repositories;
using budgettracker.business.Api.Converters;
using budgettracker.business.Api.Contracts.BudgetApi;
using budgettracker.data.Exceptions;
using budgettracker.common;

namespace budgettracker.business.Api
{
    public class BudgetApi :  ApiBase<User>, IBudgetApi
    {

        private readonly IBudgetRepository _budgetRepository;

        private readonly BudgetApiConverter _budgetConverter;

        public BudgetApi(IBudgetRepository budgetRepository, IConfiguration appConfig, UserRepository userRepository)
            : base(userRepository, new Rfc2898Encryptor(),
                    ConfigurationReader.FromAppConfiguration(appConfig))
        {
            _budgetRepository = budgetRepository;
            _budgetConverter = new BudgetApiConverter();
        }

        public async Task<ApiResponse> CreateBudget(ApiRequest request)
        {
            Authenticate(request);           

            CreateBudgetRequestContract budgetRequest = request.Arguments<CreateBudgetRequestContract>();

            Budget newBudget = _budgetConverter.ToModel(budgetRequest);

            if(!Validation.IsCreateBudgetRequestValid(budgetRequest))
            {
                return new ApiResponse(Constants.Budget.ApiResponseErrorCodes.INVALID_ARGUMENTS);
            }

            newBudget.Id = Guid.NewGuid();
            newBudget.BudgetStart = new DateTime();

            try
            {
                await _budgetRepository.CreateBudget(newBudget);
            }
            catch (RepositoryException ex)
            {
                return new ApiResponse(ex.Message);
            }

            CreateBudgetResponseContract response = _budgetConverter.ToResponseContract(newBudget);

            return new ApiResponse(response);
        }
    }
}