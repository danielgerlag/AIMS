using AIMS.DistributedServices.Infrastructure;
using AIMS.DomainModel.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Services
{
    public class TransactionPollManager : ITransactionPollManager
    {

        private IDataContext _db;
        private IQueueManager _txnTriggerQueue;
        private Timer _timer;

        public TransactionPollManager(IDataContext db)
        {
            _db = db;
            _txnTriggerQueue = AIMS.Services.IoC.Container.ResolveKeyed<IQueueManager>("TransactionTriggerQueue");                                    
        }

        public void Start(TimeSpan interval)
        {
            _timer = new Timer(new TimerCallback(TriggerPoll), null, TimeSpan.FromSeconds(10), interval);
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }

        private void TriggerPoll(object state)
        {
            try
            {
                var query = _db.TransactionTriggers.Where(x => x.Status.Code == "A" && x.NextExecutionDate <= DateTime.Now).Select(x => x.ID);
                var list = query.ToList();
                foreach (var item in list)
                    _txnTriggerQueue.Enqueue(item);
            }
            catch (Exception ex)
            {
                //log
            }
        }

    }
}
