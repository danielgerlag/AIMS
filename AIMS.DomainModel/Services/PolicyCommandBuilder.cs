using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Services
{
    public class PolicyCommandBuilder : IPolicyCommandBuilder
    {

        public IEnumerable<PolicyTransitionCommand> Build(Policy policy)
        {
            List<PolicyTransitionCommand> result = new List<PolicyTransitionCommand>();
            foreach (var transition in policy.PolicySubType.PolicyType.Transitions)
            {
                PolicyTransitionCommand cmd = new PolicyTransitionCommand();
                cmd.PolicyTypeTransitionID = transition.ID;
                cmd.Text = transition.ButtonText;
                cmd.CanExecute = (policy.StatusID == transition.InitialStatusID);
                result.Add(cmd);
            }

            return result;
        }
    }
}
