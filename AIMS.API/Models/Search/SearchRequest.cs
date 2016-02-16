using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AIMS.API.Models.Search
{
    [DataContract]
    public class SearchRequest
    {
        [DataMember]
        public string SearchQuery { get; set; }

        [DataMember]
        public string SearchType { get; set; }
    }
}