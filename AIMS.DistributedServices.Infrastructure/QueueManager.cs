using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DistributedServices.Infrastructure
{
    public class QueueManager : IQueueManager
    {

        protected readonly string _connectionString;

        protected readonly string _receiveService;

        protected readonly string _queueName;

        public QueueManager(string connectionString, string queueName, string receiveService)
        {
            _connectionString = connectionString;
            _receiveService = receiveService;
            _queueName = queueName;
        }                
                

        public void Enqueue(int obj)
        {
            lock (this)
            {
                SqlConnection conn = new SqlConnection(_connectionString);
                conn.Open();
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "[dbo].[EnqueueMessage] @Service = @Service, @ID = @ID";
                    cmd.Parameters.Add(new SqlParameter("@Service", _receiveService));
                    cmd.Parameters.Add(new SqlParameter("@ID", obj));
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public ReceivedMessage TryDequeue(int timeout)
        {            
            SqlConnection conn = new SqlConnection(_connectionString);

            conn.Open();
            SqlCommand cmd = conn.CreateCommand();


            SqlTransaction txn = conn.BeginTransaction();
            //cmd.CommandText = "WAITFOR (RECEIVE TOP(1) CONVERT(INT, message_body) FROM [dbo]." + QueueName + " ),TIMEOUT " + timeout.ToString() + " ;";            
            cmd.CommandText = "RECEIVE TOP(1) CONVERT(INT, message_body) FROM [dbo]." + _queueName + ";"; // " WHERE message_enqueue_time < (DATEADD(SECOND, -5, getdate()));";

            cmd.Transaction = txn;
            object result = cmd.ExecuteScalar();

            if (result is int)
            {
                return new ReceivedMessage(Convert.ToInt32(result), this, txn);
            }

            txn.Rollback();
            conn.Close();
            return null;
        }

        //public int Count()
        //{
        //    DataService dataService = _dataServiceFactory.CreateDataService();
        //    int result = dataService.Context.ExecuteStoreQuery<int>("SELECT count(*) FROM [dbo]." + QueueName + " WITH(NOLOCK)").First();
        //    return result;
        //}

    }
}
