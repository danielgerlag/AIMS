using AIMS.DomainModel.Context;
using AIMS.Services.Indexer;
using AIMS.Services.Indexer.Interface;
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

            var indexWorker = Services.IoC.Container.Resolve<IIndexWorker>();
            indexWorker.Start();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            var indexWorker = Services.IoC.Container.Resolve<IIndexWorker>();
            indexWorker.Stop();
        }

        public void ConfigureIoC()
        {
            Autofac.ContainerBuilder builder = new Autofac.ContainerBuilder();
            
            builder.RegisterType<DataContext>().As<IDataContext>();
            builder.AddSearchIndexer();
            builder.RegisterSearchIndexers(typeof(DataContext).Assembly);            

            Services.IoC.Container.IOCContainer = builder.Build();
            //AutofacHostFactory.Container = Westland.Documents.IoC.Container.IOCContainer;
        }
    }
}
