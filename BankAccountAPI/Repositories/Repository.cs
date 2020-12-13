using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BankAccountAPI.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> Entities;

        public Repository(DbContext context)
        {
            Context = context;
            Entities = Context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            Entities.Add(entity);
            Context.SaveChanges(true);
        }

        public TEntity Get(int id)
        {
            return Entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        public void Remove(int id)
        {
            var entity = Entities.Find(id);
            Entities.Remove(entity);
            Context.SaveChanges(true);
        }

        public void Update(TEntity entity)
        {
            Entities.Update(entity);
            Context.SaveChanges(true);
        }
    }
}
