using Microsoft.EntityFrameworkCore;
using SampleDDDWebApiApp.DataAccess.DataContext;
using SampleDDDWebApiApp.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleDDDWebApiApp.Business.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext context;
        private DbSet<T> entities;

        public Repository(AppDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            return entities.Where(predicate).AsEnumerable();
        }

        public T Get(int id) => entities.SingleOrDefault(s => s.Id == id);

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
