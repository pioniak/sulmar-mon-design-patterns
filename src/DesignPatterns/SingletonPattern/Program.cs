using System;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Singleton Pattern!");

            LoggerTest();
        }

        private static void LoggerTest()
        {
            Logger logger1 = LazySingleton<Logger>.Instance;

            Logger logger2 = LazySingleton<Logger>.Instance;

            if (ReferenceEquals(logger1, logger2))
            {

            }


            MessageService messageService = new MessageService();
            PrintService printService = new PrintService();
            messageService.Send("Hello World!");
            printService.Print("Hello World!", 3);

            if (ReferenceEquals(messageService.logger, printService.logger))
            {
                Console.WriteLine("The same instances");
            }
            else
            {
                Console.WriteLine("Different instances");
            }
        }
    }

    public class Logger
    {
        public void LogInformation(string message)
        {
            Console.WriteLine($"Logging {message}");
        }
    }



    public class ApplicationContext
    {
        public string Username { get; set; }
        public DateTime LoginDate { get; set; }

        public int SelectedTab { get; set; }
        public Customer SelectedCustomer { get; set; }
        public Product SelectedProduct { get; set; }

    }

    public class Customer
    {

    }

    public class Product
    {

    }


    public class MessageService
    {
        public Logger logger;

        public MessageService()
        {
            logger = new Logger();
        }

        public void Send(string message)
        {
            logger.LogInformation($"Send {message}");
        }
    }

    public class PrintService
    {
        public Logger logger;

        public PrintService()
        {
            logger = new Logger();
        }

        public void Print(string content, int copies)
        {
            for (int i = 1; i < copies+1; i++)
            {
                logger.LogInformation($"Print {i} copy of {content}");
            }
        }
    }

    public sealed class LazySingleton<T>
        where T : new()
    {

        private static readonly Lazy<T> lazy = new Lazy<T>(() => new T());

        public static T Instance => lazy.Value;



    }    
}
