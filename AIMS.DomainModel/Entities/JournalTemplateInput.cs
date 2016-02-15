using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class JournalTemplateInput : BaseEntity
    {

        public int JournalTemplateID { get; set; }
        public virtual JournalTemplate JournalTemplate { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
                
        [MaxLength(500)]
        public string Description { get; set; }

        public int AttributeDataTypeID { get; set; }
        public virtual AttributeDataType AttributeDataType { get; set; }

    }
}
