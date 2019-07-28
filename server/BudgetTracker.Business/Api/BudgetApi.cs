using BudgetTracker.Business.Api.Contracts.Requests;
using BudgetTracker.Business.Api.Contracts.Responses;
using BudgetTracker.Business.Api.Interfaces;
using BudgetTracker.Business.Api.Converters.BudgetConverters;
using BudgetTracker.Business.Api.Contracts.BudgetApi;
using BudgetTracker.Business.Api.Contracts.BudgetApi.BudgetTree;
using BudgetTracker.Business.Api.Contracts.BudgetApi.CreateBudget;
using BudgetTracker.Business.Api.Contracts.BudgetApi.DeleteBudgets;
using BudgetTracker.Business.Api.Contracts.BudgetApi.GetBudget;
using BudgetTracker.Business.Api.Contracts.BudgetApi.UpdateBudget;
using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.Budgeting.Tracking.Periods;
using BudgetTracker.Common.Models;
using BudgetTracker.Business.Ports.Exceptions;
using BudgetTracker.Business.Ports.Repositories;
using BudgetTracker.Common;

using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;
using GateKeeper.Repositories;

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

        public BudgetApi(IBudgetRepository budgetRepository, IConfiguration appConfig, IGateKeeperUserRepository<User> userRepository)
            : base(userRepository, new Rfc2898Encryptor(),
                    ConfigurationReader.FromAppConfiguration(appConfig))
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<ApiResponse> CreateBudget(ApiRequest request)
        {
            User user = await Authenticate(request);
            CreateBudgetArgumentApiContract budgetRequest = request.Arguments<CreateBudgetArgumentApiContract>();

            if(!BudgetValidation.IsCreateBudgetRequestValid(budgetRequest.BudgetValues))
                return new ApiResponse(Constants.Budget.ApiResponseErrorCodes.INVALID_ARGUMENTS);
            try
            {
                Budget newBudgetValues = CreateBudgetApiConverter.ToModel(budgetRequest.BudgetValues);
                Budget createdBudget = await BudgetCreation.CreateBudgetForUser(newBudgetValues, user, _budgetRepository);
                CreateBudgetResponseMessage response = CreateBudgetApiConverter.ToResponseContract(createdBudget);
                return new ApiResponse(response);
            }
            catch (RepositoryException ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateBudget(ApiRequest request)
        {
            await Authenticate(request);
            UpdateBudgetArgumentApiContract budgetRequest = request.Arguments<UpdateBudgetArgumentApiContract>();
            Budget budgetChanges = UpdateBudgetApiConverter.ToModel(budgetRequest.BudgetValues);

            if(!BudgetValidation.IsUpdateBudgetRequestValid(budgetRequest.BudgetValues))
                return new ApiResponse(Constants.Budget.ApiResponseErrorCodes.INVALID_ARGUMENTS);

            try
            {
                budgetChanges.SetAmount = budgetChanges.CalculateBudgetSetAmount();
                Budget updatedBudget = await _budgetRepository.UpdateBudget(budgetChanges);
                BudgetResponseContract response = GeneralBudgetApiConverter.ToGeneralResponseMessage(updatedBudget);
                return new ApiResponse(response);
            }
            catch (RepositoryException ex)
            {
                return new ApiResponse(ex.Message);
            }
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
                    response.Response = GeneralBudgetApiConverter.ToGeneralResponseMessage(retrievedBudget);
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
            List<BudgetResponseContract> rootBudgetContracts = GeneralBudgetApiConverter.ToGeneralResponseMessages(rootBudgets);
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
                    BudgetResponseContract responseContract = GeneralBudgetApiConverter.ToGeneralResponseMessage(rootBudget);
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
