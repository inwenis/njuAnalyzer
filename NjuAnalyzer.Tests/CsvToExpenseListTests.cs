using System.Linq;
using njuAnalyzer;
using NUnit.Framework;

[TestFixture]
class CsvToExpenseListTests
{
    [Test]
    public static void Parse_ForEmptyInput_ReturnsEmptyOuput()
    {
        var lines = new string[]
        {
        };
        var result = CsvToExpenseList.Parse(lines);

        Assert.AreEqual(0, result.Count());
    }

    [Test]
    public static void Parse_ParsingMobileDataExpense_ReturnsCorrectlyParsedMobileDataExpense()
    {
        var lines = new string[]
        {
            "data i godzina;nr telefonu;liczba;zdarzenie;operator;koszt",
            "2018.07.01 00:52;Internet;100.00kB; Dane;Standard;0.00 zł"
        };
        var result = CsvToExpenseList.Parse(lines);

        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(ExpenseType.MobileData, result.First().ExpenseType);
        Assert.AreEqual(0.00m, result.First().Charge);
    }

    [Test]
    public static void Parse_Parsing2MobileDataExpenses_ReturnsCorrectlyParsedMobileDataExpenses()
    {
        var lines = new string[]
        {
            "data i godzina;nr telefonu;liczba;zdarzenie;operator;koszt",
            "2018.07.01 00:52;Internet;100.00kB; Dane;Standard;1.00 zł",
            "2018.07.01 00:52;Internet;100.00kB; Dane;Standard;2.00 zł"
        };
        var result = CsvToExpenseList.Parse(lines);

        Assert.AreEqual(2, result.Count());
        Assert.AreEqual(ExpenseType.MobileData, result.First().ExpenseType);
        Assert.AreEqual(1.00m, result.First().Charge);
        Assert.AreEqual(ExpenseType.MobileData, result.Last().ExpenseType);
        Assert.AreEqual(2.00m, result.Last().Charge);
    }

    [Test]
    public static void Parse_ParsingCellPhoneCall_ReturnsCorrectlyParsedExpense()
    {
        var lines = new string[]
        {
            "data i godzina;nr telefonu;liczba;zdarzenie;operator;koszt",
            "2018.05.19 13:14;+48 796 196 666;0:05min:sek; Rozmowa głosowa;Play;0.02 zł"
        };
        var result = CsvToExpenseList.Parse(lines);

        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(ExpenseType.CellPhoneCall, result.First().ExpenseType);
        Assert.AreEqual(0.02m, result.First().Charge);
    }


}
