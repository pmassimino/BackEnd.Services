using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services.Core
{
    public interface IRepository<TEntity, Tid> where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; set; }
        TEntity GetOne(Tid id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        void Delete(Tid id);
        TEntity Update(Tid id,TEntity entity);
    }
    public class RepositoryBase<TEntity, Tid> : IRepository<TEntity, Tid> where TEntity : class
    {
        public IUnitOfWork UnitOfWork { get; set ; }
        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public TEntity Add(TEntity entity)
        {
            var result = UnitOfWork.Context.Set<TEntity>().Add(entity).Entity;
            return result;
        }

        public void Delete(Tid id)
        {
            TEntity existing = this.GetOne(id);
            if (existing != null) UnitOfWork.Context.Set<TEntity>().Remove(existing);
        }
        public TEntity GetOne(Tid id)
        {
            return UnitOfWork.Context.Set<TEntity>().Find(id);           
        }
        public IQueryable<TEntity> GetAll()
        {
            //return _unitOfWork.Context.Set<TEntity>().AsEnumerable<TEntity>();
            return UnitOfWork.Context.Set<TEntity>().AsQueryable<TEntity>();
        }

        public IQueryable<TEntity> GetAll(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return UnitOfWork.Context.Set<TEntity>().Where(predicate).AsQueryable<TEntity>();
        }

        public TEntity Update(Tid id,TEntity entity)
        {
            TEntity existing = UnitOfWork.Context.Set<TEntity>().Find(id);
            UnitOfWork.Context.Entry(existing).CurrentValues.SetValues(entity);
            return existing;
            
        }
    }
}
