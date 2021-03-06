﻿using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class JournalType : BaseEntity
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsGeneral { get; set; }

        [Required]
        public bool IsInvoice { get; set; }

        [Required]
        public bool IsPayment { get; set; }
                

    }
}
