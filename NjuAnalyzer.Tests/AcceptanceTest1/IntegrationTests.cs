using System;
using System.IO;
using njuAnalyzer;
using NUnit.Framework;

[TestFixture]
class AcceptanceTest1
{
    [Test]
    public void analysing_expenses_for_may_returns_expected_total_cost()
    {
        Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        var lines1 = File.ReadAllLines("./AcceptanceTest1/2018.05.19_2018.06.18.csv");
        var lines2 = File.ReadAllLines("./AcceptanceTest1/2018.05.19_2018.06.18_2.csv");
        var expenses1 = CsvToExpenseList.Parse(lines1);
        var expenses2 = CsvToExpenseList.Parse(lines2);
        var analyzer = new Analyzer(29, 10);
        foreach (var expense in expenses1)
        {
            analyzer.Add(expense);
        }
        foreach (var expense in expenses2)
        {
            analyzer.Add(expense);
        }
        // 9zł is the extra change for my second number
        Assert.AreEqual(71.67m, analyzer.GetTotalCallsCost() + 9);
    }
}
