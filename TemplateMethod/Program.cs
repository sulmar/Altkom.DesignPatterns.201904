using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethod
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

            var visitCalculator = new HappyHoursCalculator(
                TimeSpan.FromHours(9), 
                TimeSpan.FromHours(16), 
                10);

            decimal totalAmount = visitCalculator.Calculate(visit);

            Console.WriteLine($"Total amount: {totalAmount:C2}");
        }
    }

    class HappyHoursCalculator : CalculatorBase
    {
        private readonly TimeSpan from;
        private readonly TimeSpan to;
        private readonly decimal discount;

        public HappyHoursCalculator(TimeSpan from, TimeSpan to, decimal discount)
        {
            this.from = from;
            this.to = to;
            this.discount = discount;
        }

        public override bool CanDiscount(Visit visit)
        {
            return visit.VisitDate.TimeOfDay >= from 
                && visit.VisitDate.TimeOfDay <= to;
        }

        public override decimal Discount(Visit visit)
        {
            return visit.Amount - discount;
        }
    }

    class GenderCalculator : CalculatorBase
    {
        private readonly Gender gender;
        private readonly decimal ratio;

        public GenderCalculator(Gender gender, decimal ratio)
        {
            this.gender = gender;
            this.ratio = ratio;
        }

        public override bool CanDiscount(Visit visit)
        {
            return visit.Patient.Gender == gender;
        }

        public override decimal Discount(Visit visit)
        {
            return visit.Amount * ratio;
        }
    }

    abstract class CalculatorBase
    {
        public abstract bool CanDiscount(Visit visit);
        public abstract decimal Discount(Visit visit);

        public virtual decimal Calculate(Visit visit)
        {
            if (CanDiscount(visit))
            {
                return Discount(visit);
            }
            else
            {
                return visit.Amount;
            }
        }

    }

    class VisitCalculator
    {
        public decimal Calculate(Visit visit)
        {
            if (visit.Patient.Gender == Gender.Female)
            {
                return visit.Amount * 0.8m;
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
                return (decimal) Duration.TotalHours * UnitPrice;
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
