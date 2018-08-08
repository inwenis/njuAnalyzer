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
            public decimal Charge;
        }
    }
}
