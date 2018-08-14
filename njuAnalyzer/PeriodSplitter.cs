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

            var orderedExpenses = expensesCopy.OrderBy(x => x.DateTime);
            List<Period> periods = new List<Period>();
            var currentPeriodExpenses = new List<Expense>();
            Expense previousExpense = null;
            foreach (var expense in orderedExpenses)
            {
                if (currentPeriodExpenses.Any() && expense.DateTime.Day >= periodStartDay &&
                    previousExpense.DateTime.Day < periodStartDay)
                {
                    periods.Add(new Period()
                    {
                        Expenses = currentPeriodExpenses
                    });
                    currentPeriodExpenses = new List<Expense>();
                }

                currentPeriodExpenses.Add(expense);
                previousExpense = expense;
            }

            periods.Add(new Period()
            {
                Expenses = currentPeriodExpenses
            });

            return periods;
        }
    }
}
