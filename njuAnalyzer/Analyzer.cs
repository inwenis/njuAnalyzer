using System;

namespace njuAnalyzer
{
    public class Analyzer
    {
        private decimal _sum;
        private decimal _threshold;

        public Analyzer(decimal threshold)
        {
            _threshold = threshold;
        }

        public decimal GetCurrentCost()
        {
            return _sum;
        }

        public void Add(Expense expense)
        {
            if (_sum + expense.Charge < _threshold)
            {
                _sum += expense.Charge;
            }
            else
            {
                _sum = _threshold;
            }
        }

        public class Expense
        {
            private decimal _charge;
            private ExpenseTypes _expenseType;

            public Expense(decimal charge, ExpenseTypes expenseType)
            {
                _charge = charge;
                _expenseType = expenseType;
            }

            public decimal Charge
            {
                get => _charge;
            }

            public ExpenseTypes ExpenseType
            {
                get => _expenseType;
            }
        }

        public enum ExpenseTypes
        {
            LandlineCall,
            CellPhoneCall
        }

    }
}
