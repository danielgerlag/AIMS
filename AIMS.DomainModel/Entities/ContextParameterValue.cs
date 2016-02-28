﻿using AIMS.DomainModel.Abstractions.Annotations;
using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class ContextParameterValue : BaseEntity
    {
        public int ContextParameterID { get; set; }
        public virtual ContextParameter ContextParameter { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        [Required]
        [DecimalPrecision(14, 4)]        
        public decimal Value { get; set; }

        
    }
}
