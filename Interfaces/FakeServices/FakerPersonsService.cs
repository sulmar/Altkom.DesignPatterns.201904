using Interfaces.Fakers;
using Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interfaces.FakeServices
{
    public class FakerPersonsService : IPersonsService
    {
        protected IList<Models.Person> persons;

        private PersonFaker personFaker;

        public FakerPersonsService(PersonFaker personFaker)
        {
            this.personFaker = personFaker;
            persons = personFaker.Generate(100);
        }


        public void Add(Models.Person entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Person> Get()
        {
            return persons;
        }

        public IEnumerable<Models.Person> Get(string city)
        {
            throw new NotImplementedException();
        }

        public Models.Person Get(int id)
        {
            return persons.SingleOrDefault(p => p.Id == id);
        }

        public void Update(Models.Person entity)
        {
            throw new NotImplementedException();
        }

      
    }
}
