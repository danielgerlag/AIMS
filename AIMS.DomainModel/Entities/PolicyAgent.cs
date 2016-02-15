using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyAgent : BaseEntity
    {

        public int PolicyID { get; set; }
        public virtual Policy Policy { get; set; }

        public int AgentTypeID { get; set; }
        public virtual AgentType AgentType { get; set; }

        public int AgentID { get; set; }
        public virtual Agent Agent { get; set; }

        public decimal Percentage { get; set; }


    }
}
