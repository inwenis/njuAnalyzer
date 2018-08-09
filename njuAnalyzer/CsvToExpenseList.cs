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
                    Analyzer.ExpenseTypes expenseTypes;
                    var @event = split[3].Trim();
                    if (@event == "Dane")
                    {
                        expenseTypes = Analyzer.ExpenseTypes.MobileData;
                    }
                    else if(@event == "Rozmowa głosowa")
                    {
                        expenseTypes = Analyzer.ExpenseTypes.CellPhoneCall;
                    }
                    else
                    {
                        expenseTypes = Analyzer.ExpenseTypes.SMS;
                    }
                    return new Analyzer.Expense(parsedCost, expenseTypes);
                });
                return expenses;
            }
        }
    }
}
