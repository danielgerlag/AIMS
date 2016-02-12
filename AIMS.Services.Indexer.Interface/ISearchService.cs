using AIMS.DomainModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.Services.Indexer.Interface
{
    public interface ISearchService
    {
        List<SearchResult> Search(string searchString, IDbContext dataContext, int? dataAreaID = default(int?));
        List<T> SearchEntity<T>(string searchString, IDbContext dataContext, IEnumerable<string> include, int? dataAreaID = null) where T : class, IDomainEntity;
        List<string> GetSearchSuggestions(string searchString, int? dataAreaID = null);
    }
}
