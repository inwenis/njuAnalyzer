using System;
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
                    var dateTime = split[0];
                    var @event = split[3].Trim();
                    var @operator = split[4].Trim();
                    var cost = split[5].Replace(" zł", "").Replace('.', ',');
                    var parsedDateTime = DateTime.Parse(dateTime);
                    var parsedCost = decimal.Parse(cost);
                    var expenseType = ExpenseType(@event, @operator);
                    return new Expense(parsedCost, expenseType, parsedDateTime);
                });
                return expenses.ToArray();
            }
        }

        private static ExpenseType ExpenseType(string @event, string @operator)
        {
            ExpenseType expenseType;
            if (@event == "Dane")
            {
                expenseType = njuAnalyzer.ExpenseType.MobileData;
            }
            else if (@event == "Rozmowa głosowa")
            {
                if (@operator == "Polska")
                {
                    expenseType = njuAnalyzer.ExpenseType.LandlineCall;
                }
                else if (@operator == "Infolinie")
                {
                    expenseType = njuAnalyzer.ExpenseType.InfolineCall;
                }
                else if (@operator == "Dania kom")
                {
                    expenseType = njuAnalyzer.ExpenseType.InternationalCall;
                }
                else
                {
                    expenseType = njuAnalyzer.ExpenseType.CellPhoneCall;
                }
            }
            else if(@event == "Wiadomość SMS")
            {
                if (@operator == "SMS międzynarodowy")
                {
                    expenseType = njuAnalyzer.ExpenseType.InternationlSms;
                }
                else if (@operator == "SMS Specjalny")
                {
                    expenseType = njuAnalyzer.ExpenseType.SpecialSms;
                }
                else
                {
                    expenseType = njuAnalyzer.ExpenseType.SMS;
                }
            }
            else
            {
                expenseType = njuAnalyzer.ExpenseType.RoamingCall;
            }
            return expenseType;
        }
    }
}
