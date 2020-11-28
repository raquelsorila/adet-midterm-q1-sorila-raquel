using System;
using System.Linq;
using app.domain;
using Microsoft.EntityFrameworkCore;

namespace app.repository
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MovieDbContext _context;
        private readonly DbSet<T> entities;
        public GenericRepository(MovieDbContext context)
        {
            this._context = context;
            entities = _context.Set<T>();
        }
        public void Create(T entity)
        {
            entities.Add(entity);
        }

        public T Get(Guid id) => entities.Find(id);

        public IQueryable<T> GetAll() => entities;

        public void SaveChanges() => _context.SaveChanges();

        public void Update(T entity)
        {
            entities.Update(entity);
        }
    }
}