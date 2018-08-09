using System;

namespace njuAnalyzer
{
    public class Analyzer
    {
        private decimal _cellPhoneCallsSum;
        private decimal _cellPhoneCallsThreshold;
        private decimal _landlineCallsSum;

        public Analyzer(decimal cellPhoneCallsThreshold)
        {
            _cellPhoneCallsThreshold = cellPhoneCallsThreshold;
        }

        public void Add(Expense expense)
        {
            if (expense.ExpenseType == ExpenseTypes.CellPhoneCall)
            {
                if (_cellPhoneCallsSum + expense.Charge < _cellPhoneCallsThreshold)
                {
                    _cellPhoneCallsSum += expense.Charge;
                }
                else
                {
                    _cellPhoneCallsSum = _cellPhoneCallsThreshold;
                }
            }
            else
            {
                _landlineCallsSum += expense.Charge;
            }
        }

        public decimal GetCurrentCost()
        {
            return _cellPhoneCallsSum + _landlineCallsSum;
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
