using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyContextParameterValue : BaseEntity
    {
        public int ContextParameterValueID { get; set; }
        public virtual ContextParameterValue ContextParameterValue { get; set; }

        public int PolicyID { get; set; }
        public virtual Policy Policy { get; set; }
                
    }
}
