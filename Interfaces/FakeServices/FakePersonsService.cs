using Interfaces.IServices;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.FakeServices
{

    public class FakePersonsService : IPersonsService
    {
        protected IList<Person> persons;

        public FakePersonsService()
        {
            persons = Generate().ToList();
        }

        protected virtual IEnumerable<Person> Generate()
        {
            return new List<Person>
            {
                new Person { Id = 1, FirstName = "John", LastName = "Smith"},
                new Person { Id = 2, FirstName = "Ann", LastName = "Smith"},
                new Person { Id = 3, FirstName = "Bart", LastName = "Spider"},
            };
        }

        public IEnumerable<Person> Get()
        {
            return persons;
        }

        public IEnumerable<Person> Get(string city)
        {
            throw new NotImplementedException();
        }

        public Person Get(int id)
        {
            return persons.SingleOrDefault(p => p.Id == id);
        }

        public void DoWork()
        {
            Console.WriteLine("Working...");
        }

        public void Add(Person entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Person entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
