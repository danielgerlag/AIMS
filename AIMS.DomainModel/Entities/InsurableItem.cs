using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class InsurableItem : BaseEntity
    {

        public InsurableItem()
        {
        
        }

        [Index]
        public int InsurablePartyID { get; set; }
        public virtual InsurableParty InsurableParty { get; set; }
        
        [Index]
        public int InsurableItemClassID { get; set; }
        public virtual InsurableItemClass InsurableItemClass { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(200)]
        public string SerialNo { get; set; }


        public override string GetLookupText()
        {
            return Description;
        }


    }
}
