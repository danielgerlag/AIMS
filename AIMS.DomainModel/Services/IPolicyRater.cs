using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;

namespace AIMS.DomainModel.Services
{
    public interface IPolicyRater
    {
        bool Rate(Policy policy, IDataContext db);
    }
}