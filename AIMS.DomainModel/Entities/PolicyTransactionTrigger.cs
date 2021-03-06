﻿using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyTransactionTrigger
    {
        [Key]
        public int TransactionTriggerID { get; set; }
        public virtual TransactionTrigger TransactionTrigger { get; set; }

        public int PolicyID { get; set; }
        public virtual Policy Policy { get; set; }
    }
}
