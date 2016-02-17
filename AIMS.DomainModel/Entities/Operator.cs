using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class Operator : BaseEntity
    {
        [Index]
        public int OperatorPublicID { get; set; }
        public virtual Public OperatorPublic { get; set; }

        [Index]
        public int PolicyID { get; set; }
        public virtual Policy Policy { get; set; }

        public int OperatorTypeID { get; set; }
        public virtual OperatorType OperatorType { get; set; }


        public virtual ICollection<OperatorAttribute> Attributes { get; set; } = new HashSet<OperatorAttribute>();

    }
}
