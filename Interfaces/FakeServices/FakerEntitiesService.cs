using Bogus;
using Interfaces.IServices;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interfaces.FakeServices
{
    public class FakerEntitiesService<TEntity> : IEntitiesService<TEntity>
        where TEntity : Base
    {
        protected IList<TEntity> entities;

        private Faker<TEntity> entityFaker;

        public FakerEntitiesService(Faker<TEntity> entityFaker)
        {
            this.entityFaker = entityFaker;

            entities = entityFaker.Generate(100);
        }

        public void Add(TEntity entity)
        {
            entities.Add(entity);
        }

        public void Delete(int id)
        {
            TEntity entity = Get(id);

            entities.Remove(entity);
        }

        public IEnumerable<TEntity> Get()
        {
            return entities;
        }

        public TEntity Get(int id)
        {
            return entities.SingleOrDefault(p => p.Id == id);
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
