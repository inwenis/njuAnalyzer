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

    [Test]
    public static void Split_ExprensesAreFromTwoPeriods_Returns2Periods()
    {
        var expenses = new List<Expense>
        {
            new Expense(1, ExpenseType.CellPhoneCall, DateTime.Now),
            new Expense(1, ExpenseType.CellPhoneCall, DateTime.Now.AddDays(35))
        };
        var periods = PeriodSplitter.Split(expenses, 18);
        Assert.AreEqual(2, periods.Count());
        Assert.AreSame(expenses.First(), periods.First().Expenses.First());
        Assert.AreSame(expenses.Last(), periods.Last().Expenses.First());
    }
}