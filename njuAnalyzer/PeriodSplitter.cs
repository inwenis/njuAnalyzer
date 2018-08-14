using System;
using System.Collections.Generic;
using System.Linq;

namespace njuAnalyzer
{
    public class PeriodSplitter
    {
        public static IEnumerable<Period> Split(IEnumerable<Expense> expenses, int periodStartDay)
        {
            return expenses
                .GroupBy(x => ToPeriodName(x.DateTime, periodStartDay))
                .Select(x => new Period()
                {
                    Expenses = x
                });
        }

        private static string ToPeriodName(DateTime dateTime, int periodStartDay)
        {
            if (dateTime.Day == periodStartDay)
            {
                return dateTime.ToString("yyyy.MM");
            }
            else
            {
                return ToPeriodName(dateTime.AddDays(-1), periodStartDay);
            }
        }
    }
}
