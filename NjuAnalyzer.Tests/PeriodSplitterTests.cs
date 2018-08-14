using System;
using System.Collections.Generic;
using System.Linq;
using njuAnalyzer;
using NUnit.Framework;

[TestFixture]
class PeriodSplitterTests
{
    [Test]
    public static void GroupIntoPeriods_ForEmptyInput_ReturnsEmptyOuput()
    {
        var expenses = new List<Expense>();
        var periods = PeriodSplitter.GroupIntoPeriods(expenses, 18);
        Assert.AreEqual(0, periods.Count());
    }

    [Test]
    public static void GroupIntoPeriods_OneExpense_ReturnsInOnePeriod()
    {
        var expenses = new List<Expense>
        {
            new Expense(1, ExpenseType.CellPhoneCall, DateTime.Now)
        };
        var periods = PeriodSplitter.GroupIntoPeriods(expenses, 18);
        Assert.AreEqual(1, periods.Count());
        Assert.AreSame(expenses.First(), periods.First().Expenses.First());
    }

    [Test]
    public static void GroupIntoPeriods_TwoExprensesFromOnePeriod_ReturnsInOnePeriodWith2Expenses()
    {
        var expenses = new List<Expense>
        {
            new Expense(1, ExpenseType.CellPhoneCall, new DateTime(2018, 10, 10)),
            new Expense(1, ExpenseType.CellPhoneCall, new DateTime(2018, 10, 11))
        };
        var periods = PeriodSplitter.GroupIntoPeriods(expenses, 18);
        Assert.AreEqual(1, periods.Count());
        Assert.AreSame(expenses.First(), periods.First().Expenses.First());
    }

    [Test]
    public static void GroupIntoPeriods_TwoExprensesAreFromTwoPeriods_Returns2PeriodsWith1ExpensePerPeriod()
    {
        var expenses = new List<Expense>
        {
            new Expense(1, ExpenseType.CellPhoneCall, DateTime.Now),
            new Expense(1, ExpenseType.CellPhoneCall, DateTime.Now.AddDays(35))
        };
        var periods = PeriodSplitter.GroupIntoPeriods(expenses, 18);
        Assert.AreEqual(2, periods.Count());
        Assert.AreSame(expenses.First(), periods.First().Expenses.First());
        Assert.AreSame(expenses.Last(), periods.Last().Expenses.First());
    }

    [Test]
    public static void GroupIntoPeriods_MultipleExpensesFromTwoPeriods_Returns2PeriodsWithCorrectlyAssignedExpenses()
    {
        var period1expense1 = new Expense(1, ExpenseType.CellPhoneCall, new DateTime(2018, 10, 10));
        var period1expense2 = new Expense(1, ExpenseType.CellPhoneCall, new DateTime(2018, 10, 11));
        var period2expense1 = new Expense(1, ExpenseType.CellPhoneCall, new DateTime(2018, 10, 19));
        var expenses = new List<Expense>
        {
            period1expense1,
            period1expense2,
            period2expense1,
        };
        var periods = PeriodSplitter.GroupIntoPeriods(expenses, 18);
        Assert.AreEqual(2, periods.Count());
        Assert.AreEqual(2, periods.First().Expenses.Count());
        Assert.AreEqual(1, periods.Last().Expenses.Count());
        Assert.AreSame(period1expense1, periods.First().Expenses.First());
        Assert.AreSame(period1expense2, periods.First().Expenses.Last());
        Assert.AreSame(period2expense1, periods.Last().Expenses.First());
    }

}