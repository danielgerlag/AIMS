using AIMS.DomainModel.Entities;
using AIMS.Services.Indexer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Indexers
{
    public class PolicyIndexer : EntityIndexer<Policy>
    {
        protected override IEnumerable<SearchKeyword> BuildKeyWords(Policy entity)
        {
            List<SearchKeyword> result = new List<SearchKeyword>();                        

            result.Add(new SearchKeyword(entity.PolicyNumber));

            foreach (var holder in entity.PolicyHolders)
            {
                if (holder.Public != null)
                {
                    result.Add(new SearchKeyword(holder.Public.Name, typeof(Public), holder.PublicID));
                    result.Add(new SearchKeyword(holder.Public.FirstName, typeof(Public), holder.PublicID));
                }
            }

            foreach (var addr in entity.RiskLocations)
            {
                result.Add(new SearchKeyword(addr.StreetAddressLine1, typeof(PolicyRiskLocation), addr.ID));
            }

            foreach (var item in entity.InsurableItems)
            {
                foreach (var group in item.InsurableItemClass.Groups)
                {
                    foreach (var classAttr in group.Attributes.Where(x => x.IndexField))
                    {
                        var indexerAttrs = item.Attributes.Where(x => x.InsurableItemClassAttributeID == classAttr.ID).ToList();
                        foreach (var toIndex in indexerAttrs)
                        {
                            result.Add(new SearchKeyword(toIndex.Value, typeof(InsurableItemAttribute), toIndex.ID));                            
                        }
                    }
                }
            }


            return result;
        }

        protected override SearchResult BuildSearchResult(Policy entity)
        {
            SearchResult result = new SearchResult();

            result.EntityType = "Policy";
            result.Entity = entity;
            result.ID = entity.ID;
            result.Reference = entity.PolicyNumber;

            result.Name = "";

            foreach (var holder in entity.PolicyHolders)
            {
                if (holder.Public != null)
                {
                    result.Name += holder.Public.GetLookupText() + " ";
                }
            }

            result.Summary = "";

            if (entity.PolicySubType != null)
            {
                if (entity.PolicySubType.Region != null)
                    result.Summary += entity.PolicySubType.Region.Name + " - ";

                if (entity.PolicySubType.PolicyType != null)
                    result.Summary += entity.PolicySubType.PolicyType.Name + " - ";

                result.Summary += entity.PolicySubType.Name;
            }

            return result;
        }
    }
}
