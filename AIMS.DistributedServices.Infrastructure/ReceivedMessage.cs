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

            if (_transaction != null)
            {
                _transaction = transaction;
                _connection = _transaction.Connection;
            }
        }

        public void Ack()
        {
            if (!_finalised)
            {
                if (_transaction != null)
                {
                    _transaction.Commit();
                    CloseConnection();
                }
                _finalised = true;
            }
        }

        public void NAck()
        {
            if (!_finalised)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    CloseConnection();
                }
                _finalised = true;
            }
        }

        public void Requeue()
        {
            if (!_finalised)
            {
                _finalised = true;
                
                Ack();
                System.Threading.Thread.Sleep(2000);
                _queueManager.Enqueue(Payload);                
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
