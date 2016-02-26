using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Models;

namespace AIMS.DomainModel.Services
{
    public interface IPolicyTransitionManager
    {
        PolicyTransitionResponse Transition(Policy policy, PolicyTransitionRequest request, IDataContext db);
    }
}