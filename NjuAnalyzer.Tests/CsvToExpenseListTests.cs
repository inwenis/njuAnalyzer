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
}