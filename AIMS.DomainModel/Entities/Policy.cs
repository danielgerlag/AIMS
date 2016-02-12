using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    [Table("Policy")]
    public class Policy : BaseEntity
    {

        public Policy()
        {
            //RiskLocations = new HashSet<PolicyRiskLocation>();
        }


        [Required]
        [StringLength(200)]
        public string PolicyNumber { get; set; }

        [Index]
        public int PolicyStatusID { get; set; }
        public virtual PolicyStatus PolicyStatus { get; set; }

        public int InsurablePartyID { get; set; }
        public virtual InsurableParty InsurableParty { get; set; }


        //public virtual ICollection<PolicyRiskLocation> RiskLocations { get; set; }



        public override string GetLookupText()
        {
            return PolicyNumber;
        }


    }

}
