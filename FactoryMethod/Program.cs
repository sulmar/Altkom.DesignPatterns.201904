using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    class Program
    {
        static void MainBefore(string[] args)
        {
            Console.WriteLine("Podaj rodzaj wizyty: (N)FZ (P)rywatna");

            string input = Console.ReadLine();

            Visit visit = null;

            switch(input)
            {
                case "N": visit = new NFZVisit(); break;
                case "P": visit = new PrivateVisit(); break;
            }
            
            decimal totalAmount = visit.Calculate();
            Console.WriteLine($"Total amount {totalAmount:C2}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Podaj rodzaj wizyty: (N)FZ (P)rywatna");

            string input = Console.ReadLine();

            Visit visit = VisitFactory.Create(input);

            //switch (input)
            //{
            //    case "N": visit = new NFZVisit(); break;
            //    case "P": visit = new PrivateVisit(); break;
            //}

            decimal totalAmount = visit.Calculate();
            Console.WriteLine($"Total amount {totalAmount:C2}");
        }
    }

    class VisitFactory
    {
        public static Visit CreateDynamic(string input)
        {
            string classname = $"FactoryMethod.{input}Visit";

            Type type = Type.GetType(classname);

            if (type == null)
            {
                throw new NotSupportedException($"{classname}");
            }

            Visit visit = (Visit)Activator.CreateInstance(type);

            return visit;
        }

        public static Visit Create(string input)
        {
            Visit visit = null;

            switch (input)
            {
                case "N": visit = new NFZVisit(); break;
                case "P": visit = new PrivateVisit(); break;
                default:
                    throw new NotSupportedException($"{nameof(input)} = {input}");
            }

            return visit;
        }
    }

    class PrivateVisit : Visit
    {

    }

    class NFZVisit : Visit
    {
        public override decimal Calculate()
        {
            return 0;
        }
    }

    abstract class Visit
    {
        public DateTime VisitDate { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Amount { get; set; }

        public Visit()
        {
            Amount = 100;
            Duration = TimeSpan.FromMinutes(15);            
        }

        public virtual decimal Calculate()
        {
            return (decimal) Duration.TotalHours * Amount;
        }
    }

    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
