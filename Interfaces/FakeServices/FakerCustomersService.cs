using Interfaces.Fakers;
using Interfaces.IServices;
using Interfaces.Models;

namespace Interfaces.FakeServices
{
    public class FakerCustomersService : FakerEntitiesService<Customer>, ICustomersService
    {
        public FakerCustomersService(CustomerFaker entityFaker) 
            : base(entityFaker)
        {
        }
    }
}
