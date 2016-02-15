﻿using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class TransactionTriggerFrequency : BaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
    }
}
