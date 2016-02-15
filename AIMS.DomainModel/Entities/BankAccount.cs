using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class BankAccount : BaseEntity
    {
        public int PublicID { get; set; }
        public virtual Public Public { get; set; }

        [MaxLength(20)]
        public string InstitutionCode { get; set; }

        [MaxLength(50)]
        public string BranchCode { get; set; }

        [MaxLength(50)]
        public string AccountNumber { get; set; }

    }
}
