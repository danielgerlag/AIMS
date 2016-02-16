using AIMS.API.Models.Search;
using AIMS.DomainModel.Context;
using AIMS.Services.Indexer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace AIMS.API
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Search
    {

        private ISearchService _searchService;
        private IDataContext db;

        public Search()
        {
            _searchService = AIMS.Services.IoC.Container.Resolve<ISearchService>();
            db = AIMS.Services.IoC.Container.Resolve<IDataContext>();
        }


        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        public SearchResponse DoSearch(SearchRequest request)
        {
            List<SearchResult> data = _searchService.Search(request.SearchQuery, request.SearchType, "AIMS.DomainModel.Entities", db);
            SearchResponse result = new SearchResponse();

            result.Results = data.Select(x => new SearchResponseLine()
            {
                EntityType = x.EntityType,
                ID = x.ID,
                Name = x.Name,
                Number = x.Number,
                Reference = x.Reference,
                Summary = x.Summary
            }).ToList();

            return result;
        }

        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        [OperationContract]
        public SuggestionResponse Suggest(SearchRequest request)
        {
            SuggestionResponse result = new SuggestionResponse();
            result.Suggestions = _searchService.GetSearchSuggestions(request.SearchQuery);
            return result;
        }

    }
}
