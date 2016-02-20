using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using AIMS.Services.Indexer.Interface;
using AIMS.DomainModel.Abstractions.Intercepts;
using AIMS.DomainModel.Services;

namespace AIMS.DomainModel
{
    public static class ServiceCollectionExtensions
    {
        public static Autofac.ContainerBuilder AddDomainServices(this Autofac.ContainerBuilder services)
        {

            services.RegisterType<JournalGenerator>().As<IJournalGenerator>().InstancePerDependency();
            
            return services;
        }

        public static Autofac.ContainerBuilder RegisterDomainModelIntercepts(this Autofac.ContainerBuilder services)
        {
            Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            foreach (var t in types.Where(x => x.IsClass && !x.IsAbstract).Where(x => x.GetInterfaces().Any(y => y == typeof(IEntityIntercept))))
            {
                services.RegisterType(t).InstancePerDependency();
                //services.AddTransient(t);
            }

            return services;
        }

    }
}
