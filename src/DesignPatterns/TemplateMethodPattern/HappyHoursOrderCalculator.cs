using System;

namespace TemplateMethodPattern
{
    // Happy Hours - 10% upustu w godzinach od 9 do 15
    public class HappyHoursOrderCalculator
    {
        public decimal CalculateDiscount(Order order)
        {
            // Warunek
            if (order.OrderDate.Hour >= 9 && order.OrderDate.Hour <= 15)
            {
                // Upust
                return order.Amount * 0.1m;
            }
            else
                return 0;
        }
    }

    public class HappyHoursPercentageDiscountOrderCalculator : DiscountOrderCalculator
    {
        private readonly TimeSpan from;
        private readonly TimeSpan to;
        private readonly decimal percentage;

        public HappyHoursPercentageDiscountOrderCalculator(TimeSpan from, TimeSpan to, decimal percentage)
        {
            this.from = from;
            this.to = to;
            this.percentage = percentage;
        }

        public override bool CanDiscount(Order order)
        {
            return order.OrderDate.TimeOfDay >= from && order.OrderDate.TimeOfDay <= to;
        }

        public override decimal Discount(Order order)
        {
            return order.Amount * percentage;
        }
    }

    public class HappyHoursFixedDiscountOrderCalculator : DiscountOrderCalculator
    {
        private readonly TimeSpan from;
        private readonly TimeSpan to;
        private readonly decimal discount;

        public override bool CanDiscount(Order order)
        {
            return order.OrderDate.TimeOfDay >= from && order.OrderDate.TimeOfDay <= to;
        }

        public override decimal Discount(Order order)
        {
            return discount;
        }
    }


    // Template Method (Szablon metody)
    public abstract class DiscountOrderCalculator
    {
        public abstract bool CanDiscount(Order order);

        public abstract decimal Discount(Order order);

        public decimal CalculateDiscount(Order order)
        {
            // Warunek (predykat)
            if (CanDiscount(order))
            {
                // Upust
                return Discount(order);
            }
            else
                return 0;
        }
    }



}
