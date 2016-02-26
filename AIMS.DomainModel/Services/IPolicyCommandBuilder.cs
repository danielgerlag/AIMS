using System.Collections.Generic;
using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Models;

namespace AIMS.DomainModel.Services
{
    public interface IPolicyCommandBuilder
    {
        IEnumerable<PolicyTransitionCommand> Build(Policy policy);
    }
}