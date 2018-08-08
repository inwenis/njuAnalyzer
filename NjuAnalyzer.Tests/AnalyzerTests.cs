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
}