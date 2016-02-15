using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{    
    
    public class RiskLocation : BaseEntity
    {

        public RiskLocation()
        {
            //ContactDetails = new HashSet<PartyContactDetail>();
        }

        

        [Required]
        [StringLength(200)]
        public string StreetAddressLine1 { get; set; }
                
        [StringLength(200)]
        public string StreetAddressLine2 { get; set; }

        [StringLength(200)]
        public string City { get; set; }

        [StringLength(20)]
        public string PostalCode { get; set; }
        


        public override string GetLookupText()
        {
            return StreetAddressLine1;
        }


    }
}
