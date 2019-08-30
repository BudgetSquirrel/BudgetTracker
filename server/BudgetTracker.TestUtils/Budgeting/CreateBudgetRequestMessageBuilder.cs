using Bogus;
using BudgetTracker.Business.Budgeting;
using BudgetTracker.Business.BudgetPeriods;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            if (isDaySpanDuration == true)
            {
                _durationBuild["NumberDays"] = _faker.Random.Number(29,31);
            }
            else
            {
                int startDayOfMonth = _faker.Random.Number(1,10);
                int daysSpanned = _faker.Random.Number(29,31);
                _durationBuild["StartDayOfMonth"] = startDayOfMonth;
                _durationBuild["EndDayOfMonth"] = startDayOfMonth + daysSpanned;
                _durationBuild["RolloverStartDateOnSmallMonths"] = _faker.Random.Bool();
                _durationBuild["RolloverEndDateOnSmallMonths"] = _faker.Random.Bool();
            }
        }

        public IBudgetBuilder<CreateBudgetRequestMessage> SetName(string name){
            _budgetValueBuild.Name = name;
            return this;
        }

        public IBudgetBuilder<CreateBudgetRequestMessage> SetPercentAmount(double? percentAmount)
        {
            _budgetValueBuild.PercentAmount = percentAmount;
            return this;
        }

        public IBudgetBuilder<CreateBudgetRequestMessage> SetFixedAmount(decimal? setAmount)
        {
            _budgetValueBuild.SetAmount = setAmount;
            return this;
        }

        public IBudgetBuilder<CreateBudgetRequestMessage> SetDurationStartDayOfMonth(int? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            _durationBuild["StartDayOfMonth"] = value;
            return this;
        }

        public IBudgetBuilder<CreateBudgetRequestMessage> SetDurationEndDayOfMonth(int? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            _durationBuild["EndDayOfMonth"] = value;
            return this;
        }

        public IBudgetBuilder<CreateBudgetRequestMessage> SetDurationRolloverStartDateOnSmallMonths(bool? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            _durationBuild["RolloverStartDateOnSmallMonths"] = value;
            return this;
        }

        public IBudgetBuilder<CreateBudgetRequestMessage> SetDurationRolloverEndDateOnSmallMonths(bool? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(false);
            _durationBuild["RolloverEndDateOnSmallMonths"] = value;
            return this;
        }

        public IBudgetBuilder<CreateBudgetRequestMessage> SetDurationNumberDays(int? value)
        {
            if (_durationBuild == null)
                InitRandomDuration(true);
            _durationBuild["NumberDays"] = value;
            return this;
        }

        public IBudgetBuilder<CreateBudgetRequestMessage> SetParentBudget(Guid? parentId, bool clearDurationValues = true) {
            _budgetValueBuild.ParentBudgetId = parentId;
            if (clearDurationValues)
            {
                _budgetValueBuild.DurationTemp = null;
                _durationBuild = null;
            }
            return this;
        }

        public CreateBudgetRequestMessage Build()
        {
            return _budgetValueBuild;
        }
    }
}
