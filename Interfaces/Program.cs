using Interfaces.DbServices;
using Interfaces.Fakers;
using Interfaces.FakeServices;
using Interfaces.IServices;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplaysCustomers();

            DisplaysPersons();

        }

        private static void DisplaysCustomers()
        {
            ICustomersService customersService = new FakerCustomersService(new CustomerFaker());
        }

        private static void DisplaysPersons()
        {
            IPersonsService personsService = new FakerPersonsService(new PersonFaker());
            // IPersonsService personsService = new FakePersonsService();

            var persons = personsService.Get();

            foreach (var person in persons)
            {
                Console.WriteLine(person);
            }

            //     personsService.DoWork();
        }
    }
        

}
