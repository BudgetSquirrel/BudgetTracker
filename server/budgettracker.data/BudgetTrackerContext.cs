using budgettracker.data.Models;
using Microsoft.EntityFrameworkCore;

namespace budgettracker.data
{
    public class BudgetTrackerContext : DbContext
    {
        public BudgetTrackerContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<BudgetModel> Budgets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureForeignKeys(modelBuilder);
        }

        protected virtual void ConfigureForeignKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetModel>()
                .HasOne<UserModel>(budget => budget.Owner)
                .WithMany(user => user.Budgets)
                .HasForeignKey(budget => budget.OwnerId);
        }
    }
}
