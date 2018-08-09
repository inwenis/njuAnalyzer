namespace njuAnalyzer
{
    public class Analyzer
    {
        private decimal _cellPhoneCallsTotalCost;
        private decimal _cellPhoneCallsCostThreshold;
        private decimal _landlineCallsCostThreshold;
        private decimal _landlineCallTotalCost;
        private decimal _otherExpensesTotalCost;

        public Analyzer(decimal cellPhoneCallsCostThreshold, decimal landlineCallsCostThreshold)
        {
            _cellPhoneCallsCostThreshold = cellPhoneCallsCostThreshold;
            _landlineCallsCostThreshold = landlineCallsCostThreshold;
        }

        public void Add(Expense expense)
        {
            if (expense.ExpenseType == ExpenseType.CellPhoneCall || 
                expense.ExpenseType == ExpenseType.SMS || 
                expense.ExpenseType == ExpenseType.MobileData)
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
            else if(expense.ExpenseType == ExpenseType.LandlineCall)
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
            else
            {
                _otherExpensesTotalCost += expense.Charge;
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
            return _cellPhoneCallsTotalCost + _landlineCallTotalCost + _otherExpensesTotalCost;
        }




    }
}
