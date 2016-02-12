using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class Operator : BaseEntity
    {
        [Index]
        public int InsurablePartyID { get; set; }
        public virtual InsurableParty InsurableParty { get; set; }

        [Index]
        public int OperatorPartyID { get; set; }
        public virtual Party OperatorParty { get; set; }

    }
}
