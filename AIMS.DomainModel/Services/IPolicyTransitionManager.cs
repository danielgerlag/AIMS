using AIMS.DomainModel.Context;
using AIMS.DomainModel.Models;

namespace AIMS.DomainModel.Services
{
    public interface IPolicyTransitionManager
    {
        PolicyTransitionResponse Transition(PolicyTransitionRequest request, IDataContext db);
    }
}