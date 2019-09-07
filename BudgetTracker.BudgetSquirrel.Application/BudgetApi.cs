using BudgetTracker.BudgetSquirrel.Application;
using BudgetTracker.BudgetSquirrel.Application.Messages;
using BudgetTracker.Business.Converters.BudgetConverters;
using BudgetTracker.BudgetSquirrel.Application.Messages.BudgetApi;
using BudgetTracker.Business.Auth;
using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.BudgetPeriods;
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

namespace BudgetTracker.BudgetSquirrel.Application
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
            CreateBudgetArgumentApiMessage budgetRequest = request.Arguments<CreateBudgetArgumentApiMessage>();

            if(!BudgetValidation.IsCreateBudgetRequestValid(budgetRequest.BudgetValues))
                return new ApiResponse(Constants.Budget.ApiResponseErrorCodes.INVALID_ARGUMENTS);
            try
            {
                Budget newBudgetValues = CreateBudgetApiConverter.ToModel(budgetRequest.BudgetValues);
                Budget createdBudget = await BudgetCreation.CreateBudgetForUser(newBudgetValues, user, _budgetRepository);
                BudgetResponseMessage response = GeneralBudgetApiConverter.ToGeneralResponseMessage(createdBudget);
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
            UpdateBudgetArgumentApiMessage budgetRequest = request.Arguments<UpdateBudgetArgumentApiMessage>();
            Budget budgetChanges = UpdateBudgetApiConverter.ToModel(budgetRequest.BudgetValues);

            if(!BudgetValidation.IsUpdateBudgetRequestValid(budgetRequest.BudgetValues))
                return new ApiResponse(Constants.Budget.ApiResponseErrorCodes.INVALID_ARGUMENTS);

            try
            {
                budgetChanges.SetAmount = budgetChanges.CalculateBudgetSetAmount();
                Budget updatedBudget = await _budgetRepository.UpdateBudget(budgetChanges);
                BudgetResponseMessage response = GeneralBudgetApiConverter.ToGeneralResponseMessage(updatedBudget);
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

            DeleteBudgetArgumentsApiMessage deleteArgs = request.Arguments<DeleteBudgetArgumentsApiMessage>();
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

            GetBudgetArgumentApiMessage getBudget = request.Arguments<GetBudgetArgumentApiMessage>();

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
            List<BudgetResponseMessage> rootBudgetMessages = GeneralBudgetApiConverter.ToGeneralResponseMessages(rootBudgets);
            BudgetListResponseMessage responseData = new BudgetListResponseMessage()
            {
                Budgets = rootBudgetMessages
            };

            response = new ApiResponse(responseData);
            return response;
        }

        public async Task<ApiResponse> FetchBudgetTree(ApiRequest request)
        {
            User user = await Authenticate(request);
            ApiResponse response = null;

            Guid rootBudgetId = request.Arguments<FetchBudgetTreeArgumentsApiMessage>().RootBudgetId;

            try
            {
                Budget rootBudget = await GetBudgetIfAuthorized(rootBudgetId, user.Id.Value);

                if (rootBudget != null)
                {
                    await _budgetRepository.LoadSubBudgets(rootBudget, true);
                    BudgetResponseMessage responseMessage = GeneralBudgetApiConverter.ToGeneralResponseMessage(rootBudget);
                    response = new ApiResponse(responseMessage);
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
