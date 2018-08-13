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
            var path = @"C:\Users\fku-ext\Downloads\2018.05.19_2018.06.18.csv";
            var lines = File.ReadAllLines(path);
            var expenses = CsvToExpenseList.Parse(lines);
            var analyzer = new Analyzer(29, 10);
            foreach (var expense in expenses)
            {
                analyzer.Add(expense);
            }

            Console.WriteLine("Analyzer cost: " + analyzer.GetTotalCallsCost());
            Console.WriteLine("+9 " + (analyzer.GetTotalCallsCost() + 9));
            Console.WriteLine("expected: 71,67");
            Console.WriteLine(analyzer.GetCostDetails());
            Console.WriteLine();

            var path2 = @"C:\Users\fku-ext\Downloads\2018.05.19_2018.06.18_2.csv";
            var lines2 = File.ReadAllLines(path2);
            var expenses2 = CsvToExpenseList.Parse(lines2);
            foreach (var expense in expenses2)
            {
                analyzer.Add(expense);
            }

            Console.WriteLine("Analyzer cost: " + analyzer.GetTotalCallsCost());
            Console.WriteLine("+9 " + (analyzer.GetTotalCallsCost() + 9));
            Console.WriteLine("expected: 71,67");
            Console.WriteLine(analyzer.GetCostDetails());
            Console.WriteLine();

            ShowTotalCostForType(expenses, ExpenseType.InternationalCall);
            ShowTotalCostForType(expenses, ExpenseType.InternationlSms);
            ShowTotalCostForType(expenses, ExpenseType.SpecialSms);
            ShowTotalCostForType(expenses, ExpenseType.InfolineCall);

            Console.WriteLine();

            var tataAnalyzer = new Analyzer(29, 10);
            foreach (var expense in expenses2)
            {
                tataAnalyzer.Add(expense);
            }

            Console.WriteLine(tataAnalyzer.GetCostDetails());

            ShowTotalCostForType(expenses2, ExpenseType.InternationalCall);
            ShowTotalCostForType(expenses2, ExpenseType.InternationlSms);
            ShowTotalCostForType(expenses2, ExpenseType.SpecialSms);
            ShowTotalCostForType(expenses2, ExpenseType.InfolineCall);
            ShowTotalCostForType(expenses2, ExpenseType.RoamingCall);

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
