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
            else
            {
                return new List<Period>
                {
                    new Period
                    {
                        Expenses = expenses
                    }
                };
            }
        }
    }
}
