using AIMS.DomainModel.Abstractions.Intercepts;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Interface;
using AIMS.DomainModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Intercepts
{
    public class EnqueueTransactionTrigger : EntityIntercept<TransactionTrigger>
    {

        IJournalGenerator _journalGenerator;
        public EnqueueTransactionTrigger(IJournalGenerator journalGenerator)
        {
            _journalGenerator = journalGenerator;
        }

        public override void Run(TransactionTrigger entity, IDbContext dataContext)
        {
            //entity.Description = entity.Description + " Boo";
            //if (entity.Status.Code == "A")
            //    _journalGenerator.Run(entity.ID);

        }

    }
}