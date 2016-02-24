using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyContextParameterValue : ContextParameterValue
    { 
        public int PolicyID { get; set; }
        public virtual Policy Policy { get; set; }
                
    }
}
