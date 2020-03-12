using System;
using BudgetSquirrel.Business.Auth;
using BudgetSquirrel.Business.BudgetPlanning;
using BudgetSquirrel.Business.Tracking;
using Microsoft.EntityFrameworkCore;

namespace BudgetSquirrel.Data.EntityFramework
{
    public class BudgetSquirrelContext : DbContext
    {
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<DaySpanDuration> DaySpanDurations { get; set; }
        public DbSet<MonthlyBookEndedDuration> MonthlyBookEndedDurations { get; set; }
        public DbSet<BudgetPeriod> BudgetPeriods { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
