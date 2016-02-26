using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Models
{
    public class PolicyTransitionRequest
    {
        public int PolicyID { get; set; }

        public int PolicyTypeTransitionID { get; set; }

        public List<PolicyTransitionRequestInput> Inputs { get; set; } = new List<PolicyTransitionRequestInput>();
    }

    public class PolicyTransitionRequestInput
    {
        public int PolicyTypeTransitionInputID { get; set; }

        public string Value { get; set; }
    }
}
