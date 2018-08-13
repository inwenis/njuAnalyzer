using System.Collections.Generic;
using System.Linq;

namespace njuAnalyzer
{
    public class PeriodSplitter
    {
        public static IEnumerable<Period> Split(IEnumerable<Expense> expenses, int periodStartDay)
        {
            if (expenses.Count() == 0)
            {
                return new List<Period>();
            }
            else if (expenses.Count() == 1)
            {
                return new List<Period>
                {
                    new Period
                    {
                        Expenses = expenses
                    }
                };
            }
            else
            {
                var periods = expenses.Select(x => new Period(){Expenses = new List<Expense>(){x}});
                return periods.ToArray();
            }
        }
    }
}
