using System;
using System.Linq;

namespace ReciPiBook.Repository
{
    public interface IRepository<T> : IDisposable
    {
        T Get<TKey>(TKey id);
        IQueryable<T> GetAll();
        T Add(T entity);
        void Update(T entity);
        void Delete<TKey>(TKey id);
    }
}
