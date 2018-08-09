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
                var expenses = lines.Skip(1).Select(x =>
                {
                    var split = x.Split(';');
                    var cost = split[5].Replace(" zł", "").Replace('.', ',');
                    var parsedCost = decimal.Parse(cost);
                    return new Analyzer.Expense(parsedCost, Analyzer.ExpenseTypes.MobileData);
                });
                return expenses;
            }
        }
    }
}
