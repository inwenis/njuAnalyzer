using System.Collections.Generic;

namespace njuAnalyzer
{
    public class CsvToExpenseList
    {
        public static IEnumerable<Analyzer.Expense> Parse(IEnumerable<string> lines)
        {
            return new List<Analyzer.Expense>();
        }
    }
}
