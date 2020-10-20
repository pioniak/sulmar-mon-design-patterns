using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace BuilderPattern
{
  
    public class FluentPhoneTests
    {
        public static void CallTest()
        {
            FluentPhone
                .Hangup()
                .From("555666777")
                .To("555999000")
                .To("555999000")
                //.WithSubject("Design Patterns")
                .Call();
        }
    }

    public interface IFrom
    {
        ITo From(string number);
    }

    public interface ITo
    {
        ISubject To(string number);
    }

    public interface ISubject : ICall, ITo
    {
        ICall WithSubject(string subject);
    }

    public interface ICall
    {
        void Call();
    }

    public class FluentPhone : IFrom, ITo, ISubject, ICall
    {
        private string from;
        private ICollection<string> to;
        private string subject;


        private FluentPhone()
        {
            to = new Collection<string>(); 
        }

        public static IFrom Hangup()
        {
            return new FluentPhone();
        }

        public ITo From(string number)
        {
            from = number;

            return this;
        }

        public ISubject To(string number)
        {
            to.Add(number);

            return this;
        }

        public ICall WithSubject(string subject)
        {
            this.subject = subject;

            return this;
        }

        public void Call()
        {
            Call(from, to, subject);
        }

        private void Call(string from, string to, string subject)
        {
            if (string.IsNullOrEmpty(from))
                throw new ArgumentNullException(nameof(from));

            if (string.IsNullOrEmpty(to))
                throw new ArgumentNullException(nameof(to));


            Console.WriteLine($"Calling from {from} to {to} with subject {subject}");
        }

        private void Call(string from, string to)
        {
            Console.WriteLine($"Calling from {from} to {to}");
        }

        private void Call(string from, IEnumerable<string> tos, string subject)
        {
            foreach (var to in tos)
            {
                if (string.IsNullOrEmpty(subject))
                    Call(from, to);
                else
                    Call(from, to, subject);
            }
        }
    }

    public class Phone
    {
        public void Call(string from, string to, string subject)
        {
            Console.WriteLine($"Calling from {from} to {to} with subject {subject}");
        }

        public void Call(string from, string to)
        {
            Console.WriteLine($"Calling from {from} to {to}");
        }

        public void Call(string from, IEnumerable<string> tos, string subject)
        {
            foreach (var to in tos)
            {
                Call(from, to, subject);
            }
        }
    }


}