using System;

namespace StrategyPattern
{
    // Happy Hours - 10% upustu w godzinach od 9 do 15
    public class HappyHoursOrderCalculator
    {
        public decimal CalculateDiscount(Order order)
        {
            if (order.OrderDate.Hour >= 9 && order.OrderDate.Hour <= 15)
            {
                return order.Amount * 0.1m;
            }
            else
                return 0;
        }
    }


    public class DiscountOrderCalculator
    {
        private IDiscountOrderStrategy strategy;

        public DiscountOrderCalculator(IDiscountOrderStrategy strategy)
        {
            this.strategy = strategy;
        }

        public decimal CalculateDiscount(Order order)
        {
            if (strategy.CanDiscount(order))
            {
                return strategy.Discount(order);
            }
            else
                return 0;
        }
    }

    public class DiscountOrderCalculator2
    {
        private readonly ICanDiscountStrategy canDiscountStrategy;
        private readonly IDiscountStrategy discountStategy;

        public DiscountOrderCalculator2(
            ICanDiscountStrategy canDiscountStrategy, 
            IDiscountStrategy discountStategy)
        {
            this.canDiscountStrategy = canDiscountStrategy;
            this.discountStategy = discountStategy;
        }

        public decimal CalculateDiscount(Order order)
        {
            if (canDiscountStrategy.CanDiscount(order))
            {
                return discountStategy.Discount(order);
            }
            else
                return 0;
        }
    }


    public interface IDiscountOrderStrategy
    {
        bool CanDiscount(Order order);
        decimal Discount(Order order);
    }

    public interface ICanDiscountStrategy
    {
        bool CanDiscount(Order order);
    }

    public class HappyHoursCanDiscountStrategy : ICanDiscountStrategy
    {
        private readonly TimeSpan from;
        private readonly TimeSpan to;

        public HappyHoursCanDiscountStrategy(TimeSpan from, TimeSpan to)
        {
            this.from = from;
            this.to = to;
        }

        public bool CanDiscount(Order order)
        {
            return order.OrderDate.TimeOfDay >= from && order.OrderDate.TimeOfDay <= to;
        }
    }

    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal percentage;

        public PercentageDiscountStrategy(decimal percentage)
        {
            this.percentage = percentage;
        }

        public decimal Discount(Order order)
        {
            return order.Amount * percentage;
        }
    }

    public class FixedDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal amount;

        public FixedDiscountStrategy(decimal amount)
        {
            this.amount = amount;
        }

        public decimal Discount(Order order)
        {
            return amount;
        }
    }

    public interface IDiscountStrategy
    {
        decimal Discount(Order order);
    }

    public class HappyHoursDiscountOrderStrategy : IDiscountOrderStrategy
    {
        private readonly TimeSpan from;
        private readonly TimeSpan to;
        private readonly decimal percentage;

        public HappyHoursDiscountOrderStrategy(TimeSpan from, TimeSpan to, decimal percentage)
        {
            this.from = from;
            this.to = to;
            this.percentage = percentage;
        }

        public bool CanDiscount(Order order)
        {
            return order.OrderDate.TimeOfDay >= from && order.OrderDate.TimeOfDay <= to;
        }

        public decimal Discount(Order order)
        {
            return order.Amount * percentage;
        }
    }



}
