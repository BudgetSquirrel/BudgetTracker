using BudgetTracker.Business.Api.Contracts.Requests;
using BudgetTracker.Business.Api.Contracts.Responses;
using BudgetTracker.Business.Api.Interfaces;
using BudgetTracker.Data.Repositories.Interfaces;
using BudgetTracker.Common.Models;
using BudgetTracker.Data.Repositories;
using BudgetTracker.Business.Api.Converters.BudgetConverters;
using BudgetTracker.Business.Api.Contracts.BudgetApi;
using BudgetTracker.Business.Api.Contracts.BudgetApi.BudgetTree;
using BudgetTracker.Business.Api.Contracts.BudgetApi.CreateBudget;
using BudgetTracker.Business.Api.Contracts.BudgetApi.DeleteBudgets;
using BudgetTracker.Business.Api.Contracts.BudgetApi.GetBudget;
using BudgetTracker.Business.Api.Contracts.BudgetApi.UpdateBudget;
using BudgetTracker.Data.Exceptions;
using BudgetTracker.Common;

using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetTracker.Business.Api
{
    public class BudgetApi :  ApiBase<User>, IBudgetApi
    {

        private readonly IBudgetRepository _budgetRepository;

        public BudgetApi(IBudgetRepository budgetRepository, IConfiguration appConfig, UserRepository userRepository)
            : base(userRepository, new Rfc2898Encryptor(),
                    ConfigurationReader.FromAppConfiguration(appConfig))
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<ApiResponse> CreateBudget(ApiRequest request)
        {
            User user = await Authenticate(request);

            CreateBudgetArgumentApiContract budgetRequest = request.Arguments<CreateBudgetArgumentApiContract>();

            if(!Validation.IsCreateBudgetRequestValid(budgetRequest.BudgetValues))
            {
                return new ApiResponse(Constants.Budget.ApiResponseErrorCodes.INVALID_ARGUMENTS);
            }

            Budget newBudget = CreateBudgetApiConverter.ToModel(budgetRequest.BudgetValues);
            newBudget.Owner = user;

            if(newBudget.ParentBudgetId != null)
            {
                newBudget.ParentBudget = await _budgetRepository.GetBudget(newBudget.ParentBudgetId.Value);
                newBudget.Duration = newBudget.ParentBudget.Duration;
            }

            try
            {
                newBudget = await _budgetRepository.CreateBudget(newBudget);
            }
            catch (RepositoryException ex)
            {
                return new ApiResponse(ex.Message);
            }

            CreateBudgetResponseContract response = CreateBudgetApiConverter.ToResponseContract(newBudget);

            return new ApiResponse(response);
        }

        public async Task<ApiResponse> UpdateBudget(ApiRequest request)
        {
            await Authenticate(request);

            UpdateBudgetArgumentApiContract budgetRequest = request.Arguments<UpdateBudgetArgumentApiContract>();

            Budget newBudget = UpdateBudgetApiConverter.ToModel(budgetRequest.BudgetValues);

            if(!Validation.IsUpdateBudgetRequestValid(budgetRequest.BudgetValues))
            {
                return new ApiResponse(Constants.Budget.ApiResponseErrorCodes.INVALID_ARGUMENTS);
            }

            try
            {
                newBudget = await _budgetRepository.UpdateBudget(newBudget);
            }
            catch (RepositoryException ex)
            {
                return new ApiResponse(ex.Message);
            }

            UpdateBudgetResponseContract response = UpdateBudgetApiConverter.ToResponseContract(newBudget);

            return new ApiResponse(response);
        }

        public async Task<ApiResponse> DeleteBudgets(ApiRequest request)
        {
            await Authenticate(request);

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

        public async Task<ApiResponse> GetBudget(ApiRequest request)
        {
            User user = await Authenticate(request);

            GetBudgetArgumentApiContract getBudget = request.Arguments<GetBudgetArgumentApiContract>();

            ApiResponse response = new ApiResponse();

            try
            {
                Budget retrievedBudget = await GetBudgetIfAuthorized(getBudget.BudgetValues.Id, user.Id.Value);

                if (retrievedBudget != null)
                {
                    response.Response = UpdateBudgetApiConverter.ToResponseContract(retrievedBudget);
                }
                else
                {
                    response.Error = "Could not find the requested budget for this user";
                }
            }
            catch (RepositoryException ex)
            {
                response.Error = ex.Message;
            }

            return response;
        }

        public async Task<ApiResponse> GetRootBudgets(ApiRequest request)
        {
            User user = await Authenticate(request);
            ApiResponse response;

            List<Budget> rootBudgets = await _budgetRepository.GetRootBudgets(user.Id.Value);
            List<BudgetResponseContract> rootBudgetContracts = GeneralBudgetApiConverter.ToGeneralResponseContracts(rootBudgets);
            BudgetListResponseContract responseData = new BudgetListResponseContract()
            {
                Budgets = rootBudgetContracts
            };

            response = new ApiResponse(responseData);
            return response;
        }

        public async Task<ApiResponse> FetchBudgetTree(ApiRequest request)
        {
            User user = await Authenticate(request);
            ApiResponse response = null;

            Guid rootBudgetId = request.Arguments<FetchBudgetTreeArgumentsApiContract>().RootBudgetId;

            try
            {
                Budget rootBudget = await GetBudgetIfAuthorized(rootBudgetId, user.Id.Value);

                if (rootBudget != null)
                {
                    await _budgetRepository.LoadSubBudgets(rootBudget, true);
                    BudgetResponseContract responseContract = GeneralBudgetApiConverter.ToGeneralResponseContract(rootBudget);
                    response = new ApiResponse(responseContract);
                }
                else
                {
                    response = new ApiResponse("Could not find the requested budget for this user");
                }
            }
            catch (RepositoryException ex)
            {
                response = new ApiResponse(ex.Message);
            }

            return response;
        }

        private async Task<Budget> GetBudgetIfAuthorized(Guid budgetId, Guid userId)
        {
            Budget retrievedBudget = await _budgetRepository.GetBudget(budgetId);

            if (retrievedBudget.Owner.Id != userId)
            {
                return null;
            }

            return retrievedBudget;
        }
    }
}
