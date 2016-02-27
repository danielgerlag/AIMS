using AIMS.DomainModel.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Models
{
    public class PolicyTransitionRequest
    {
        public int PolicyTypeTransitionID { get; set; }

        public List<PolicyTransitionRequestInput> Inputs { get; set; } = new List<PolicyTransitionRequestInput>();

        public object GetInputValue(string name, IDataContext db)
        {
            var srcInput = db.PolicyTypeTransitionInputs.FirstOrDefault(x => x.PolicyTypeTransitionID == PolicyTypeTransitionID && x.Name == name);

            if (srcInput == null)
                return null;

            var destInput = Inputs.FirstOrDefault(x => x.PolicyTypeTransitionInputID == srcInput.ID);

            if (destInput == null)
                return null;


            switch (srcInput.AttributeDataType.Code)
            {
                case "NUM":
                case "YEA":
                    return Convert.ToInt32(destInput.Value);
                case "DEC":
                case "PER":
                    return Convert.ToDecimal(destInput.Value);
                case "DTE":
                    return Convert.ToDateTime(destInput.Value);
                case "BLN":
                    return Convert.ToBoolean(destInput.Value);
            }

            return destInput.Value;
        }
    }

    public class PolicyTransitionRequestInput
    {
        public int PolicyTypeTransitionInputID { get; set; }

        public string Value { get; set; }
    }
}
