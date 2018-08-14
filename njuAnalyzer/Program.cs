using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace njuAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var myExpenses = File.ReadAllLines(@"C:\Users\fku-ext\Downloads\Połączenia nju mobile.csv");
            var dadsExpenses = File.ReadAllLines(@"C:\Users\fku-ext\Downloads\Połączenia nju mobile (1).csv");
            var my = CsvToExpenseList.Parse(myExpenses);
            var dads = CsvToExpenseList.Parse(dadsExpenses);
            var all = my.Union(dads);
            var periods = PeriodSplitter.GroupIntoPeriods(all, 19);
            foreach (var period in periods)
            {
                var analyzer = new Analyzer(29, 10, 9);
                foreach (var expense in period.Expenses)
                {
                    analyzer.Add(expense);
                }
                Console.WriteLine(period.Expenses.First().DateTime);
                Console.WriteLine(period.Expenses.Last().DateTime);
                Console.WriteLine(analyzer.GetCostDetails());
                ShowTotalCostForType(period.Expenses, ExpenseType.InternationlSms);
                ShowTotalCostForType(period.Expenses, ExpenseType.CellPhoneCall);
                ShowTotalCostForType(period.Expenses, ExpenseType.InfolineCall);
                ShowTotalCostForType(period.Expenses, ExpenseType.InternationalCall);
                ShowTotalCostForType(period.Expenses, ExpenseType.LandlineCall);
                ShowTotalCostForType(period.Expenses, ExpenseType.MobileData);
                ShowTotalCostForType(period.Expenses, ExpenseType.RoamingCall);
                ShowTotalCostForType(period.Expenses, ExpenseType.SMS);
                ShowTotalCostForType(period.Expenses, ExpenseType.SpecialSms);
                Console.WriteLine();
            }

            Console.WriteLine("Press [enter] to exit");
            Console.ReadLine();
        }

        private static void ShowTotalCostForType(IEnumerable<Expense> expenses, ExpenseType type)
        {
            var sum = expenses.Where(x => x.ExpenseType == type).Sum(x => x.Charge);
            Console.WriteLine(type + " " + sum);
        }
    }
}
