using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AIMS.API.Models.Search
{
    [DataContract]
    public class SearchResponse
    {
        public SearchResponse()
        {
            Results = new List<SearchResponseLine>();
        }

        [DataMember]
        public List<SearchResponseLine> Results { get; set; }
    }

    [DataContract]
    public class SearchResponseLine
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string EntityType { get; set; }

        [DataMember]
        public string Reference { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Number { get; set; }

        [DataMember]
        public string Summary { get; set; }
    }
}