using System.Web;
using StructureMap.Web.Pipeline;

namespace Karin.Web.DependencyResolution
{
    /// <summary>
    /// 
    /// </summary>
    public class StructureMapScopeModule : IHttpModule
    {
        #region Public Methods and Operators
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) => StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
            context.EndRequest += (sender, e) =>
            {
                HttpContextLifecycle.DisposeAndClearAll();
                StructuremapMvc.StructureMapDependencyScope.DisposeNestedContainer();
            };
        }

        #endregion
    }
}