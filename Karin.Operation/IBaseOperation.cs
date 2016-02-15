using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karin.Domain.Inherit;
using Karin.Models;

namespace Karin.Operation
{
    public interface IBaseOperation<TEntity, TModel>
        where TEntity : BaseEntity
        where TModel : BaseModel
    {
        List<TModel> GetAll();

        TModel Add(TModel model);

        TModel Update(TModel model);

        void Delete(TModel model);
    }
}
