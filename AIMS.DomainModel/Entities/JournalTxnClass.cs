using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public  class JournalTxnClass : BaseEntity
    {

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]        
        public bool IsDailyCalc { get; set; }

        [Required]
        public bool IsDefinedAmount { get; set; }

        [Required]
        public bool IsPercentage { get; set; }

        [Required]
        public bool OfLedgerAccount { get; set; }

        [Required]
        public bool OfContextParameter { get; set; }

        [Required]
        public bool OfCoveragePremium { get; set; }


    }
}
