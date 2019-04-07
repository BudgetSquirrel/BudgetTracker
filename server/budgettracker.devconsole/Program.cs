using System;
using budgettracker.data;

namespace budgettracker.devconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Budget b = new Budget();
            b.Name = "Test budget";
            b.TotalBalance = 65000.56;
            Console.WriteLine("Name: " + b.Name + " with $" + b.TotalBalance.ToString());
            Console.WriteLine("Hello World!");
        }
    }
}
