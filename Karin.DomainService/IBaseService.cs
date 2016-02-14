using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Karin.Domain.Inherit;

namespace Karin.DomainService
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includePaths"></param>
        /// <returns></returns>
        TEntity GetFirst(Expression<Func<TEntity, bool>> predicate = null, List<string> includePaths = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        void Save();
    }
}
