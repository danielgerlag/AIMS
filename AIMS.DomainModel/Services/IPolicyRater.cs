using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Models;

namespace AIMS.DomainModel.Services
{
    public interface IPolicyRater
    {
        RateResult Rate(Policy policy, IDataContext db);
    }
}