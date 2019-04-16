using ISPStrategy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISPStrategy
{
    class Program
    {
        static void Main(string[] args)
        {
            FixedHappyHoursTest();

            PercentageHappyHoursTest();

            PercentageGenderTest();

        }

        // 50% rabat dla kobiet
        private static void PercentageGenderTest()
        {
            Person person = new Person
            {
                FirstName = "Ann",
                LastName = "Smith",
                Gender = Gender.Female
            };

            Visit visit = new PrivateVisit
            {
                VisitDate = DateTime.Now,
                Duration = TimeSpan.FromHours(2),
                Patient = person
            };

            
            ICanDiscountStrategy canDiscountStrategy = new GenderCanDiscountStrategy(Gender.Female);
            IDiscountStrategy discountStrategy = new PercentageDiscountStrategy(50);

            VisitCalculator visitCalculator = new VisitCalculator(canDiscountStrategy, discountStrategy);

            decimal totalAmount = visitCalculator.Calculate(visit);
        }

        private static void PercentageHappyHoursTest()
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

            var period = new Range<TimeSpan>(TimeSpan.FromHours(8.5), TimeSpan.FromHours(15.5));
            ICanDiscountStrategy canDiscountStrategy = new HappyHoursCanDiscountStrategy(period);
            IDiscountStrategy discountStrategy = new PercentageDiscountStrategy(10);

            VisitCalculator visitCalculator = new VisitCalculator(canDiscountStrategy, discountStrategy);

            decimal totalAmount = visitCalculator.Calculate(visit);


        }

        private static void FixedHappyHoursTest()
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

            var period = new Range<TimeSpan>(TimeSpan.FromHours(8.5), TimeSpan.FromHours(15.5));
            ICanDiscountStrategy canDiscountStrategy = new HappyHoursCanDiscountStrategy(period);
            IDiscountStrategy discountStrategy = new FixedDiscountStrategy(20);

            VisitCalculator visitCalculator = new VisitCalculator(canDiscountStrategy, discountStrategy);

            decimal totalAmount = visitCalculator.Calculate(visit);
        }
    }


    interface ICanDiscountStrategy
    {
        bool CanDiscount(Visit visit);
    }

    interface IDiscountStrategy
    {
        decimal Discount(Visit visit);
    }

    class VisitCalculator
    {
        private readonly ICanDiscountStrategy canDiscountStrategy;
        private readonly IDiscountStrategy discountStrategy;

        public VisitCalculator(
            ICanDiscountStrategy canDiscountStrategy, 
            IDiscountStrategy discountStrategy)
        {
            this.canDiscountStrategy = canDiscountStrategy;
            this.discountStrategy = discountStrategy;
        }

        public decimal Calculate(Visit visit)
        {
            if (canDiscountStrategy.CanDiscount(visit))
            {
                return discountStrategy.Discount(visit);
            }
            else
            {
                return visit.Amount;
            }
        }
    }

    class HappyHoursCanDiscountStrategy : ICanDiscountStrategy
    {
        private readonly Range<TimeSpan> period;

        public HappyHoursCanDiscountStrategy(Range<TimeSpan> period)
        {
            this.period = period;
        }

        public bool CanDiscount(Visit visit)
        {
            return visit.VisitDate.TimeOfDay >= period.From
               && visit.VisitDate.TimeOfDay <= period.To;
        }
    }

    class GenderCanDiscountStrategy : ICanDiscountStrategy
    {
        private readonly Gender gender;

        public GenderCanDiscountStrategy(Gender gender)
        {
            this.gender = gender;
        }

        public bool CanDiscount(Visit visit)
        {
            return visit.Patient.Gender == gender;
        }
    }

    class FixedDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal discount;

        public FixedDiscountStrategy(decimal discount)
        {
            this.discount = discount;
        }

        public decimal Discount(Visit visit)
        {
            return visit.Amount - discount;
        }
    }

    class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal percentage;

        public PercentageDiscountStrategy(decimal percentage)
        {
            this.percentage = percentage;
        }

        public decimal Discount(Visit visit)
        {
            return visit.Amount * percentage;
        }
    }
}
