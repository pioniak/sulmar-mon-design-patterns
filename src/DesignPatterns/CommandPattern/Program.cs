using System;
using System.Collections.Generic;

namespace CommandPattern
{

    public class CommandTests
    {
        public static void Test()
        {
            //ICommand command1 = new PrintCommand("Hello World!", 5);
            //command1.Execute();


            //ICommand command2 = new ScanCommand();
            //command2.Execute();

            Queue<ICommand> commands = new Queue<ICommand>();
            commands.Enqueue(new PrintCommand("Hello World!", 5));
            commands.Enqueue(new PrintCommand("Hello World!", 5));
            commands.Enqueue(new ScanCommand());


           while(commands.Count>0)
           {
                ICommand command = commands.Peek();

                if (command.CanExecute())
                {
                    commands.Dequeue().Execute();
                }
           }


        }
    }

    public interface ICommand
    {
        void Execute();

        bool CanExecute();
    }

    public class PrintCommand : ICommand
    {
        private readonly string content;
        private int pages;

        public PrintCommand(string content, int pages)
        {
            this.content = content;
            this.pages = pages;
        }

        public bool CanExecute()
        {
            return pages > 0;
        }

        public void Execute()
        {
            Console.WriteLine($"Printing {content}...");

            pages--;
        }
    }

    public class ScanCommand : ICommand
    {
        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            Console.WriteLine("Scanning...");
        }


    }


    public class Printer
    {
        private bool hasPaper => pages > 0;

        private int pages;


        public Printer()
        {
            pages = 5;
        }

        public void Print(string content)
        {
            Console.WriteLine($"Printing {content}...");

            pages--;
        }

        public bool CanPrint()
        {
            return hasPaper;
        }

        public void Scan()
        {
            Console.WriteLine("Scanning...");
        }

        public bool CanScan()
        {
            return pages > 0;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Command Pattern!");

            CommandTests.Test();

            Printer printer = new Printer();

            for (int i = 0; i < 3; i++)
            {
                if (printer.CanPrint())
                {
                    printer.Print("Hello World!");
                }
            }


            if (printer.CanScan())
            {
                printer.Scan();
            }

            for (int i = 0; i < 3; i++)
            {
                if (printer.CanPrint())
                {
                    printer.Print("Hello World!");
                }
            }


        }
    }

    #region Models

    public class Message
    {
        public Message(string from, string to, string content)
        {
            From = from;
            To = to;
            Content = content;
        }

        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }

     
        public void Send()
        {
            Console.WriteLine($"Send message from <{From}> to <{To}> {Content}");
        }

        public bool CanSend()
        {
            return !(string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Content));
        }

        public void Print()
        {
            Console.WriteLine($"Print message from <{From}> to <{To}> {Content}");
        }

        public bool CanPrint()
        {
            return string.IsNullOrEmpty(Content);
        }



    }

    #endregion
}
