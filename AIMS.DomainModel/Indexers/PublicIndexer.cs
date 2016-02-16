using AIMS.DomainModel.Entities;
using AIMS.Services.Indexer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Indexers
{
    public class PublicIndexer : EntityIndexer<Public>
    {
        protected override IEnumerable<SearchKeyword> BuildKeyWords(Public entity)
        {
            List<SearchKeyword> result = new List<SearchKeyword>();

            result.Add(new SearchKeyword(entity.Name));
            result.Add(new SearchKeyword(entity.FirstName));

            return result;
        }

        protected override SearchResult BuildSearchResult(Public entity)
        {
            SearchResult result = new SearchResult();

            result.EntityType = "Public";
            result.Entity = entity;
            result.ID = entity.ID;

            switch (entity.PartyType)
            {
                case "I":
                    result.Name = entity.Name + ", " + entity.FirstName;
                    break;
                default:
                    result.Name = entity.Name;
                    break;
            }           

            return result;
        }
    }
}
