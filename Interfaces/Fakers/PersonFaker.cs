using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Fakers
{
    public class PersonFaker : Faker<Models.Person>
    {
        public PersonFaker()
        {
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.IsDeleted, f => f.Random.Bool(0.8f));
            RuleFor(p => p.Description, f => f.Lorem.Sentence());
            Ignore(p => p.Color);
        }
    }
}
