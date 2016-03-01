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
            services.RegisterType<SequenceNumberGeneator>().As<ISequenceNumberGeneator>().InstancePerDependency();
            services.RegisterType<JournalPoster>().As<IJournalPoster>().InstancePerDependency();
            services.RegisterType<ContextParameterResolver>().As<IContextParameterResolver>().InstancePerDependency();
            services.RegisterType<PolicyRater>().As<IPolicyRater>().InstancePerDependency();
            services.RegisterType<PolicyTransitionManager>().As<IPolicyTransitionManager>().InstancePerDependency();
            services.RegisterType<PolicyCommandBuilder>().As<IPolicyCommandBuilder>().InstancePerDependency();

            services.RegisterType<TransactionPollManager>().As<ITransactionPollManager>().SingleInstance();

            return services;
        }

        public static Autofac.ContainerBuilder AddDomainModelIntercepts(this Autofac.ContainerBuilder services)
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
