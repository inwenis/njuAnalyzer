using System;
using njuAnalyzer;
using NUnit.Framework;

[TestFixture]
class AnalyzerTests
{
    private static decimal _dummyLargeThreshold = 100.00m;

    [Test]
    public static void GetTotalCallsCost_NoCallsAdded_Returns0()
    {
        var sut = new Analyzer(_dummyLargeThreshold, _dummyLargeThreshold);

        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(0, currentCost);
    }

    [Test]
    public static void GetTotalCallsCost_OneCallAdded_ReturnsCallsCost()
    {
        var sut = new Analyzer(_dummyLargeThreshold, _dummyLargeThreshold);

        var expense = new Expense(10.00m, ExpenseType.CellPhoneCall, new DateTime());
        sut.Add(expense);
        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(10.00m, currentCost);
    }

    [Test]
    public static void GetTotalCallsCost_TwoCallsAdded_ReturnsCallsCost()
    {
        var sut = new Analyzer(_dummyLargeThreshold, _dummyLargeThreshold);

        var expense = new Expense(10.00m, ExpenseType.CellPhoneCall, new DateTime()); 
        sut.Add(expense);
        sut.Add(expense);
        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(20.00m, currentCost);
    }

    [Test]
    public static void GetTotalCallsCost_CallsExceedCostThreshold_ReturnsCallsCostNoGreaterThanThreshold()
    {
        var sut = new Analyzer(29.00m, _dummyLargeThreshold);

        var expense = new Expense(10.00m, ExpenseType.CellPhoneCall, new DateTime()); 
        sut.Add(expense);
        sut.Add(expense);
        sut.Add(expense);
        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(29.00m, currentCost);
    }

    [Test]
    public static void GetTotalCallsCost_AddingLanlineCall_AddsChargeToCost()
    {
        var sut = new Analyzer(29.00m, _dummyLargeThreshold);

        var callPhoneCall = new Expense(10.00m, ExpenseType.CellPhoneCall, new DateTime());
        var landLineExpense = new Expense(5.0m, ExpenseType.LandlineCall, new DateTime());
        sut.Add(callPhoneCall);
        sut.Add(callPhoneCall);
        sut.Add(landLineExpense);
        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(25.00m, currentCost);
    }

    [Test]
    public static void GetTotalCallsCost_AddingLanlineCallWhenCellPhoneCallReachThreshold_AddsChargeToCost()
    {
        var sut = new Analyzer(29.00m, _dummyLargeThreshold);

        var callPhoneCall = new Expense(10.00m, ExpenseType.CellPhoneCall, new DateTime());
        var landLineExpense = new Expense(5.0m, ExpenseType.LandlineCall, new DateTime());
        sut.Add(callPhoneCall);
        sut.Add(callPhoneCall);
        sut.Add(callPhoneCall);
        sut.Add(landLineExpense);
        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(34.00m, currentCost);
    }

    [Test]
    public static void GetTotalCallsCost_LandlineCallsExceedLandlineCallsThreshold_SumReturnsLandLineCallsThreshold()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var landLineExpense = new Expense(5.0m, ExpenseType.LandlineCall, new DateTime());
        sut.Add(landLineExpense);
        sut.Add(landLineExpense);
        sut.Add(landLineExpense);
        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(10.00m, currentCost);
    }

    [Test]
    public static void GetTotalCallsCost_AddingSMSCostAndCellPhoneCallsCostIsReached_SumReturnsCellPhoneThreshold()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var cellPhoneExponse = new Expense(10.0m, ExpenseType.CellPhoneCall, new DateTime());
        var sms = new Expense(0.09m, ExpenseType.SMS, new DateTime());
        sut.Add(cellPhoneExponse);
        sut.Add(cellPhoneExponse);
        sut.Add(cellPhoneExponse);
        sut.Add(sms);
        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(29.00m, currentCost);
    }

    [Test]
    public static void GetTotalCallsCost_AddingMobileDataCostAndCellPhoneCallsCostIsReached_SumReturnsCellPhoneThreshold()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var cellPhoneExponse = new Expense(10.0m, ExpenseType.CellPhoneCall, new DateTime());
        var mobileData = new Expense(15.00m, ExpenseType.MobileData, new DateTime());
        sut.Add(cellPhoneExponse);
        sut.Add(cellPhoneExponse);
        sut.Add(cellPhoneExponse);
        sut.Add(mobileData);
        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(29.00m, currentCost);
    }

    [Test]
    public static void GetTotalCallsCost_AddingInternationlSMSAndThresholdsAreReached_InternationSMSCostIsAddedToTotalCost()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var cellPhoneExponse = new Expense(30.0m, ExpenseType.CellPhoneCall, new DateTime());
        var landLineExpense = new Expense(30.0m, ExpenseType.LandlineCall, new DateTime());
        var internationalSms = new Expense(0.50m, ExpenseType.InternationlSms, new DateTime());
        sut.Add(cellPhoneExponse);
        sut.Add(landLineExpense);
        sut.Add(internationalSms);
        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(39.50m, currentCost);
    }

    [Test]
    public static void GetTotalCallsCost_AddingSpecialSmsAndThresholdsAreReached_SpecialSmsCostIsAddedToTotalCost()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var cellPhoneExponse = new Expense(30.0m, ExpenseType.CellPhoneCall, new DateTime());
        var landLineExpense = new Expense(30.0m, ExpenseType.LandlineCall, new DateTime());
        var specialSms = new Expense(2.49m, ExpenseType.SpecialSms, new DateTime());
        sut.Add(cellPhoneExponse);
        sut.Add(landLineExpense);
        sut.Add(specialSms);
        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(41.49m, currentCost);
    }

    [Test]
    public static void GetTotalCallsCost_AddingInfolineCallAndThresholdsAreReached_SpecialSmsCostIsAddedToTotalCost()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var cellPhoneExponse = new Expense(30.0m, ExpenseType.CellPhoneCall, new DateTime());
        var landLineExpense = new Expense(30.0m, ExpenseType.LandlineCall, new DateTime());
        var infoLineCall = new Expense(2.49m, ExpenseType.InfolineCall, new DateTime());
        sut.Add(cellPhoneExponse);
        sut.Add(landLineExpense);
        sut.Add(infoLineCall);
        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(41.49m, currentCost);
    }

    [Test]
    public static void GetTotalCallsCost_AddingRoamingCallAndThresholdsAreReached_SpecialSmsCostIsAddedToTotalCost()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var cellPhoneExponse = new Expense(30.0m, ExpenseType.CellPhoneCall, new DateTime());
        var landLineExpense = new Expense(30.0m, ExpenseType.LandlineCall, new DateTime());
        var roamingCall = new Expense(2.49m, ExpenseType.RoamingCall, new DateTime());
        sut.Add(cellPhoneExponse);
        sut.Add(landLineExpense);
        sut.Add(roamingCall);
        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(41.49m, currentCost);
    }

    [Test]
    public static void GetTotalCallsCost_AddingInternationlCallAndThresholdsAreReached_SpecialSmsCostIsAddedToTotalCost()
    {
        var sut = new Analyzer(29.00m, 10.00m);

        var cellPhoneExponse = new Expense(30.0m, ExpenseType.CellPhoneCall, new DateTime());
        var landLineExpense = new Expense(30.0m, ExpenseType.LandlineCall, new DateTime());
        var internationalCall = new Expense(2.49m, ExpenseType.InternationalCall, new DateTime());
        sut.Add(cellPhoneExponse);
        sut.Add(landLineExpense);
        sut.Add(internationalCall);
        var currentCost = sut.GetTotalCallsCost();

        Assert.AreEqual(41.49m, currentCost);
    }

    [Test]
    public static void GetCostDetails_ForNoExpenses_ReturnsPredefinedExtraCostForSecondNumber()
    {
        var sut = new Analyzer(29, 10, 9);
        var costDetails = sut.GetCostDetails();
        Assert.AreEqual(9, costDetails.ExtraSecondNumberCost);
    }
}
