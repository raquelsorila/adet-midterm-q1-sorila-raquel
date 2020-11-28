using System;
using System.Linq;
using app.domain;

namespace app.repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(Guid id);
        IQueryable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void SaveChanges();

    }
}
