using AIMS.DistributedServices.Infrastructure;
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
                
        public EnqueueTransactionTrigger()
        {            
        }

        public override void Run(TransactionTrigger entity, IDbContext dataContext)
        {
            if ((entity.Status.Code == "A") && (entity.NextExecutionDate.HasValue))
            {
                if (entity.NextExecutionDate.Value <= DateTime.Now)
                {
                    var queue = AIMS.Services.IoC.Container.ResolveKeyed<IQueueManager>("TransactionTriggerQueue");
                    queue.Enqueue(entity.ID);
                }
            }
        }

    }
}