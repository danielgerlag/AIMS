using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{    
    public class PolicyRiskLocation : BaseEntity
    {

        public PolicyRiskLocation()
        {
            //ContactDetails = new HashSet<PartyContactDetail>();
        }


        [Index]
        public int PolicyID { get; set; }
        public virtual Policy Policy { get; set; }

        [Index]
        public int RiskLocationID { get; set; }
        public virtual RiskLocation RiskLocation { get; set; }


    }

}
