namespace TemplateMethodPattern
{
    // Gender - 20% upustu dla kobiet
    public class GenderOrderCalculator
    {
        public decimal CalculateDiscount(Order order)
        {
            // Warunek
            if (order.Customer.Gender == Gender.Female)
            {
                // Upust
                return order.Amount * 0.2m;
            }
            else
                return 0;
        }
    }

    public class GenderDiscountOrderCalculator : DiscountOrderCalculator
    {
        private readonly Gender gender;
        private readonly decimal percentage;

        public GenderDiscountOrderCalculator(Gender gender, decimal percentage)
        {
            this.gender = gender;
            this.percentage = percentage;
        }

        public override bool CanDiscount(Order order)
        {
            return order.Customer.Gender == gender;
        }

        public override decimal Discount(Order order)
        {
            return order.Amount* percentage;
        }
    }
}
