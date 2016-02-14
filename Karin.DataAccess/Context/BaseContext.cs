using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Karin.DataAccess.Interface;
using Karin.DataAccess.Migrations;
using Karin.Domain.Inherit;

namespace Karin.DataAccess.Context
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseContext : DbContext, IUnitOfWork
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public BaseContext(string connectionString)
            : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public BaseContext()
        {
            //this.Database.Log = data => System.Diagnostics.Debug.WriteLine(data);
            //فقط تعريف شده تا يك برك پوينت در اينجا قرار داده شود براي آزمايش تعداد بار فراخواني آن
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IDbSet<TEntity> DbSet<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return base.Entry(entity);
        }
    }
}
