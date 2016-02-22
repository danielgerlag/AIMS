using AIMS.DistributedServices.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.Services.TransactionProcessing.TransactionTriggers
{
    public class TransactionTriggerPool : WorkerPool
    {
        public TransactionTriggerPool(int threadCount, TimeSpan timeOut) : base(threadCount, timeOut)
        {
        }

        protected override string QueueKey
        {
            get
            {
                return "TransactionTriggerQueue";
            }
        }

        protected override string WorkerKey
        {
            get
            {
                return "TransactionTriggerWorker";
            }
        }
    }
}
