using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.IServices
{
    public interface IPersonsService : IEntitiesService<Person>
    {
        IEnumerable<Person> Get(string city);
    }


}
