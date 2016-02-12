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

        [Index]
        public int InsurablePartyID { get; set; }
        public virtual InsurableParty InsurableParty { get; set; }


        [Required]
        [StringLength(100)]
        public string StreetAddressLine1 { get; set; }
                
        [StringLength(100)]
        public string StreetAddressLine2 { get; set; }


        //public virtual ICollection<PartyContactDetail> ContactDetails { get; set; }



        public override string GetLookupText()
        {
            return StreetAddressLine1;
        }


    }
}
