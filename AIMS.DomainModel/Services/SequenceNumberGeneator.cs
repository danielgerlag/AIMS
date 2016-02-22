using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Services
{
    public class SequenceNumberGeneator : ISequenceNumberGeneator
    {

        public string GetNextNumber(IDbContext db, int sequenceNumberID)
        {
            var seqNum = db.Set<SequenceNumber>().Find(sequenceNumberID);
            db.Entry(seqNum).State = System.Data.Entity.EntityState.Detached;            
            var result = db.Database.SqlQuery<int>(@"EXEC GetNextSequenceNumber {0}", sequenceNumberID);

            if (!String.IsNullOrEmpty(seqNum.FormatMask))
                return string.Format(seqNum.FormatMask, result.First());
            else
                return result.First().ToString();
        }

    }
}
