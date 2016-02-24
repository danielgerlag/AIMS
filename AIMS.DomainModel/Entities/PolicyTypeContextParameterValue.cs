using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyTypeContextParameterValue : ContextParameterValue
    { 
        public int PolicyTypeID { get; set; }
        public virtual PolicyType PolicyType { get; set; }
                
    }
}
