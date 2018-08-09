using System.Collections.Generic;
using System.Linq;

namespace njuAnalyzer
{
    public class CsvToExpenseList
    {
        public static IEnumerable<Expense> Parse(IEnumerable<string> lines)
        {
            if (lines.Count() == 0)
            {
                return new List<Expense>();
            }
            else
            {
                var expenses = lines.Skip(1).Select(x =>
                {
                    var split = x.Split(';');
                    var @event = split[3].Trim();
                    var @operator = split[4].Trim();
                    var cost = split[5].Replace(" zł", "").Replace('.', ',');
                    var parsedCost = decimal.Parse(cost);
                    ExpenseType expenseType;
                    if (@event == "Dane")
                    {
                        expenseType = ExpenseType.MobileData;
                    }
                    else if(@event == "Rozmowa głosowa")
                    {
                        if (@operator == "Polska")
                        {
                            expenseType = ExpenseType.LandlineCall;
                        }
                        else
                        {
                            expenseType = ExpenseType.CellPhoneCall;
                        }
                    }
                    else
                    {
                        if(@operator == "SMS międzynarodowy")
                        {
                            expenseType = ExpenseType.InternationlSms;
                        }
                        else
                        {
                            expenseType = ExpenseType.SMS;
                        }
                    }
                    return new Expense(parsedCost, expenseType);
                });
                return expenses;
            }
        }
    }
}
