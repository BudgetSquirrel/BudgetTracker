using System;
using System.Collections.Generic;

namespace BudgetTracker.Data.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateDeleted { get; set; }

        public List<BudgetModel> Budgets { get; set; }
    }
}