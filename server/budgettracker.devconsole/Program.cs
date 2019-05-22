using System;
using budgettracker.business.Serialization;
using budgettracker.common.Models;
using budgettracker.data;

namespace budgettracker.devconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Budget b = BudgetStore.Get();
            Console.WriteLine(BudgetSerialization.BudgetToJson(b));
        }
    }
}
