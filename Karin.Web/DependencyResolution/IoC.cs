// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Karin.Web.DependencyResolution
{
    using System.Reflection;
    using Karin.DataAccess.Context;
    using Karin.DataAccess.Interface;
    using Karin.DomainService;
    using Karin.Operation;
    using StructureMap;
    /// <summary>
    /// 
    /// </summary>
    public static class IoC
    {
        #region Initialize
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IContainer Initialize()
        {
            var container = new StructureMap.Container();
            container.Configure(cfg =>
            {
                cfg.Scan(s =>
                {
                    s.WithDefaultConventions();
                    s.Assembly(Assembly.GetAssembly(typeof(BaseOperation<,>)));
                    s.Assembly(Assembly.GetAssembly(typeof(BaseService<>)));
                });
                cfg.For<IKarinUnitOfWork>().Use<KarinContext>();
            });
            return container;
            //new Container(c => c.AddRegistry<DefaultRegistry>());
        }
        #endregion
    }
}