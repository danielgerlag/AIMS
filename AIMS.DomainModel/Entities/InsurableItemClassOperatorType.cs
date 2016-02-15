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
    public class InsurableItemClassOperatorType : BaseEntity
    {        

        [Index]
        public int InsurableItemClassID { get; set; }
        public virtual InsurableItemClass InsurableItemClass { get; set; }
                
        public int? OperatorTypeID { get; set; }
        public virtual OperatorType OperatorType { get; set; }
        
    }
}
