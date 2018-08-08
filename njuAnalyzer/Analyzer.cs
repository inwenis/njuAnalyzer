using System;

namespace njuAnalyzer
{
    public class Analyzer
    {
        private decimal _sum;

        public decimal GetCurrentCost()
        {
            return _sum;
        }

        public void Add(Expense expense)
        {
            _sum = expense.Charge;
        }

        public class Expense
        {
            public decimal Charge;
        }
    }
}
