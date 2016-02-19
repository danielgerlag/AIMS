using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DistributedServices.Infrastructure
{
    interface IWorkerPool
    {
        void Start();
        void Stop();
    }
}
