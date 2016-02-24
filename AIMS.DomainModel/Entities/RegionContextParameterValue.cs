using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class RegionContextParameterValue : ContextParameterValue
    { 
        public int RegionID { get; set; }
        public virtual Region Region { get; set; }
                
    }
}
