using BilgeCinema.Data.Context;
using BilgeCinema.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BilgeCinema.Data.Repository
{
    public class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : MovieEntity
    {
        private readonly BilgeCinemaContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public InMemoryRepository(BilgeCinemaContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var entity = _dbSet.Find(Id);
            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
            _db.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is not null ? _dbSet.Where(predicate) : _dbSet;
        }

        public TEntity GetById(int Id)
        {
            return _dbSet.Find(Id);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _db.SaveChanges();
        }
    }
}
