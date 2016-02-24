using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMS.DistributedServices.Infrastructure;
using AIMS.DomainModel.Services;
using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using System.Data.Entity.Core;

namespace AIMS.Services.TransactionProcessing.TransactionTriggers
{
    public class TransactionTriggerWorker : DistributedWorker
    {
        IJournalGenerator _journalGenerator;

        IDataContext _db;
        IDataContext _controlDb;

        TransactionTrigger trigger;
        TransactionTriggerLog log;

        public TransactionTriggerWorker(IJournalGenerator journalGenerator)
            : base()
        {
            _journalGenerator = journalGenerator;
        }

        public override void Init(ReceivedMessage message, int threadNumber)
        {
            base.Init(message, threadNumber);
            _db = IoC.Container.Resolve<IDataContext>();
            _controlDb = IoC.Container.Resolve<IDataContext>();
            trigger = _db.TransactionTriggers.Find(message.Payload);

            if (trigger == null)
                return;

            _lockKeys.Add("TransactionTrigger:" + trigger.ID.ToString());

            if (trigger.PolicyTransactionTrigger != null)
            {
                var policyTransactionTrigger = (trigger.PolicyTransactionTrigger);
                //if (policyTransactionTrigger.PolicyID.HasValue)
                _lockKeys.Add("Policy:" + policyTransactionTrigger.PolicyID.ToString());
            }

        }

        public override void DoWork()
        {
            if (trigger == null)
                return;

            _db.Entry(trigger).Reload();


            var txOptions = new System.Transactions.TransactionOptions();
            txOptions.IsolationLevel = System.Transactions.IsolationLevel.Snapshot;

            System.Transactions.TransactionScope txnScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.RequiresNew, txOptions);
            try
            {
                JournalRunResult runResult;
                using (txnScope)
                {

                    runResult = _journalGenerator.Run(_db, trigger);
                    if ((runResult.Errors.Count() == 0) && (runResult.Holders.Count() == 0) && (runResult.Deferrals.Count() == 0))
                    {
                        _db.SaveChanges();
                        txnScope.Complete();
                    }
                    else
                    {
                        txnScope.Dispose();
                    }
                }

                foreach (var err in runResult.Errors)
                {
                    log.Exceptions.Add(new TransactionTriggerException()
                    {
                        Message = err,
                        ExceptionType = "E"
                    });
                }

                foreach (var err in runResult.Holders)
                {
                    log.Exceptions.Add(new TransactionTriggerException()
                    {
                        Message = err,
                        ExceptionType = "H"
                    });
                }

                foreach (var err in runResult.Deferrals)
                {
                    log.Exceptions.Add(new TransactionTriggerException()
                    {
                        Message = err,
                        ExceptionType = "D"
                    });
                }

                if (runResult.Errors.Count() > 0)
                    MarkError();

                if (runResult.Holders.Count() > 0)
                    MarkHold();

                if ((runResult.Errors.Count() == 0) && (runResult.Holders.Count() == 0) && (runResult.Deferrals.Count() == 0))
                {
                    if (runResult.Journals.Count > 0)
                    {
                        //
                    }
                }
            }
            catch (Exception ex)
            {
                txnScope.Dispose();

                if ((!(ex is OptimisticConcurrencyException)) && (!ex.Message.Contains("deadlock victim")))
                    MarkError();

                if (ex is AggregateException)
                {
                    foreach (var ex2 in (ex as AggregateException).InnerExceptions)
                    {
                        log.Exceptions.Add(new TransactionTriggerException()
                        {
                            Message = ex2.Message,
                            ExceptionType = "E"

                        });
                    }
                }
                else
                {
                    log.Exceptions.Add(new TransactionTriggerException()
                    {
                        Message = ex.Message,
                        ExceptionType = "E"
                    });
                }
                throw ex;
            }

        }


        public override bool CanExecuteNow()
        {
            if (trigger == null)
                return true;

            if (!trigger.NextExecutionDate.HasValue)
                return false;

            if (trigger.NextExecutionDate.Value > DateTime.Now)
                return false;

            int priority = trigger.JournalTemplate.Priority;
            int blockingGenerators = 0;
            IQueryable<TransactionTrigger> subSet = null;

            if (trigger.PolicyTransactionTrigger != null)
            {
                var policyTransactionTrigger = (trigger.PolicyTransactionTrigger);
                subSet = _db.Entry<Policy>(policyTransactionTrigger.Policy).Collection<PolicyTransactionTrigger>("TransactionTriggers").Query().Select(x => x.TransactionTrigger);
            }

            if (subSet == null)
                return true;

            blockingGenerators = subSet.Count(g => (!g.OnHold) && (g.Status.Code == "A") && g.ID != trigger.ID && g.TxnDate < trigger.TxnDate);

            if (blockingGenerators == 0)
                blockingGenerators = subSet.Count(g => (!g.OnHold) && (g.Status.Code == "A") && g.ID != trigger.ID && g.TxnDate == trigger.TxnDate && g.JournalTemplate.Priority < priority);

            return (blockingGenerators == 0);
        }

        public override void StartLog()
        {
            log = new TransactionTriggerLog();
            log.StartTime = DateTime.Now;
            log.Thread = ThreadNumber;
            log.TransactionTriggerID = trigger.ID;
            log.MachineName = Environment.MachineName;

            _controlDb.TransactionTriggerLogs.Add(log);

            _controlDb.SaveChanges();
        }

        public override void EndLog(bool success, bool failure, bool requeue)
        {
            log.EndTime = DateTime.Now;
            log.IsSuccess = success;
            log.IsError = failure;
            log.IsRequeue = requeue;
            _controlDb.SaveChanges();
        }

        private void MarkError()
        {
            //_db.SaveChanges();
        }

        private void MarkHold()
        {
            //trigger.OnHold = true;
            //_db.SaveChanges();
        }
    }
}
