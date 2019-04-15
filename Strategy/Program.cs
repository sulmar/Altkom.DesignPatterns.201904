using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person
            {
                FirstName = "John",
                LastName = "Smith",
                Gender = Gender.Male
            };

            Visit visit = new PrivateVisit
            {
                VisitDate = DateTime.Now,
                Duration = TimeSpan.FromHours(2),
                Patient = person
            };

            IStrategy strategy = new HappyHoursStrategy(
                new Range<TimeSpan>(TimeSpan.FromHours(9), TimeSpan.FromHours(16)),
                10);

            VisitCalculator visitCalculator = new VisitCalculator(strategy);

            decimal totalAmount = visitCalculator.Calculate(visit);

            Console.WriteLine($"Total amount: {totalAmount:C2}");
        }
    }

    interface IStrategy
    {
        bool CanDiscount(Visit visit);
        decimal Discount(Visit visit);
    }

    struct Range<T>
        where T : struct
    {
        public Range(T from, T to) 
        {
            From = from;
            To = to;
        }

        public T From { get; set; }
        public T To { get; set; }

        //public bool IsBetween(T value)
        //{
        //    return value >= From && value <= To;
        //}
    }

    class HappyHoursStrategy : IStrategy
    {
        // private readonly TimeSpan from;
        // private readonly TimeSpan to;

        private readonly Range<TimeSpan> period;
        private readonly decimal discount;

        public HappyHoursStrategy(Range<TimeSpan> period, decimal discount)
        {
            this.period = period;
            this.discount = discount;
        }

        public bool CanDiscount(Visit visit)
        {
            return visit.VisitDate.TimeOfDay >= period.From
               && visit.VisitDate.TimeOfDay <= period.To;
        }

        public decimal Discount(Visit visit)
        {
            return visit.Amount - discount;
        }
    }

    class VisitCalculator
    {
        private readonly IStrategy strategy;

        public VisitCalculator(IStrategy strategy)
        {
            this.strategy = strategy;
        }

        public decimal Calculate(Visit visit)
        {
            if (strategy.CanDiscount(visit))
            {
                return strategy.Discount(visit);
            }
            else
            {
                return visit.Amount;
            }
        }
    }
 


    class PrivateVisit : Visit
    {

    }

    class NFZVisit : Visit
    {

    }

    abstract class Visit
    {
        public Person Patient { get; set; }
        public Person Doctor { get; set; }
        public DateTime VisitDate { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Amount
        {
            get
            {
                return (decimal)Duration.TotalHours * UnitPrice;
            }
        }

        public Visit()
        {
            UnitPrice = 100;
            Duration = TimeSpan.FromMinutes(15);
        }
    }

    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }

    enum Gender
    {
        Female,
        Male
    }
}
