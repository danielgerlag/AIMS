using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using AIMS.Services.Indexer.Interface;
using AIMS.DomainModel.Abstractions.Intercepts;

namespace AIMS.DomainModel
{
    public static class ServiceCollectionExtensions
    {        

        public static Autofac.ContainerBuilder RegisterDomainModelIntercepts(this Autofac.ContainerBuilder services)
        {
            Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            foreach (var t in types.Where(x => x.IsClass && !x.IsAbstract).Where(x => x.GetInterfaces().Any(y => y == typeof(IEntityIntercept))))
            {
                services.RegisterType(t).SingleInstance();
                //services.AddTransient(t);
            }

            return services;
        }

    }
}
