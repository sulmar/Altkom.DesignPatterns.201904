using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("Marcin", "Sulecki", Gender.Male);
            person.Salary = 100;

            Visit visit = new Visit(person) { TotalAmount = 200 };

            Visit copyVisit = (Visit) visit.Clone();

            copyVisit.Patient.FirstName = "Bartek";



            // Person copyPerson = new Person(person.FirstName, person.LastName, person.Gender);

            Person copyPerson = (Person) person.Clone();

            copyPerson.FirstName = "Bartek";

            if (!ReferenceEquals(person, copyPerson))
            {
                Console.WriteLine("Rozne obiekty");
            }

        }
    }


    class Visit : ICloneable
    {
        public Visit(Person patient)
        {
            Patient = patient;
        }

        public decimal TotalAmount { get; set; }
        public Person Patient { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    class Person : ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Salary { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        protected Person()
        {
            Gender = Gender.Female;
        }

        public Person(string firstName, string lastName, Gender gender = Gender.Female)
            : this()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
        }

        public object Clone()
        {
            //Person copyPerson = new Person(this.FirstName, this.LastName, this.Gender)
            //{
            //    IsDeleted = this.IsDeleted
            //};

            return this.MemberwiseClone();

        }
    }

    enum Gender
    {
        Female,
        Male
    }
}
