using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Karin.DataAccess.Interface;
using Karin.Domain.Inherit;

namespace Karin.DomainService
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TEntity> _entities;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        protected BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _entities = unitOfWork.DbSet<TEntity>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? _entities : _entities.Where(predicate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includePaths"></param>
        /// <returns></returns>
        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate = null, List<string> includePaths = null)
        {
            var query = _entities.AsNoTracking();
            if (includePaths != null)
            {
                foreach (var include in includePaths)
                {
                    query = query.Include(include);
                }
            }
            return predicate == null ? query.First() : query.Where(predicate).First();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            _entities.Attach(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            _entities.Attach(entity);
            _unitOfWork.Entry(entity).State = EntityState.Modified;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            _unitOfWork.SaveChanges();
        }
    }
}
