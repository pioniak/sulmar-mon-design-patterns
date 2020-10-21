using Stateless;
using System;
using System.Timers;

namespace StatePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello State Pattern!");

            // OrderTest();

            IMessageService messageService = new ConsoleMessageService();

            ProxyLamp lamp = new ProxyLamp(messageService);
            lamp.TimeLimit = TimeSpan.Parse("13:00");

            Console.WriteLine(lamp.Graph);


            Console.WriteLine(lamp.State);

            lamp.PushUp();
            Console.WriteLine(lamp.State);

            
            Console.ReadKey();

            lamp.PushDown();
            Console.WriteLine(lamp.State);

            lamp.PushUp();
            Console.WriteLine(lamp.State);

            lamp.PushUp();
            Console.WriteLine(lamp.State);

            lamp.PushUp();
            Console.WriteLine(lamp.State);

            lamp.PushDown();
            Console.WriteLine(lamp.State);

        }

        private static void OrderTest()
        {
            Order order = Order.Create();

            order.Completion();

            if (order.Status == OrderStatus.Completion)
            {
                order.Status = OrderStatus.Sent;
                Console.WriteLine("Your order was sent.");
            }

            order.Cancel();
        }
    }

    #region Models

    public class Order
    {
        public Order(string orderNumber)
        {
            Status = OrderStatus.Created;

            OrderNumber = orderNumber;
            OrderDate = DateTime.Now;
         
        }

        public DateTime OrderDate { get; set; }

        public string OrderNumber { get; set; }

        public OrderStatus Status { get; set; }

        private static int indexer;

        public static Order Create()
        {
            Order order = new Order($"Order #{indexer++}");

            if (order.Status == OrderStatus.Created)
            {
                Console.WriteLine("Thank you for your order");
            }

            return order;
        }

        public void Completion()
        {
            if (Status == OrderStatus.Created)
            {
                this.Status = OrderStatus.Completion;

                Console.WriteLine("Your order is in progress");
            }
        }

        public void Cancel()
        {
            if (this.Status == OrderStatus.Created || this.Status == OrderStatus.Completion)
            {
                this.Status = OrderStatus.Canceled;

                Console.WriteLine("Your order was cancelled.");
            }
        }

    }

    public enum OrderStatus
    {
        Created,
        Completion,
        Sent,
        Canceled,
        Done
    }


    public interface IMessageService
    {
        void Send(string message);
    }

    public class ConsoleMessageService : IMessageService
    {
        public void Send(string message)
        {
            Console.WriteLine(message);
        }
    }

    // Przykład użycia wzorca Proxy
    public class ProxyLamp : Lamp
    {
        private StateMachine<LampState, LampTrigger> machine;

        public string Graph => Stateless.Graph.UmlDotGraph.Format(machine.GetInfo());

        public override LampState State => machine.State;

        private Timer timer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds);

        public ProxyLamp(IMessageService messageService)
            : base(messageService)
        {
            machine = new StateMachine<LampState, LampTrigger>(LampState.Off);

            timer.Elapsed += (s, a) => machine.Fire(LampTrigger.ElapsedTime);

            machine.Configure(LampState.Off)
                // .Permit(LampTrigger.PushUp, LampState.On)
                .PermitIf(LampTrigger.PushUp, LampState.On, () => DateTime.Now.TimeOfDay > TimeLimit, $"Limit > {TimeLimit}")
                .IgnoreIf(LampTrigger.PushUp, () => DateTime.Now.TimeOfDay <= TimeLimit, $"Limit <= {TimeLimit}")
                .Ignore(LampTrigger.PushDown)
                .OnEntry(() => messageService.Send("Dziękujemy za wyłączenie światła"), nameof(messageService.Send));

            machine.Configure(LampState.On)
                .OnEntry(() => messageService.Send("Pamiętaj o wyłączeniu światła!"), nameof(messageService.Send))
                .OnEntry(() => timer.Start(), "Start timer")
                .Permit(LampTrigger.PushDown, LampState.Off)
                .Permit(LampTrigger.PushUp, LampState.Red)
                .Permit(LampTrigger.ElapsedTime, LampState.Off)
                .OnExit(() => timer.Stop());


            machine.Configure(LampState.Red)
                .Permit(LampTrigger.PushUp, LampState.On)
                .Permit(LampTrigger.PushDown, LampState.Off);
        }

        public override void PushUp() => machine.Fire(LampTrigger.PushUp);
        public override void PushDown() => machine.Fire(LampTrigger.PushDown);

    }


    // dotnet add package stateless

    public class Lamp
    {
        public virtual LampState State { get; set; }

        private readonly IMessageService messageService;

        public TimeSpan TimeLimit { get; set; } = TimeSpan.Parse("13:00");

        public Lamp(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public virtual void PushUp()
        {
            if (State== LampState.Off)
            {
                State = LampState.On;
            }
            else
            if (State == LampState.On)
            {
                State = LampState.Red;
            }
        }

        public virtual void PushDown()
        {
            if (State == LampState.On)
            {
                State = LampState.Off;
            }
            else
           if (State == LampState.Red)
            {
                State = LampState.Off;
            }
        }
    }

    public enum LampTrigger
    {
        PushUp,
        PushDown,
        ElapsedTime
    }

    public enum LampState
    {
        On,
        Off,
        Red
    }

    #endregion

}
