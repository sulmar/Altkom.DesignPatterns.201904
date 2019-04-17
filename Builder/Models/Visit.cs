using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Models
{
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
