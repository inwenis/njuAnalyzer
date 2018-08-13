using System;

namespace njuAnalyzer
{
    public class Expense
    {
        private decimal _charge;
        private ExpenseType _expenseType;
        private DateTime _dateTime;

        public Expense(decimal charge, ExpenseType expenseType, DateTime dateTime)
        {
            _charge = charge;
            _expenseType = expenseType;
            _dateTime = dateTime;
        }

        public decimal Charge
        {
            get => _charge;
        }

        public ExpenseType ExpenseType
        {
            get => _expenseType;
        }

        public DateTime DateTime
        {
            get => _dateTime;
        }
    }
}