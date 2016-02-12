using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyRiskUnitOperator : BaseEntity
    {

        public PolicyRiskUnitOperator()
        {
        }

        [Index]
        public int PolicyRiskUnitID { get; set; }
        public virtual PolicyRiskUnit PolicyRiskUnit { get; set; }

        [Index]
        public int OperatorID { get; set; }
        public virtual Operator Operator { get; set; }
                
        public decimal? Percentage { get; set; }

    }
}
