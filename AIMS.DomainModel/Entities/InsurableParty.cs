using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class InsurableParty : BaseEntity
    {
        public InsurableParty()
        {            
        }

        public int PartyID { get; set; }
        public virtual Party Party { get; set; }

        public int BrokerageBranchID { get; set; }
        public virtual BrokerageBranch BrokerageBranch { get; set; }
                

    }

}
