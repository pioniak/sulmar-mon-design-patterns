using System;

namespace ProxyPattern
{
    public class Customer
    {
        public int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }


    public class ProxyCustomer : Customer
    {
        public bool HasChanged { get; private set; }

        public override string FirstName
        {
            get => base.FirstName;
            set
            {
                base.FirstName = value;
                HasChanged = true;
            }
        }

        public override string LastName
        {
            get => base.LastName; set
            {
                base.LastName = value;
                HasChanged = true;
            }
        }

    }



    class Program
    {
        static void Main(string[] args)
        {
            ProxyCustomer customer = new ProxyCustomer();
            customer.FirstName = "John";

            if (customer.HasChanged)
            {

            }    



            Console.WriteLine("Hello Proxy Pattern!");
            SaveProductTest();

        }

        private static void SaveProductTest()
        {
            ProductsDbContext context = new ProductsDbContext();

            Product product = new Product(1, "Design Patterns w C#", 150m);

            context.Add(product);

            product.UnitPrice = 99m;

            context.MarkAsChanged();

            context.SaveChanges();
        }
    }

    #region Models
    public class Product
    {
        public Product(int id, string name, decimal unitPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class ProductsDbContext
    {
        private Product product;
        private bool changed;

        public void Add(Product product)
        {
            this.product = product;
        }

        public Product Get()
        {
            return product;
        }

        public void SaveChanges()
        {
            if (changed)
            {
                Console.WriteLine($"UPDATE dbo.Products SET UnitPrice = {product.UnitPrice} WHERE ProductId = {product.Id}" );
            }
        }

        public void MarkAsChanged()
        {
            changed = true;
        }
    }

    #endregion
}
