using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Services
{
    public class JournalPoster : IJournalPoster
    {
        
        public JournalRunResult Run(IDataContext db, Journal journal)
        {
            JournalRunResult result = new JournalRunResult();                       

            return result;
        }

        
    }
        
}
