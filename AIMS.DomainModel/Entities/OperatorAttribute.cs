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
    public class OperatorAttribute : BaseEntity
    {
        [Index]
        public int OperatorID { get; set; }
        public virtual Operator Operator { get; set; }

        [Index]
        public int OperatorTypeAttributeID { get; set; }
        public virtual OperatorTypeAttribute OperatorTypeAttribute { get; set; }

        [MaxLength(500)]
        public string Value { get; set; }
    }
}
