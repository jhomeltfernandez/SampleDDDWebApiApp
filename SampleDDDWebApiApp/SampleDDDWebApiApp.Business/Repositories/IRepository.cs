using SampleDDDWebApiApp.Models.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDDDWebApiApp.Business.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Func<T, bool> predicate);
        T Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
