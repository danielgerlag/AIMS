using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Autofac;
using Autofac.Core;
using AIMS.DomainModel.Abstractions.Intercepts;
using AIMS.DistributedServices.Infrastructure;

namespace AIMS.Services.TransactionProcessing
{
    public static class ServiceCollectionExtensions
    {
        public static Autofac.ContainerBuilder AddTransactionProcessingServices(this Autofac.ContainerBuilder services)
        {
            services.RegisterType<LockService>()
                .As<ILockService>()
                .WithParameter("connectionString", ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            services.RegisterType<TransactionTriggers.TransactionTriggerWorker>()
                .Keyed<IWorker>("TransactionTriggerWorker")
                .InstancePerDependency();

            services.RegisterType<TransactionTriggers.TransactionTriggerPool>()
                .Keyed<IWorkerPool>("TransactionTrigger")
                .WithParameter("threadCount", 1)
                .WithParameter("timeOut", TimeSpan.FromSeconds(5))
                .SingleInstance();

            //services.RegisterType<SSBQueueManager>()
            //    .Keyed<IQueueManager>("TransactionTriggerQueue")
            //    .WithParameter("connectionString", ConfigurationManager.ConnectionStrings["DB"].ConnectionString)
            //    .WithParameter("queueName", "QTransactionTrigger")
            //    .WithParameter("receiveService", "RX_TransactionTrigger")
            //    .SingleInstance();

            services.RegisterType<MemoryQueue>()
                .Keyed<IQueueManager>("TransactionTriggerQueue")
                .SingleInstance();

            return services;
        }
        
    }
}
