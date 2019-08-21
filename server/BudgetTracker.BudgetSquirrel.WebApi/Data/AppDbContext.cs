using BudgetTracker.Data.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.BudgetSquirrel.WebApi.Data
{
    /// <summary>
    /// <p>
    /// Problem: The entity framework command line tools can't be used inside the data assembly
    /// because Identity library wants to run the Startup.cs file which is in the web assembly,
    /// not the data assembly. But I can't run it in the Web assembly without this either because
    /// Entity Framework requires the DbContext implementation to be in the assembly in which the
    /// migration command is run, which would be the web assembly in that case.
    /// </p>
    /// <p>
    /// Solution: The web project will use this proxy class that acts as a DbContext extending the
    /// real DbContext in the data assembly. This class should not have anything in it.
    /// Any logic that needs to go in the DbContext should go in <see cref="BudgetTrackerContext"/>.
    /// </p>
    /// </summary>
    public class AppDbContext : BudgetTrackerContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }
    }
}
