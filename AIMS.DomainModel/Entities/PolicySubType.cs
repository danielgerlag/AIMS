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
    public class PolicySubType : BaseEntity
    {   
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public int PolicyTypeID { get; set; }
        public virtual PolicyType PolicyType { get; set; }

        public int SequenceNumberID { get; set; }
        public virtual SequenceNumber SequenceNumber { get; set; }

        public int RegionID { get; set; }
        public virtual Region Region { get; set; }


        public virtual ICollection<PolicySubTypeRatingProfile> RatingProfiles { get; set; } = new HashSet<PolicySubTypeRatingProfile>();        

        public virtual ICollection<PolicySubTypeCoverage> Coverages { get; set; } = new HashSet<PolicySubTypeCoverage>();

        public virtual ICollection<PolicySubTypeContextParameterValue> ContextParameterValues { get; set; } = new HashSet<PolicySubTypeContextParameterValue>();

        public override string GetLookupText()
        {
            string result = string.Empty;
            if (PolicyType != null)
                result += PolicyType.Name + " - ";
            result += Name;
            return result;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName
        {
            get
            {
                return GetLookupText();
            }
            private set { }
        }
    }
}
