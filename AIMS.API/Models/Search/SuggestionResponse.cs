using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AIMS.API.Models.Search
{
    [DataContract]
    public class SuggestionResponse
    {
        public SuggestionResponse()
        {
            Suggestions = new List<string>();
        }

        [DataMember]
        public List<string> Suggestions { get; set; }
    }
}