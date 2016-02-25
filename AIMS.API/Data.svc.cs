//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Models;
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
using AIMS.API;

namespace AIMS.DomainModel
{
    public class WCFDataContext : DataContext
    {
        public WCFDataContext()
            : base(AIMS.Services.IoC.Container.Resolve<IIndexQueue>(), AIMS.Services.IoC.Container.Resolve<IIndexRegister>())
        {
            //hack to get DI to work properly for WCF
            //hack to workaround WCF bug in .net 4  - https://social.msdn.microsoft.com/Forums/en-US/4025b688-2e46-4a08-998c-228c6a11d659/adonet-c-poco-entity-generator-and-data-services?forum=adodotnetdataservices
            //Configuration.ProxyCreationEnabled = false;
        }
    }

    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Data : EntityFrameworkDataService<WCFDataContext>
    {
        //System.Data.Services.Providers.ServiceAction
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
            //config.DataServiceBehavior.
        }

        private ISearchService _searchService = AIMS.Services.IoC.Container.Resolve<ISearchService>();


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

        [WebGet]
        public IQueryable<Policy> SearchPolicies(string query)
        {
            var task = Task.Factory.StartNew<List<Policy>>(new Func<List<Policy>>(() =>
            {
                List<Policy> rawResult = new List<Policy>();
                rawResult.AddRange(_searchService.SearchEntity<Policy>(query, CurrentDataSource, new string[] { }, null));
                return rawResult;
            }));

            return task.Result.AsQueryable();
        }

        [WebGet]
        public IQueryable<JournalTemplate> GetAvailableJournalTemplates(string origin, int reportingEntityID)
        {
            return CurrentDataSource.JournalTemplates.Where(x => x.TransactionOrigin == origin && x.ReportingEntityProfile.ReportingEntities.Any(y => y.ID == reportingEntityID));
        }

        [WebGet]
        public IQueryable<LedgerAccountBalance> GetLedgerAccountBalances(string source, int id, string effectiveDate)
        {
            DateTime effDate = DateTime.Parse(effectiveDate);
            var query = CurrentDataSource.LedgerTxns.Where(x => x.TxnDate <= effDate);

            switch (source)
            {
                case "ReportingEntity":
                    query = query.Where(x => x.ReportingEntityID == id);
                    return query
                            .GroupBy(x => new { x.ReportingEntity, x.LedgerAccount })
                            .Select(x => new
                            {
                                Balance = x.Sum(t => t.Amount),
                                LedgerAccountID = x.Key.LedgerAccount.ID,
                                LedgerAccountName = x.Key.LedgerAccount.Name,
                                ReportingEntityID = x.Key.ReportingEntity.ID,
                                ReportingEntityName = x.Key.ReportingEntity.Public.Name
                            }).ToList()
                            .Select(x => new LedgerAccountBalance()
                            {
                                Balance = x.Balance,
                                LedgerAccountID = x.LedgerAccountID,
                                LedgerAccountName = x.LedgerAccountName,
                                ReportingEntityID = x.ReportingEntityID,
                                ReportingEntityName = x.ReportingEntityName,
                                EffectiveDate = effDate
                            }).AsQueryable();

                case "Policy":
                    query = query.Where(x => x.PolicyID == id);
                    break;
                case "Public":
                    query = query.Where(x => x.PublicID == id);
                    break;
                default:
                    return new List<LedgerAccountBalance>().AsQueryable();
            }

            return query
                .GroupBy(x => new { x.ReportingEntity, x.LedgerAccount, x.Policy, x.Public })
                .Select(x => new
                {
                    Balance = x.Sum(t => t.Amount),
                    LedgerAccountID = x.Key.LedgerAccount.ID,
                    LedgerAccountName = x.Key.LedgerAccount.Name,
                    ReportingEntityID = x.Key.ReportingEntity.ID,
                    ReportingEntityName = x.Key.ReportingEntity.Public.Name,
                    Policy = x.Key.Policy,
                    Public = x.Key.Public
                }).ToList()
                .Select(x => new LedgerAccountBalance(x.Public, x.Policy)
                {
                    Balance = x.Balance,
                    LedgerAccountID = x.LedgerAccountID,
                    LedgerAccountName = x.LedgerAccountName,
                    ReportingEntityID = x.ReportingEntityID,
                    ReportingEntityName = x.ReportingEntityName,
                    EffectiveDate = effDate
                }).AsQueryable();

        }




        [WebGet]
        public IQueryable<LedgerAccountBalance> GetDebtorCreditorBalances(string source, int id, string effectiveDate, bool isDebtor, bool isCreditor)
        {
            DateTime effDate = DateTime.Parse(effectiveDate);
            var query = CurrentDataSource.LedgerTxns.Where(x => x.TxnDate <= effDate && x.LedgerAccount.LedgerAccountType.IsDebtor == isDebtor && x.LedgerAccount.LedgerAccountType.IsCredior == isCreditor);

            switch (source)
            {
                case "ReportingEntity":
                    query = query.Where(x => x.ReportingEntityID == id);
                    break;
                case "Policy":
                    query = query.Where(x => x.PolicyID == id);
                    break;
                case "Public":
                    query = query.Where(x => x.PublicID == id);
                    break;
                default:
                    return new List<LedgerAccountBalance>().AsQueryable();
            }

            return query
                .GroupBy(x => new { x.ReportingEntity, x.LedgerAccount, x.Public })
                .Select(x => new
                {
                    Balance = x.Sum(t => t.Amount),
                    LedgerAccountID = x.Key.LedgerAccount.ID,
                    LedgerAccountName = x.Key.LedgerAccount.Name,
                    ReportingEntityID = x.Key.ReportingEntity.ID,
                    ReportingEntityName = x.Key.ReportingEntity.Public.Name,
                    Public = x.Key.Public
                }).ToList()
                .Select(x => new LedgerAccountBalance(x.Public)
                {
                    Balance = x.Balance,
                    LedgerAccountID = x.LedgerAccountID,
                    LedgerAccountName = x.LedgerAccountName,
                    ReportingEntityID = x.ReportingEntityID,
                    ReportingEntityName = x.ReportingEntityName,
                    EffectiveDate = effDate
                }).AsQueryable();

        }


               

        [WebGet]
        public IQueryable<LedgerTxnBalance> GetLedgerTxnBalances(int reportingEntityID, int ledgerAccountID, int? publicID, int? policyID, string effectiveDate)
        {
            DateTime effDate = DateTime.Parse(effectiveDate);
            var query = CurrentDataSource.LedgerTxns.Where(x => x.ReportingEntityID == reportingEntityID && x.LedgerAccountID == ledgerAccountID && x.TxnDate <= effDate);

            if (publicID.HasValue)
                query = query.Where(x => x.PublicID == publicID);

            if (policyID.HasValue)
                query = query.Where(x => x.PolicyID == policyID);


            var sumQuery = query
                .OrderByDescending(x => x.TxnDate).ThenByDescending(x => x.ID)
                .Select(x => new
                {
                    Amount = x.Amount,
                    Description = x.JournalTxn.Description,
                    Reference = x.JournalTxn.Journal.Reference,
                    TxnDate = x.TxnDate,
                    Policy = x.JournalTxn.Journal.Policy,
                    Balance = query.Where(y => y.TxnDate <= x.TxnDate && y.ID <= x.ID).Sum(y => y.Amount)
                });


            return sumQuery.ToList()
                .Select(x => new LedgerTxnBalance(x.Policy)
                {
                    Balance = x.Balance,
                    Amount = x.Amount,
                    Description = x.Description,
                    Reference = x.Reference,
                    TxnDate = x.TxnDate
                }).AsQueryable();

        }


    }

}
