using System;

namespace State
{
    class Program
    {
        static void Main(string[] args)
        {
            LampTest();

            // OrderTest();
        }

        private static void LampTest()
        {
            Lamp lamp = new Lamp("L001");

            Console.WriteLine(lamp.Graph);
            
            Console.WriteLine(lamp.State);

            if (lamp.CanPushDown)
                lamp.PushDown();


            Console.WriteLine(lamp.State);

            lamp.PushDown();

            Console.WriteLine(lamp.State);

            if (lamp.CanPushUp)
            lamp.PushUp();

            Console.WriteLine(lamp.State);
            if (lamp.CanPushUp)
                lamp.PushUp();

            Console.WriteLine(lamp.State);

        }

        private static void OrderTest()
        {
            Order order = new Order(1, "ZA 1/04/2019", 1000);
            Warehouse(order);

            Cancel(order);
        }

        private static void Cancel(Order order)
        {
            if (order.Status == OrderStatus.Created
                || order.Status == OrderStatus.Completion)
            {
                order.Status = OrderStatus.Canceled;
            }
        }

        private static void Warehouse(Order order)
        {
            if (order.Status == OrderStatus.Created)
            {
                order.Status = OrderStatus.Completion;

                // ...               

                order.Status = OrderStatus.Sent;

            }
        }
    }

    class Order
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }

        public Order()
        {
            Status = OrderStatus.Created;
        }

        public Order(int id, string number, decimal totalAmount)
            : this()
        {
            Id = id;
            Number = number;
            TotalAmount = totalAmount;
        }
    }

    enum OrderStatus
    {
        Created,
        Completion,
        Sent,
        Canceled,
        Done
    }
}
