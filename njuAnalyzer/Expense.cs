namespace njuAnalyzer
{
    public class Expense
    {
        private decimal _charge;
        private ExpenseType _expenseType;

        public Expense(decimal charge, ExpenseType expenseType)
        {
            _charge = charge;
            _expenseType = expenseType;
        }

        public decimal Charge
        {
            get => _charge;
        }

        public ExpenseType ExpenseType
        {
            get => _expenseType;
        }
    }
}