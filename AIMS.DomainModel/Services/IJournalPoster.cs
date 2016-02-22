using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;

namespace AIMS.DomainModel.Services
{
    public interface IJournalPoster
    {
        JournalRunResult Run(IDataContext db, Journal journal);
    }
}