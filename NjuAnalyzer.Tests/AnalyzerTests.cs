using njuAnalyzer;
using NUnit.Framework;

[TestFixture]
class AnalyzerTests
{
    private static decimal _dummyLargeThreshold = 100.00m;

    [Test]
    public static void GetcurrentCost_NoCallsAdded_Returns0()
    {
        var sut = new Analyzer(_dummyLargeThreshold, _dummyLargeThreshold);

        var currentCost = sut.GetTotalCost();

        Assert.AreEqual(0, currentCost);
    }

    [Test]
    public static void GetCurrentCost_OneCallAdded_ReturnsCallsCost()
    {
        var sut = new Analyzer(_dummyLargeThreshold, _dummyLargeThreshold);

        var expense = new Expense(10.00m, ExpenseType.CellPhoneCall);
        sut.Add(expense);
        var currentCost = sut.GetTotalCost();

        Assert.AreEqual(10.00m, currentCost);
    }

    [Test]
    public static void GetCurrentCost_TwoCallsAdded_ReturnsCallsCost()
    {
        var sut = new Analyzer(_dummyLargeThreshold, _dummyLargeThreshold);

        var expense = new Expense(10.00m, ExpenseType.CellPhoneCall); 
        sut.Add(expense);
        sut.Add(expense);
        var currentCost = sut.GetTotalCost();

        Assert.AreEqual(20.00m, currentCost);
    }

    [Test]
    public static void GetCurrentCost_CallsExceedCostThreshold_ReturnsCallsCostNoGreaterThanThreshold()
    {
        var sut = new Analyzer(29.00m, _dummyLargeThreshold);

        var expense = new Expense(10.00m, ExpenseType.CellPhoneCall); 
        sut.Add(expense);
        sut.Add(expense);
        sut.Add(expense);
        var currentCost = sut.GetTotalCost();

        Assert.AreEqual(29.00m, currentCost);
    }

    [Test]
    public static void GetCurrentCost_AddingLanlineCall_AddsChargeToCost()
    {
        var sut = new Analyzer(29.00m, _dummyLargeThreshold);

        var callPhoneCall = new Expense(10.00m, ExpenseType.CellPhoneCall);
        var landLineExpense = new Expense(5.0m, ExpenseType.LandlineCall);
        sut.Add(callPhoneCall);
        sut.Add(callPhoneCall);
        sut.Add(landLineExpense);
        var currentCost = sut.GetTotalCost();

        Assert.AreEqual(25.00m, currentCost);
    }

    [Test]
    public static void GetCurrentCost_AddingLanlineCallWhenCellPhoneCallReachThreshold_AddsChargeToCost()
    {
        var sut = new Analyzer(29.00m, _dummyLargeThreshold);

        var callPhoneCall = new Expense(10.00m, ExpenseType.CellPhoneCall);
        var landLineExpense = new Expense(5.0m, ExpenseType.LandlineCall);
        sut.Add(callPhoneCall);
        sut.Add(callPhoneCall);
        sut.Add(callPhoneCall);
        sut.Add(landLineExpense);
        var currentCost = sut.GetTotalCost();

        Assert.AreEqual(34.00m, currentCost);
    }

    [Test]
    public static void GetCurrentCost_LandlineCallsExceedLandlineCallsThreshold_SumReturnsLandLineCallsThreshold()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var landLineExpense = new Expense(5.0m, ExpenseType.LandlineCall);
        sut.Add(landLineExpense);
        sut.Add(landLineExpense);
        sut.Add(landLineExpense);
        var currentCost = sut.GetTotalCost();

        Assert.AreEqual(10.00m, currentCost);
    }

    [Test]
    public static void GetCurrentCost_AddingSMSCostAndCellPhoneCallsCostIsReached_SumReturnsCellPhoneThreshold()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var cellPhoneExponse = new Expense(10.0m, ExpenseType.CellPhoneCall);
        var sms = new Expense(0.09m, ExpenseType.SMS);
        sut.Add(cellPhoneExponse);
        sut.Add(cellPhoneExponse);
        sut.Add(cellPhoneExponse);
        sut.Add(sms);
        var currentCost = sut.GetTotalCost();

        Assert.AreEqual(29.00m, currentCost);
    }

    [Test]
    public static void GetCurrentCost_AddingMobileDataCostAndCellPhoneCallsCostIsReached_SumReturnsCellPhoneThreshold()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var cellPhoneExponse = new Expense(10.0m, ExpenseType.CellPhoneCall);
        var mobileData = new Expense(15.00m, ExpenseType.MobileData);
        sut.Add(cellPhoneExponse);
        sut.Add(cellPhoneExponse);
        sut.Add(cellPhoneExponse);
        sut.Add(mobileData);
        var currentCost = sut.GetTotalCost();

        Assert.AreEqual(29.00m, currentCost);
    }

    [Test]
    public static void GetCurrentCost_AddingInternationlSMSAndThresholdsAreReached_InternationSMSCostIsAddedToTotalCost()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var cellPhoneExponse = new Expense(30.0m, ExpenseType.CellPhoneCall);
        var landLineExpense = new Expense(30.0m, ExpenseType.LandlineCall);
        var internationalSms = new Expense(0.50m, ExpenseType.InternationlSms);
        sut.Add(cellPhoneExponse);
        sut.Add(landLineExpense);
        sut.Add(internationalSms);
        var currentCost = sut.GetTotalCost();

        Assert.AreEqual(39.50m, currentCost);
    }

    [Test]
    public static void GetCurrentCost_AddingSpecialSmsAndThresholdsAreReached_SpecialSmsCostIsAddedToTotalCost()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var cellPhoneExponse = new Expense(30.0m, ExpenseType.CellPhoneCall);
        var landLineExpense = new Expense(30.0m, ExpenseType.LandlineCall);
        var specialSms = new Expense(2.49m, ExpenseType.SpecialSms);
        sut.Add(cellPhoneExponse);
        sut.Add(landLineExpense);
        sut.Add(specialSms);
        var currentCost = sut.GetTotalCost();

        Assert.AreEqual(41.49m, currentCost);
    }

    [Test]
    public static void GetCurrentCost_AddingInfolineCallAndThresholdsAreReached_SpecialSmsCostIsAddedToTotalCost()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var cellPhoneExponse = new Expense(30.0m, ExpenseType.CellPhoneCall);
        var landLineExpense = new Expense(30.0m, ExpenseType.LandlineCall);
        var specialSms = new Expense(2.49m, ExpenseType.InfolineCall);
        sut.Add(cellPhoneExponse);
        sut.Add(landLineExpense);
        sut.Add(specialSms);
        var currentCost = sut.GetTotalCost();

        Assert.AreEqual(41.49m, currentCost);
    }

}
