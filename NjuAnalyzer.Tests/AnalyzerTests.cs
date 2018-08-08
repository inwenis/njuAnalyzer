using njuAnalyzer;
using NUnit.Framework;

[TestFixture]
class AnalyzerTests
{
    [Test]
    public static void GetcurrentCost_NoCallsAdded_Returns0()
    {
        var sut = new Analyzer();

        var currentCost = sut.GetCurrentCost();

        Assert.AreEqual(0, currentCost);
    }

    [Test]
    public static void GetCurrentCost_OneCallAdded_ReturnsCallsCost()
    {
        var sut = new Analyzer();

        var expense = new Analyzer.Expense()
        {
            Charge = 10.00m
        };
        sut.Add(expense);
        var currentCost = sut.GetCurrentCost();

        Assert.AreEqual(10.00m, currentCost);
    }

    [Test]
    public static void GetCurrentCost_TwoCallsAdded_ReturnsCallsCost()
    {
        var sut = new Analyzer();

        var expense = new Analyzer.Expense() 
        {
            Charge = 10.00m
        };
        sut.Add(expense);
        sut.Add(expense);
        var currentCost = sut.GetCurrentCost();

        Assert.AreEqual(20.00m, currentCost);
    }

}
