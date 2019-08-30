using Bogus;
using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.BudgetPeriods;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
﻿using System;

namespace BudgetTracker.TestUtils.Budgeting
{
    public class BudgetBuilder : IBudgetBuilder<Budget>
    {
        private Faker _faker = new Faker();

        private Budget _budgetValueBuild;
        private BudgetDurationBase _durationBuild;

        public BudgetBuilder()
        {
            _budgetValueBuild = new Budget();
            InitRandomized();
        }

        private void InitRandomized()
        {
            _budgetValueBuild.Id = Guid.NewGuid();
            _budgetValueBuild.Name = _faker.Lorem.Word();
            _budgetValueBuild.BudgetStart = DateTime.Now;

            bool isPercentBased = _faker.Random.Bool();
            if (isPercentBased)
                _budgetValueBuild.PercentAmount = _faker.Random.Double();
            else
                _budgetValueBuild.SetAmount = _faker.Finance.Amount();
            InitRandomDuration();
        }

        private void InitRandomDuration(bool? isDaySpanDuration=null)
        {
            isDaySpanDuration = isDaySpanDuration ?? _faker.Random.Bool();

            if (isDaySpanDuration == true)
            {
                _budgetValueBuild.Duration = new MonthlyDaySpanDuration()
                {
                    NumberDays = _faker.Random.Number(29,31)
                };
            }
            else
            {
                int startDayOfMonth = _faker.Random.Number(1,10);
                int daysSpanned = _faker.Random.Number(29,31);
                _budgetValueBuild.Duration = new MonthlyBookEndedDuration()
                {
                    StartDayOfMonth = startDayOfMonth,
                    EndDayOfMonth = startDayOfMonth + daysSpanned,
                    RolloverStartDateOnSmallMonths = _faker.Random.Bool(),
                    RolloverEndDateOnSmallMonths = _faker.Random.Bool()
                };
            }
            _budgetValueBuild.Duration.Id = Guid.NewGuid();
            _durationBuild = _budgetValueBuild.Duration;
        }

        public IBudgetBuilder<Budget> SetName(string name)
        {
            _budgetValueBuild.Name = name;
            return this;
        }

        public IBudgetBuilder<Budget> SetPercentAmount(double? percentAmount)
        {
            _budgetValueBuild.PercentAmount = percentAmount;
            return this;
        }

        public IBudgetBuilder<Budget> SetFixedAmount(decimal? setAmount) {
            _budgetValueBuild.SetAmount = setAmount;
            return this;
        }

        public IBudgetBuilder<Budget> SetDurationStartDayOfMonth(int? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            ((MonthlyBookEndedDuration) _durationBuild).StartDayOfMonth = value.Value;
            return this;
        }

        public IBudgetBuilder<Budget> SetDurationEndDayOfMonth(int? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            ((MonthlyBookEndedDuration) _durationBuild).EndDayOfMonth = value.Value;
            return this;
        }

        public IBudgetBuilder<Budget> SetDurationRolloverStartDateOnSmallMonths(bool? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            ((MonthlyBookEndedDuration) _durationBuild).RolloverStartDateOnSmallMonths = value.Value;
            return this;
        }

        public IBudgetBuilder<Budget> SetDurationRolloverEndDateOnSmallMonths(bool? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            ((MonthlyBookEndedDuration) _durationBuild).RolloverEndDateOnSmallMonths = value.Value;
            return this;
        }

        public IBudgetBuilder<Budget> SetDurationNumberDays(int? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(true);
            ((MonthlyDaySpanDuration) _durationBuild).NumberDays = value.Value;
            return this;
        }

        public IBudgetBuilder<Budget> SetParentBudget(Guid? parentId, bool clearDurationValues = true) {
            _budgetValueBuild.ParentBudgetId = parentId;
            if (clearDurationValues)
            {
                _budgetValueBuild.Duration = null;
                _durationBuild = null;
            }
            return this;
        }

        public Budget Build()
        {
            return _budgetValueBuild;
        }
    }
}
