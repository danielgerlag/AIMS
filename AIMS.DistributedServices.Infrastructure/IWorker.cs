using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DistributedServices.Infrastructure
{
    interface IWorker : IDisposable
    {
        void Init(ReceivedMessage message, int threadNumber);
        bool AquireLock();
        bool CanExecuteNow();
        void DoWork();
        void EndLog(bool success, bool failure, bool requeue);
        void ReleaseLock();
        void StartLog();
    }
}
