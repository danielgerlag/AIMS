using AIMS.DomainModel.Abstractions.Intercepts;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Intercepts
{
    public class EnqueueTransactionTrigger : EntityIntercept<TransactionTrigger>
    {
        public override void Run(TransactionTrigger entity, IDbContext dataContext)
        {
            //entity.Description = entity.Description + " Boo";
        }
        
    }
}
