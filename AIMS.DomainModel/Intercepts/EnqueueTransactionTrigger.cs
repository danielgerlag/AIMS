using AIMS.DomainModel.Abstractions.Intercepts;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Intercepts
{
    public class EnqueueTransactionTrigger : IEntityIntercept<TransactionTrigger>
    {
        public void Run(TransactionTrigger entity)
        {
            //
        }
    }
}
