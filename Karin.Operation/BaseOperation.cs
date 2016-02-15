using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Karin.Domain.Inherit;
using Karin.DomainService;
using Karin.Models;

namespace Karin.Operation
{
    public abstract class BaseOperation<TEntity, TModel> : IBaseOperation<TEntity, TModel>
    where TEntity : BaseEntity
    where TModel : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IBaseService<TEntity> BaseService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseService"></param>
        protected BaseOperation(IBaseService<TEntity> baseService)
        {
            BaseService = baseService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TModel> GetAll()
        {
            return BaseService.GetAll().ProjectTo<TModel>().ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TModel Add(TModel model)
        {
            var entity = AutoMapper.Mapper.Map<TEntity>(model);
            BaseService.Add(entity);
            BaseService.Save();
            return AutoMapper.Mapper.Map<TModel>(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TModel Update(TModel model)
        {
            var entity = AutoMapper.Mapper.Map<TEntity>(model);
            BaseService.Update(entity);
            BaseService.Save();
            return AutoMapper.Mapper.Map<TModel>(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void Delete(TModel model)
        {
            var entity = AutoMapper.Mapper.Map<TEntity>(model);
            BaseService.Delete(entity);
            BaseService.Save();
        }
    }
}
