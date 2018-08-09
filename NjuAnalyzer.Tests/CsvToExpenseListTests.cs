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
        Assert.AreEqual(Analyzer.ExpenseTypes.MobileData, result.First().ExpenseType);
        Assert.AreEqual(0.00m, result.First().Charge);
    }
}