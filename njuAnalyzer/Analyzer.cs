namespace njuAnalyzer
{
    public class Analyzer
    {
        private decimal _cellPhoneCallsTotalCost;
        private decimal _cellPhoneCallsCostThreshold;
        private decimal _landlineCallsCostThreshold;
        private decimal _landlineCallTotalCost;
        private decimal _otherExpensesTotalCost;
        private decimal _extraSecondNumberCost;

        public Analyzer(decimal cellPhoneCallsCostThreshold, decimal landlineCallsCostThreshold, decimal extraSecondNumberCost = 9)
        {
            _cellPhoneCallsCostThreshold = cellPhoneCallsCostThreshold;
            _landlineCallsCostThreshold = landlineCallsCostThreshold;
            _extraSecondNumberCost = extraSecondNumberCost;
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

        public decimal GetTotalCallsCost()
        {
            return _cellPhoneCallsTotalCost + _landlineCallTotalCost + _otherExpensesTotalCost;
        }

        public CostDetails GetCostDetails()
        {
            return new CostDetails
            {
                CellPhoneCallsTotalCost = _cellPhoneCallsTotalCost,
                LandlineCallTotalCost = _landlineCallTotalCost,
                OtherExpensesTotalCost = _otherExpensesTotalCost,
                ExtraSecondNumberCost = _extraSecondNumberCost
            };
        }

    }

    public class CostDetails
    {
        public decimal CellPhoneCallsTotalCost { get; set; }
        public decimal LandlineCallTotalCost { get; set; }
        public decimal OtherExpensesTotalCost { get; set; }
        public decimal ExtraSecondNumberCost { get; set; }

        public override string ToString()
        {
            return $"cell:{CellPhoneCallsTotalCost:00.00} land:{LandlineCallTotalCost:00.00} other:{OtherExpensesTotalCost:00.00} extra: {ExtraSecondNumberCost:00.00} total: {Sum():00.00}";
        }

        public decimal Sum()
        {
            return CellPhoneCallsTotalCost + LandlineCallTotalCost + OtherExpensesTotalCost + ExtraSecondNumberCost;
        }
    }
}
