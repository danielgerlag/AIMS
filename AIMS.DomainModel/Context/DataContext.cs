using AIMS.DomainModel.Context;
using AIMS.Services.Indexer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Abstractions;

namespace AIMS.DomainModel.Context
{
    public class DataContext : BaseContext, IDataContext
    {

        //public virtual DbSet<DataArea> DataAreas { get; set; }

        public virtual DbSet<AttributeDataType> AttributeDataTypes { get; set; }
        public virtual DbSet<AttributeLookupItem> AttributeLookupItems { get; set; }
        public virtual DbSet<AttributeLookupList> AttributeLookupLists { get; set; }
        public virtual DbSet<Entities.AttributeLookupSubList> AttributeLookupSubLists { get; set; }

        public virtual DbSet<Brokerage> Brokerages { get; set; }
        public virtual DbSet<BrokerageBranch> BrokerageBranches { get; set; }
        public virtual DbSet<Broker> Brokers { get; set; }
        public virtual DbSet<BrokerDataArea> BrokerDataAreas { get; set; }

        public virtual DbSet<BusinessLine> BusinessLines { get; set; }

        public virtual DbSet<InsurableItemClassAttribute> InsurableItemClassAttributes { get; set; }
        public virtual DbSet<InsurableItemClass> InsurableItemClasses { get; set; }
        public virtual DbSet<InsurableItem> InsurableItems { get; set; }
        public virtual DbSet<InsurableParty> InsurableParties { get; set; }

        public virtual DbSet<Operator> Operators { get; set; }
        public virtual DbSet<OperatorType> OperatorTypes { get; set; }
        public virtual DbSet<OperatorTypeAttribute> OperatorTypeAttributes { get; set; }

        public virtual DbSet<Party> Parties { get; set; }
        public virtual DbSet<PartyContactDetail> PartyContactDetails { get; set; }

        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<PolicyRiskLocation> PolicyRiskLocations { get; set; }
        public virtual DbSet<PolicyRiskUnit> PolicyRiskUnits { get; set; }
        public virtual DbSet<PolicyRiskUnitAttribute> PolicyRiskUnitAttributes { get; set; }
        public virtual DbSet<PolicyRiskUnitOperator> PolicyRiskUnitOperators { get; set; }
        public virtual DbSet<PolicyStatus> PolicyStatuses { get; set; }

        public virtual DbSet<RiskLocation> RiskLocations { get; set; }


        protected override Type GetInterfaceType()
        {
            return typeof(IDataContext); //TODO: interface
        }

        public DataContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<DataContext>(new DatabaseInit());
        }

        public DataContext(IIndexQueue indexQueue, IIndexRegister indexStoreInit)
            : base(indexQueue, indexStoreInit)
        {
            Database.SetInitializer<DataContext>(new DatabaseInit());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Entities.Party>().Ignore<bool>(x => x.Deleted);
            //modelBuilder.Entity<Entities.PartyContactDetail>().Ignore<bool>(x => x.Deleted);
        }

    }
}
