//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using AIMS.Services.Indexer.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;
using System.Linq;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using System.Web;

namespace AIMS.API
{
    public class WCFDataContext : DataContext
    {
        public WCFDataContext()
            : base(Services.IoC.Container.Resolve<IIndexQueue>(), Services.IoC.Container.Resolve<IIndexRegister>())
        {
            //hack to get DI to work properly for WCF
            //hack to workaround WCF bug in .net 4  - https://social.msdn.microsoft.com/Forums/en-US/4025b688-2e46-4a08-998c-228c6a11d659/adonet-c-poco-entity-generator-and-data-services?forum=adodotnetdataservices
            //Configuration.ProxyCreationEnabled = false;
        }
    }

    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Data : EntityFrameworkDataService<WCFDataContext>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            config.UseVerboseErrors = true;
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.SetServiceActionAccessRule("*", ServiceActionRights.Invoke);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;            
        }

        private ISearchService _searchService = Services.IoC.Container.Resolve<ISearchService>();


        [WebGet]
        public string GetLookupText(string set, int id)
        {
            DbSet dbset = null;

            switch (set)
            {
                case "Publics":
                    dbset = CurrentDataSource.Publics;
                    break;
                case "ServiceProviderTypes":
                    dbset = CurrentDataSource.ServiceProviderTypes;
                    break;
            }

            object entity = dbset.Find(id);
            if (entity is BaseEntity)
                return (entity as BaseEntity).GetLookupText();
            return string.Empty;
        }

        [WebGet]
        public IQueryable<Public> SearchPublics(string query)
        {            
            var task = Task.Factory.StartNew<List<Public>>(new Func<List<Public>>(() =>
            {
                List<Public> rawResult = new List<Public>();
                rawResult.AddRange(_searchService.SearchEntity<Public>(query, CurrentDataSource, new string[] { }, null));
                return rawResult;
            }));            

            return task.Result.AsQueryable();
        }

    }
        
}
