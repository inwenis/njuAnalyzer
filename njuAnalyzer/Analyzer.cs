using System;

namespace njuAnalyzer
{
    public class Analyzer
    {
        private decimal _cellPhoneCallsTotalCost;
        private decimal _cellPhoneCallsCostThreshold;
        private decimal _landlineCallsCostThreshold;
        private decimal _landlineCallTotalCost;

        public Analyzer(decimal cellPhoneCallsCostThreshold, decimal landlineCallsCostThreshold)
        {
            _cellPhoneCallsCostThreshold = cellPhoneCallsCostThreshold;
            _landlineCallsCostThreshold = landlineCallsCostThreshold;
        }

        public void Add(Expense expense)
        {
            if (expense.ExpenseType == ExpenseTypes.CellPhoneCall || expense.ExpenseType == ExpenseTypes.SMS || expense.ExpenseType == ExpenseTypes.MobileData)
            {
                if (IsCellPhoneCallsCostThreasholdReached(expense))
                {
                    _cellPhoneCallsTotalCost = _cellPhoneCallsCostThreshold;
                }
                else
                {
                    _cellPhoneCallsTotalCost += expense.Charge;
                }
            }
            else
            {
                if (IsLandLineCallsCostthresholdReached(expense))
                {
                    _landlineCallTotalCost = _landlineCallsCostThreshold;
                }
                else
                {
                    _landlineCallTotalCost += expense.Charge;
                }
            }
        }

        private bool IsLandLineCallsCostthresholdReached(Expense expense)
        {
            return _landlineCallTotalCost + expense.Charge >= _landlineCallsCostThreshold;
        }

        private bool IsCellPhoneCallsCostThreasholdReached(Expense expense)
        {
            return _cellPhoneCallsTotalCost + expense.Charge >= _cellPhoneCallsCostThreshold;
        }

        public decimal GetCurrentCost()
        {
            return _cellPhoneCallsTotalCost + _landlineCallTotalCost;
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
            CellPhoneCall,
            SMS,
            MobileData
        }

    }
}
