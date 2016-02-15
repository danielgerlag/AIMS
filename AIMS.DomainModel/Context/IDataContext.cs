using System.Data.Entity;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Interface;

namespace AIMS.DomainModel.Context
{
    public interface IDataContext : IDbContext
    {
        //DbSet<DataArea> DataAreas { get; set; }

        DbSet<AttributeDataType> AttributeDataTypes { get; set; }
        DbSet<AttributeLookupItem> AttributeLookupItems { get; set; }
        DbSet<AttributeLookupList> AttributeLookupLists { get; set; }
        DbSet<Entities.AttributeLookupSubList> AttributeLookupSubLists { get; set; }

        DbSet<Brokerage> Brokerages { get; set; }
        DbSet<ReportingEntityBranch> BrokerageBranches { get; set; }        
        DbSet<Broker> Brokers { get; set; }
        DbSet<BrokerDataArea> BrokerDataAreas { get; set; }

        DbSet<BusinessLine> BusinessLines { get; set; }

        DbSet<InsurableItemClassAttribute> InsurableItemClassAttributes { get; set; }
        DbSet<InsurableItemClass> InsurableItemClasses { get; set; }
        DbSet<InsurableItem> InsurableItems { get; set; }
        DbSet<InsurableParty> InsurableParties { get; set; }

        DbSet<Operator> Operators { get; set; }
        DbSet<OperatorType> OperatorTypes { get; set; }
        DbSet<OperatorTypeAttribute> OperatorTypeAttributes { get; set; }

        DbSet<Party> Parties { get; set; }
        DbSet<ContactDetail> PartyContactDetails { get; set; }

        DbSet<Policy> Policies { get; set; }
        DbSet<PolicyRiskLocation> PolicyRiskLocations { get; set; }
        DbSet<PolicyRiskUnit> PolicyRiskUnits { get; set; }
        DbSet<PolicyRiskUnitAttribute> PolicyRiskUnitAttributes { get; set; }
        DbSet<PolicyRiskUnitOperator> PolicyRiskUnitOperators { get; set; }
        DbSet<PolicyStatus> PolicyStatuses { get; set; }

        DbSet<RiskLocation> RiskLocations { get; set; }

        
        
    }
}