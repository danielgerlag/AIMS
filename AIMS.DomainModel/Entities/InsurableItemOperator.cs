using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class InsurableItemOperator : BaseEntity
    {        
        [Index]
        public int InsurableItemID { get; set; }
        public virtual InsurableItem InsurableItem { get; set; }        

        [Index]
        public int OperatorID { get; set; }
        public virtual Operator Operator { get; set; }

        public bool Primary { get; set; }

                
    }
}
