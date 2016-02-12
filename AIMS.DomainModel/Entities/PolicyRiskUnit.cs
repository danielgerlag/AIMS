using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyRiskUnit : BaseEntity
    {

        [Index]
        public int PolicyRiskLocationID { get; set; }
        public virtual PolicyRiskLocation PolicyRiskLocation { get; set; }

        [Index]
        public int InsurableAssetID { get; set; }
        public virtual InsurableItem InsurableAsset { get; set; }




    }
}
