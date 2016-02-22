using AIMS.DistributedServices.Infrastructure;
using AIMS.DomainModel;
using AIMS.DomainModel.Context;
using AIMS.Services.Indexer;
using AIMS.Services.Indexer.Interface;
using AIMS.Services.TransactionProcessing;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace AIMS.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            DataContext dc = new DataContext("name=DB");
            dc.AttributeDataTypes.ToList();  //temp hack

            ConfigureIoC();

            Services.IoC.Container.Resolve<IIndexWorker>().Start();
            Services.IoC.Container.ResolveKeyed<IWorkerPool>("TransactionTrigger").Start();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Services.IoC.Container.Resolve<IIndexWorker>().Stop();
            Services.IoC.Container.ResolveKeyed<IWorkerPool>("TransactionTrigger").Stop();
        }

        public void ConfigureIoC()
        {
            Autofac.ContainerBuilder builder = new Autofac.ContainerBuilder();
            
            builder.RegisterType<DataContext>().As<IDataContext>();
            builder.AddDomainServices();
            builder.AddDomainModelIntercepts();
            builder.AddSearchIndexerServices();
            builder.AddSearchIndexers(typeof(DataContext).Assembly);
            builder.AddTransactionProcessingServices();

            Services.IoC.Container.IOCContainer = builder.Build();
            //AutofacHostFactory.Container = Westland.Documents.IoC.Container.IOCContainer;
        }
    }
}
