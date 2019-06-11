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
using budgettracker.business.Api.Converters.BudgetConverters;
using budgettracker.business.Api.Contracts.BudgetApi.CreateBudget;
using budgettracker.data.Exceptions;
using budgettracker.common;
using budgettracker.business.Api.Contracts.BudgetApi.UpdateBudget;
using budgettracker.business.Api.Converters;

namespace budgettracker.business.Api
{
    public class BudgetApi :  ApiBase<User>, IBudgetApi
    {

        private readonly IBudgetRepository _budgetRepository;

        private readonly CreateBudgetApiConverter _createBudgetConverter;
        private readonly UpdateBudgetApiConverter _updateBudgetConverter;

        public BudgetApi(IBudgetRepository budgetRepository, IConfiguration appConfig, UserRepository userRepository)
            : base(userRepository, new Rfc2898Encryptor(),
                    ConfigurationReader.FromAppConfiguration(appConfig))
        {
            _budgetRepository = budgetRepository;
            _createBudgetConverter = new CreateBudgetApiConverter();
            _updateBudgetConverter = new UpdateBudgetApiConverter();
        }

        public async Task<ApiResponse> CreateBudget(ApiRequest request)
        {
            Authenticate(request);           

            CreateBudgetArgumentApiContract budgetRequest = request.Arguments<CreateBudgetArgumentApiContract>();

            Budget newBudget = _createBudgetConverter.ToModel(budgetRequest.BudgetValue);

            if(!Validation.IsCreateBudgetRequestValid(budgetRequest.BudgetValue))
            {
                return new ApiResponse(Constants.Budget.ApiResponseErrorCodes.INVALID_ARGUMENTS);
            }

            newBudget.BudgetStart = DateTime.Now;

            try
            {
                newBudget = await _budgetRepository.CreateBudget(newBudget);
            }
            catch (RepositoryException ex)
            {
                return new ApiResponse(ex.Message);
            }

            CreateBudgetResponseContract response = _createBudgetConverter.ToResponseContract(newBudget);

            return new ApiResponse(response);
        }

        public async Task<ApiResponse> UpdateBudget(ApiRequest request)
        {
            Authenticate(request);           

            UpdateBudgetArgumentApiContract budgetRequest = request.Arguments<UpdateBudgetArgumentApiContract>();

            Budget newBudget = _updateBudgetConverter.ToModel(budgetRequest.BudgetValue);

            if(!Validation.IsUpdateBudgetRequestValid(budgetRequest.BudgetValue))
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

            CreateBudgetResponseContract response = _createBudgetConverter.ToResponseContract(newBudget);

            return new ApiResponse(response);
        }
    }
}