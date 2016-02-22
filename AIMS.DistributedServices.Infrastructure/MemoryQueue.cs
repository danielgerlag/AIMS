using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DistributedServices.Infrastructure
{
    public class MemoryQueue : IQueueManager
    {

        protected ConcurrentQueue<int> _queue = new ConcurrentQueue<int>();

        public MemoryQueue()
        {
        }

        public void Enqueue(int obj)
        {
            _queue.Enqueue(obj);
        }

        public ReceivedMessage TryDequeue(int timeout)
        {
            int item;
            if (_queue.TryDequeue(out item))
            {
                return new ReceivedMessage(item, this, null);
            }
            return null;
        }
    }
}
