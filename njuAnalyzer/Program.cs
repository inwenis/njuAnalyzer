using System;
using System.IO;

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

            Console.WriteLine("Analyzer cost: " + analyzer.GetTotalCost());
            Console.WriteLine("+9 " + (analyzer.GetTotalCost() + 9));
            Console.WriteLine("expected: 71,67");
            Console.WriteLine(analyzer.GetCostDetails());
            Console.WriteLine("Press [enter] to exit");
            Console.ReadLine();
        }
    }
}
