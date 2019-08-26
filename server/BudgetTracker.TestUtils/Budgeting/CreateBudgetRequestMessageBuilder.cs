using Bogus;
using BudgetTracker.Business.Budgeting;
using Newtonsoft.Json;
﻿using System;

namespace BudgetTracker.TestUtils.Budgeting
{
    public class CreateBudgetRequestMessageBuilder : IBudgetBuilder<CreateBudgetRequestMessage>
    {
        private Faker _faker = new Faker();

        private CreateBudgetRequestMessage _budgetValueBuild;
        private JObject _durationBuild;

        public CreateBudgetRequestMessageBuilder()
        {
            _budgetValueBuild = new CreateBudgetRequestMessage();
            InitRandomized();
        }

        private void InitRandomized()
        {
            _budgetValueBuild.Name = _faker.Lorem.Word();

            bool isPercentBased = _faker.Random.Bool();
            if (isPercentBased)
                _budgetValueBuild.PercentAmount = _faker.Random.Double();
            else
                _budgetValueBuild.SetAmount = _faker.Finance.Amount();
            InitRandomDuration();
        }

        private void InitRandomDuration(bool? isDaySpanDuration=null)
        {
            _budgetValueBuild.DurationTemp = new JObject();
            _durationBuild = _budgetValueBuild.DurationTemp;

            isDaySpanDuration = isDaySpanDuration ?? _faker.Random.Bool();

            if (isDaySpanDuration)
            {
                _durationBuild["NumberDays"] = _faker.Random.Number(29,31);
            }
            else
            {
                _durationBuild["StartDayOfMonth"] = _faker.Random.Number(1,10);
                int daysSpanned = _faker.Random.Number(29,31);
                _durationBuild["EndDayOfMonth"] = _durationBuild["StartDayOfMonth"] + daysSpanned;
                _durationBuild["RolloverStartDateOnSmallMonths"] = _faker.Random.Bool();
                _durationBuild["RolloverEndDateOnSmallMonths"] = _faker.Random.Bool();
            }
        }

        public IBudgetBuilder SetName(string name) => _budgetValueBuild.Name = name;
        public IBudgetBuilder SetPercentAmount(double? percentAmount) => _budgetValueBuild.PercentAmount = percentAmount;
        public IBudgetBuilder SetFixedAmount(decimal? setAmount) => _budgetValueBuild.SetAmount = setAmount;

        public IBudgetBuilder SetDurationStartDayOfMonth(int? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            _durationBuild["StartDayOfMonth"] = value;
        }

        public IBudgetBuilder SetDurationEndDayOfMonth(int? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            _durationBuild["EndDayOfMonth"] = value;
        }

        public IBudgetBuilder SetDurationRolloverStartDateOnSmallMonths(bool? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            _durationBuild["RolloverStartDateOnSmallMonths"] = value;
        }

        public IBudgetBuilder SetDurationRolloverEndDateOnSmallMonths(bool? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            _durationBuild["RolloverEndDateOnSmallMonths"] = value;
        }

        public IBudgetBuilder SetDurationNumberDays(int? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(true);
            _durationBuild["NumberDays"] = value;
        }

        public IBudgetBuilder SetParentBudget(Guid? parentId, bool clearDurationValues = true) {
            _budgetValueBuild.ParentBudgetId = parentId;
            if (clearDurationValues)
            {
                _budgetValueBuild.DurationTemp = null;
                _durationBuild = null;
            }
        }

        public CreateBudgetRequestMessage Build()
        {
            return _budgetValueBuild;
        }
    }
}
