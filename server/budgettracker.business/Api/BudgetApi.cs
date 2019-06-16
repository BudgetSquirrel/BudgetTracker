using budgettracker.business.Api.Contracts.Requests;
using budgettracker.business.Api.Contracts.Responses;
using budgettracker.business.Api.Interfaces;
using budgettracker.data.Repositories.Interfaces;
using budgettracker.common.Models;

using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using budgettracker.data.Repositories;
using budgettracker.business.Api.Converters;
using budgettracker.business.Api.Contracts.BudgetApi;
using budgettracker.business.Api.Contracts.BudgetApi.DeleteBudgets;
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

            CreateBudgetArgumentApiContract budgetRequest = request.Arguments<CreateBudgetArgumentApiContract>();

            Budget newBudget = _budgetConverter.ToModel(budgetRequest.BudgetValue);

            if(!Validation.IsCreateBudgetRequestValid(budgetRequest.BudgetValue))
            {
                return new ApiResponse(Constants.Budget.ApiResponseErrorCodes.INVALID_ARGUMENTS);
            }

            try
            {
                newBudget = await _budgetRepository.CreateBudget(newBudget);
            }
            catch (RepositoryException ex)
            {
                return new ApiResponse(ex.Message);
            }

            CreateBudgetResponseContract response = _budgetConverter.ToResponseContract(newBudget);

            return new ApiResponse(response);
        }

        public async Task<ApiResponse> DeleteBudgets(ApiRequest request)
        {
            Authenticate(request);

            ApiResponse response = null;

            DeleteBudgetArgumentsApiContract deleteArgs = request.Arguments<DeleteBudgetArgumentsApiContract>();
            try
            {
                await _budgetRepository.DeleteBudgets(deleteArgs.BudgetIds);
                response = new ApiResponse();
            }
            catch (RepositoryException e)
            {
                response = new ApiResponse(e.Message);
            }

            return response;
        }

        public async Task<ApiResponse> GetRootBudgets(ApiRequest request)
        {
            User user = Authenticate(request);
            ApiResponse response;

            List<Budget> rootBudgets = await _budgetRepository.GetRootBudgets(user.Id);
            List<BudgetResponseContract> rootBudgetContracts = BudgetApiConverter.ToGeneralResponseContracts(rootBudgets);

            response = new ApiResponse(rootBudgetContracts);
            return response;
        }
    }
}
