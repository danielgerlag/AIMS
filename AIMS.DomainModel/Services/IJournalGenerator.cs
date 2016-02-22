using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using System.Collections.Generic;

namespace AIMS.DomainModel.Services
{
    public interface IJournalGenerator
    {
        JournalRunResult Run(IDataContext db, TransactionTrigger transactionTrigger);
    }

    public class JournalRunResult
    {
        public List<Journal> Journals { get; set; } = new List<Journal>();
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Holders { get; set; } = new List<string>();
        public List<string> Deferrals { get; set; } = new List<string>();

    }
}