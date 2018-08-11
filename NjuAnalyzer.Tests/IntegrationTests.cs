using njuAnalyzer;
using NUnit.Framework;

[TestFixture]
class IntegrationTests
{
    [Test]
    public void CsvToExpenseList_and_Analyzer_work_together()
    {
        var lines = new string[]
        {
            "data i godzina;nr telefonu;liczba;zdarzenie;operator;koszt",
            "2018.06.15 21:24;4542259472;2:15min:sek; Rozmowa głosowa;Dania kom;0.57 zł"
        };
        var result = CsvToExpenseList.Parse(lines);
        var analyzer = new Analyzer(10, 29);
        foreach (var expense in result)
        {
            analyzer.Add(expense);
        }
        Assert.AreEqual(0.57m, analyzer.GetTotalCost());
    }
}
