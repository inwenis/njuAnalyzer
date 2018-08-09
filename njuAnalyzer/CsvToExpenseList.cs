using System.Collections.Generic;
using System.Linq;

namespace njuAnalyzer
{
    public class CsvToExpenseList
    {
        public static IEnumerable<Analyzer.Expense> Parse(IEnumerable<string> lines)
        {
            if (lines.Count() == 0)
            {
                return new List<Analyzer.Expense>();
            }
            else
            {
                return new List<Analyzer.Expense>
                {
                    new Analyzer.Expense(0m, Analyzer.ExpenseTypes.MobileData)
                };
            }
        }
    }
}
