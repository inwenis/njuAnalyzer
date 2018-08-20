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
            var myExpenses = File.ReadAllLines(@"C:\Users\fku-ext\Downloads\Połączenia nju mobile_ja.csv");
            var dadsExpenses = File.ReadAllLines(@"C:\Users\fku-ext\Downloads\Połączenia nju mobile_tata.csv");
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
                foreach (var value in (ExpenseType[]) Enum.GetValues(typeof(ExpenseType)))
                {
                    ShowTotalCostForType(period.Expenses, value);
                }
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
