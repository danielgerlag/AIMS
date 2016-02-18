using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicySubTypeCoverage : BaseEntity
    {           

        public int PolicySubTypeID { get; set; }
        public virtual PolicySubType PolicySubType { get; set; }
                
        public int CoverageTypeID { get; set; }
        public virtual CoverageType CoverageType { get; set; }

        
    }
}
