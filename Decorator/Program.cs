﻿using Decorator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
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


            var period = new Range<TimeSpan>(TimeSpan.FromHours(8.5), TimeSpan.FromHours(15.5));
            ICanDiscountStrategy canDiscountStrategy = new HappyHoursCanDiscountStrategy(period);
            IDiscountStrategy discountStrategy = new FixedDiscountStrategy(20);


            IVisitCalculator visitCalculator
                = new DiscountDecorator(
                                new HappyHoursCanDiscountStrategy(period),
                                new FixedDiscountStrategy(20),                         
                                    new DiscountDecorator(new GenderCanDiscountStrategy(Gender.Female),
                                        new PercentageDiscountStrategy(0.1m), 
                                        new VisitCalculator()));
            
            decimal totalAmount = visitCalculator.Calculate(visit);

            Console.WriteLine($"Total amount: {totalAmount:C2}");
        }
    }

    public interface ICanDiscountStrategy
    {
        bool CanDiscount(Visit visit);
    }

    public interface IDiscountStrategy
    {
        decimal Discount(Visit visit);
    }

    public interface IVisitCalculator
    {
        decimal Calculate(Visit visit);
    }

    public class VisitCalculator : IVisitCalculator
    {
        public decimal Calculate(Visit visit)
        {
            return visit.Amount;
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

    public class GenderCanDiscountStrategy : ICanDiscountStrategy
    {
        private readonly Gender gender;

        public GenderCanDiscountStrategy(Gender gender) => this.gender = gender;

        public bool CanDiscount(Visit visit) => visit.Patient.Gender == gender;
    }


    class FixedDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal discount;

        public FixedDiscountStrategy(decimal discount) => this.discount = discount;

        public decimal Discount(Visit visit) => discount;
    }

    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal percentage;

        public PercentageDiscountStrategy(decimal percentage) => this.percentage = percentage;

        public decimal Discount(Visit visit) => visit.Amount * percentage;
    }


    public class DiscountDecorator : IVisitCalculator
    {
        private readonly IVisitCalculator calculator;
        private readonly ICanDiscountStrategy canDiscountStrategy;
        private readonly IDiscountStrategy discountStrategy;

        public DiscountDecorator(ICanDiscountStrategy canDiscountStrategy, IDiscountStrategy discountStrategy, IVisitCalculator calculator)
        {
            this.calculator = calculator;
            this.canDiscountStrategy = canDiscountStrategy;
            this.discountStrategy = discountStrategy;
        }

        public decimal Calculate(Visit visit)
        {
            decimal discount = calculator.Calculate(visit);
            
            if (canDiscountStrategy.CanDiscount(visit))
            {
                discount -= discountStrategy.Discount(visit);
            }

            return discount;
        }
    }

    
}
