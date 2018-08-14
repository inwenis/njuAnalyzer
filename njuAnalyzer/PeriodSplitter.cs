using System;
using System.Collections.Generic;
using System.Linq;

namespace njuAnalyzer
{
    public class PeriodSplitter
    {
        public static IEnumerable<Period> Split(IEnumerable<Expense> expenses, int periodStartDay)
        {
            var expensesCopy = expenses.ToArray();
            if (expensesCopy.Count() == 0)
            {
                return new List<Period>();
            }
            else if (expensesCopy.Count() == 1)
            {
                return new List<Period>
                {
                    new Period
                    {
                        Expenses = expensesCopy
                    }
                };
            }
            else
            {
                var orderedExpenses = expenses.OrderBy(x => x.DateTime);
                var first = orderedExpenses.First();
                var last = orderedExpenses.Last();
                if (last.DateTime - first.DateTime < TimeSpan.FromDays(30))
                {
                    return new List<Period>
                    {
                        new Period
                        {
                            Expenses = expenses
                        }
                    };
                }
                var periods = expensesCopy.Select(x => new Period(){Expenses = new List<Expense>(){x}});
                return periods.ToArray();
            }
        }
    }
}
