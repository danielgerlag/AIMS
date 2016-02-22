using AIMS.DomainModel.Context;
using AIMS.DomainModel.Interface;

namespace AIMS.DomainModel.Services
{
    public interface ISequenceNumberGeneator
    {
        string GetNextNumber(IDbContext db, int sequenceNumberID);
    }
}