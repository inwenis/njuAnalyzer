using System;
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

    [Test]
    public static void Split_AllExprensesAreFromOnePeriod_ReturnsAllExpensesInOnePeriod()
    {
        var expenses = new List<Expense>
        {
            new Expense(1, ExpenseType.CellPhoneCall, DateTime.Now)
        };
        var periods = PeriodSplitter.Split(expenses, 18);
        Assert.AreEqual(1, periods.Count());
        Assert.AreSame(expenses.First(), periods.First().Expenses.First());
    }
}