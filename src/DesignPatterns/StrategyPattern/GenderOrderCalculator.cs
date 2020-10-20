namespace StrategyPattern
{
    // Gender - 20% upustu dla kobiet
    public class GenderOrderCalculator
    {
        public decimal CalculateDiscount(Order order)
        {
            if (order.Customer.Gender == Gender.Female)
            {
                return order.Amount * 0.2m;
            }
            else
                return 0;
        }
    }

    public class GenderDiscountOrderStrategy : IDiscountOrderStrategy
    {
        private readonly Gender gender;
        private readonly decimal percentage;

        public GenderDiscountOrderStrategy(Gender gender, decimal percentage)
        {
            this.gender = gender;
            this.percentage = percentage;
        }

        public bool CanDiscount(Order order)
        {
            return order.Customer.Gender == gender;
        }

        public decimal Discount(Order order)
        {
            return order.Amount * percentage;
        }
    }
}
