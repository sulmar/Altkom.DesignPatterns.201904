using Interfaces.IServices;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DbServices
{
    public class DbPersonsService : IPersonsService
    {
        public void Add(Person entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        // DbContext 

        public IEnumerable<Person> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> Get(string city)
        {
            throw new NotImplementedException();
        }

        public Person Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Person entity)
        {
            throw new NotImplementedException();
        }
    }
}
