using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ReciPiBook.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        protected DbSet<T> DbSet;
        private bool _disposed = false;

        public Repository(IInfrastructure<IServiceProvider> context)
        {
            Context = context as DbContext;
            DbSet = Context.Set<T>();
        }

        public T Add(T entity)
        {
            Context.Set<T>().Add(entity);
            Save();
            return entity;
        }

        public T Get<TKey>(TKey id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public void Update(T entity)
        {
            Save();
        }

        public void Delete<TKey>(TKey id)
        {
            var toDelete = Get(id);
            Context.Remove(toDelete);
        }

        private void Save()
        {
            Context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if(disposing)
                Context.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
