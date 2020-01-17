using System.Threading.Tasks;
using BudgetTracker.Business.BudgetPeriods;
using BudgetTracker.Business.Ports.Repositories;
using BudgetTracker.Common.Exceptions;

namespace BudgetTracker.Business.Budgeting
{
    public class BudgetUpdater
    {
        IBudgetRepository _budgetRepository;
        BudgetValidator _budgetValidator;

        public BudgetUpdater(IBudgetRepository budgetRepository, BudgetValidator budgetValidator)
        {
            _budgetRepository = budgetRepository;
            _budgetValidator = budgetValidator;
        }
        
        public async Task<Budget> UpdateBudget(UpdateBudgetRequestMessage budgetValues)
        {
            _budgetValidator.ValidateUpdateBudgetRequest(budgetValues);

            Budget budgetChanges = GetNewBudgetValuesFromInput(budgetValues);

            budgetChanges.SetAmount = budgetChanges.CalculateBudgetSetAmount();
            Budget updatedBudget = await _budgetRepository.UpdateBudget(budgetChanges);

            return updatedBudget;
        }

        private Budget GetNewBudgetValuesFromInput(UpdateBudgetRequestMessage requestMessage)
        {
            return new Budget()
            {
                Id = requestMessage.Id,
                Name = requestMessage.Name,
                PercentAmount = requestMessage.PercentAmount,
                SetAmount = requestMessage.SetAmount,
                Duration = GetBudgetDurationFromInput(requestMessage.Duration),
                ParentBudgetId = requestMessage.ParentBudgetId,
                BudgetStart = requestMessage.BudgetStart
            };
        }

        private BudgetDurationBase GetBudgetDurationFromInput(BudgetDurationBaseMessage durationMessage)
        {
            if (durationMessage == null)
            {
                return null;
            }
            BudgetDurationBase durationModel = null;
            if (durationMessage is MonthlyBookEndedDurationMessage)
            {
                MonthlyBookEndedDurationMessage bookEndDurationMessage = (MonthlyBookEndedDurationMessage) durationMessage;
                durationModel = new MonthlyBookEndedDuration()
                {
                    Id = bookEndDurationMessage.Id,
                    StartDayOfMonth = bookEndDurationMessage.StartDayOfMonth,
                    EndDayOfMonth = bookEndDurationMessage.EndDayOfMonth,
                    RolloverStartDateOnSmallMonths = bookEndDurationMessage.RolloverStartDateOnSmallMonths,
                    RolloverEndDateOnSmallMonths = bookEndDurationMessage.RolloverEndDateOnSmallMonths,
                };
            }
            else if (durationMessage is MonthlyDaySpanDurationMessage)
            {
                MonthlyDaySpanDurationMessage daySpanDurationMessage = (MonthlyDaySpanDurationMessage) durationMessage;
                durationModel = new MonthlyDaySpanDuration()
                {
                    Id = daySpanDurationMessage.Id,
                    NumberDays = daySpanDurationMessage.NumberDays
                };
            }
            else
            {
                throw new ConversionException(durationMessage.GetType(), typeof(BudgetDurationBase), $"Duration class '{durationMessage.GetType().ToString()}' not supported class.");
            }
            return durationModel;
        }
    }
}