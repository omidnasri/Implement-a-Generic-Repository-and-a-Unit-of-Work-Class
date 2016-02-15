using AutoMapper;
using Karin.Domain.Inherit;
using Karin.Models;

namespace Karin.Mapper
{
    public class BaseMapper<TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : BaseModel
    {
        private bool _registered;
        protected IMappingExpression<TEntity, TModel> EntityToModelMapper { get; private set; }
        protected IMappingExpression<TModel, TEntity> ModelToEntityMapper { get; private set; }

        public virtual void Initialize()
        {
            if (_registered) return;
            EntityToModelMapper = AutoMapper.Mapper.CreateMap<TEntity, TModel>();
            ModelToEntityMapper = AutoMapper.Mapper.CreateMap<TModel, TEntity>();
            _registered = true;
        }
    }
}
