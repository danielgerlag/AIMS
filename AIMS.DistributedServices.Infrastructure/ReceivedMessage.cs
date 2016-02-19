using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DistributedServices.Infrastructure
{
    public class ReceivedMessage
    {
        private bool _finalised = false;
        protected IQueueManager _queueManager;
        protected DbTransaction _transaction;
        protected DbConnection _connection;

        public int Payload { get; set; }


        public ReceivedMessage(int payload, IQueueManager queueManager, DbTransaction transaction)
        {
            Payload = payload;
            _queueManager = queueManager;
            //_txnScope = txnScope;
            _transaction = transaction;
            _connection = _transaction.Connection;
        }

        public void Ack()
        {
            if (!_finalised)
            {
                _transaction.Commit();
                //_txnScope.Complete();
                CloseConnection();
                _finalised = true;
            }
        }

        public void NAck()
        {
            if (!_finalised)
            {
                _transaction.Rollback();
                //_txnScope.Dispose();
                CloseConnection();
                _finalised = true;
            }
        }

        public void Requeue()
        {
            if (!_finalised)
            {
                _finalised = true;

                //System.Threading.Tasks.Task.Factory.StartNew(new Action(() =>
                //{
                Ack();
                System.Threading.Thread.Sleep(2000);
                _queueManager.Enqueue(Payload);
                //}));                
            }
        }


        private void CloseConnection()
        {
            try
            {
                _connection.Close();
            }
            catch
            {
            }
        }
    }
}
