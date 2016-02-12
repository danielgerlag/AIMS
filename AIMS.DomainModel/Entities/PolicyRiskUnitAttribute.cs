using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyRiskUnitAttribute : BaseEntity
    {

        public PolicyRiskUnitAttribute()
        {
        }

        [Index]
        public int PolicyRiskUnitID { get; set; }
        public virtual PolicyRiskUnit PolicyRiskUnit { get; set; }

        [Index]
        public int InsurableAssetClassAttributeID { get; set; }
        public virtual InsurableItemClassAttribute InsurableAssetClassAttribute { get; set; }

        [StringLength(200)]
        public string Value { get; set; }

    }
}
