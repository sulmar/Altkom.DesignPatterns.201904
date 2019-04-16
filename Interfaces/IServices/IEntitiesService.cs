using System.Collections.Generic;

namespace Interfaces.IServices
{
    public interface IEntitiesService<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }


}
