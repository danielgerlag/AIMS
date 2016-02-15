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
    public class InsurableItemAttribute : BaseEntity
    {        
        
        public int InsurableItemID { get; set; }
        public virtual InsurableItem InsurableItem { get; set; }        

        [Index]
        public int InsurableItemClassAttributeID { get; set; }
        public virtual InsurableItemClassAttribute InsurableItemClassAttribute { get; set; }

        [MaxLength(500)]
        public string Value { get; set; }
                
    }
}
