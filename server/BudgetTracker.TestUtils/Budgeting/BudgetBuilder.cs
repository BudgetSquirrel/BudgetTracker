using Bogus;
using BudgetTracker.Business.Budgeting;
using Newtonsoft.Json;
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
            _budgetValueBuild.Id = new Guid();
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

            if (isDaySpanDuration)
            {
                _budgetValueBuild.Duration = new MonthlyDaySpanDuration()
                {
                    NumberDays = _faker.Random.Number(29,31)
                };
            }
            else
            {
                int daysSpanned = _faker.Random.Number(29,31);
                _budgetValueBuild.Duration = new MonthlyBookendedDuration()
                {
                    StartDayOfMonth = _faker.Random.Number(1,10),
                    EndDayOfMonth = _durationBuild["StartDayOfMonth"] + daysSpanned,
                    RolloverStartDateOnSmallMonths = _faker.Random.Bool(),
                    RolloverEndDateOnSmallMonths = _faker.Random.Bool()
                };
            }
            _durationBuild.Id = new Guid();
            _durationBuild = _budgetValueBuild.Duration;
        }

        public IBudgetBuilder<Budget> SetName(string name) => _budgetValueBuild.Name = name;
        public IBudgetBuilder<Budget> SetPercentAmount(double? percentAmount) => _budgetValueBuild.PercentAmount = percentAmount;
        public IBudgetBuilder<Budget> SetFixedAmount(decimal? setAmount) => _budgetValueBuild.SetAmount = setAmount;

        public IBudgetBuilder<Budget> SetDurationStartDayOfMonth(int? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            _durationBuild.StartDayOfMonth = value;
        }

        public IBudgetBuilder<Budget> SetDurationEndDayOfMonth(int? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            _durationBuild.EndDayOfMonth = value;
        }

        public IBudgetBuilder<Budget> SetDurationRolloverStartDateOnSmallMonths(bool? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            _durationBuild.RolloverStartDateOnSmallMonths = value;
        }

        public IBudgetBuilder<Budget> SetDurationRolloverEndDateOnSmallMonths(bool? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            _durationBuild.RolloverEndDateOnSmallMonths = value;
        }

        public IBudgetBuilder<Budget> SetDurationNumberDays(int? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(true);
            _durationBuild.NumberDays = value;
        }

        public IBudgetBuilder<Budget> SetParentBudget(Guid? parentId, bool clearDurationValues = true) {
            _budgetValueBuild.ParentBudgetId = parentId;
            if (clearDurationValues)
            {
                _budgetValueBuild.DurationTemp = null;
                _durationBuild = null;
            }
        }

        public Budget Build()
        {
            return _budgetValueBuild;
        }
    }
}
