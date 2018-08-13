using System.Collections.Generic;
using System.Linq;
using njuAnalyzer;
using NUnit.Framework;

[TestFixture]
class PeriodSplitterTests
{
    [Test]
    public static void Split_ForEmptyInput_ReturnsEmptyOuput()
    {
        var expenses = new List<Expense>();
        var periods = PeriodSplitter.Split(expenses, 18);
        Assert.AreEqual(0, periods.Count());
    }
}