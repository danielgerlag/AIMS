using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Interface;
using AIMS.Services.Indexer.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Abstractions
{
    public abstract class BaseContext : DbContext, IDbContext
    {
        private readonly IIndexRegister _indexRegistry;

        public BaseContext(string connectionString)
            : base(connectionString)
        {
            _indexQueue = null;
            _indexRegistry = null;
        }

        public BaseContext(string connectionString, IIndexQueue indexQueue, IIndexRegister indexRegistry)
            : base(connectionString)
        {
            _indexQueue = indexQueue;
            _indexRegistry = indexRegistry;
            indexRegistry.RegisterEntityTypes(GetType().Assembly);
        }

        public BaseContext(DbConnection connection, IIndexQueue indexQueue, IIndexRegister indexRegistry)
            : base(connection, true)
        {
            _indexQueue = indexQueue;
            _indexRegistry = indexRegistry;
            indexRegistry.RegisterEntityTypes(GetType().Assembly);
        }

        private ConcurrentQueue<BaseEntity> _postCommitQueue = new ConcurrentQueue<BaseEntity>();
        private bool _addedPostCommitEventToTxn = false;
        private IIndexQueue _indexQueue;

        //public virtual DbSet<LedgerAccountType> LedgerAccountTypes { get; set; }
        //public virtual DbSet<LedgerAccount> LedgerAccounts { get; set; }
        //public virtual DbSet<ReportingEntityProfile> ReportingEntityProfiles { get; set; }


        //public virtual DbSet<DataArea> DataAreas { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public override int SaveChanges()
        {
            try
            {
                PreSave();
                int count = base.SaveChanges();
                PostSave();
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public override async Task<int> SaveChangesAsync()
        {
            PreSave();
            int count = await base.SaveChangesAsync();
            PostSave();
            return count;
        }

        public override DbSet Set(Type entityType)
        {
            return base.Set(entityType);
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public new DbChangeTracker ChangeTracker
        {
            get { return base.ChangeTracker; }
        }

        public new DbContextConfiguration Configuration
        {
            get { return base.Configuration; }
        }

        public new Database Database
        {
            get { return base.Database; }
        }

        private void PreSave()
        {
            var trackedEntities = ChangeTracker.Entries();

            foreach (var item in trackedEntities)
            {
                if (item.Entity is BaseEntity)
                {
                    if (item.State == EntityState.Added)
                    {
                        (item.Entity as BaseEntity).DateCreatedUTC = DateTime.UtcNow;
                        (item.Entity as BaseEntity).DateModifiedUTC = DateTime.UtcNow;
                    }

                    if (item.State == EntityState.Modified)
                    {
                        (item.Entity as BaseEntity).DateModifiedUTC = DateTime.UtcNow;
                    }

                    if ((!_postCommitQueue.Contains(item.Entity)))
                        _postCommitQueue.Enqueue(item.Entity as BaseEntity);
                }
            }
        }

        private void PostSave()
        {
            var openTransaction = System.Transactions.Transaction.Current;
            if (openTransaction == null)
            {
                Task.Factory.StartNew(new Action(() => { PerformPostCommit(); }));
            }
            else
            {
                if (!_addedPostCommitEventToTxn)
                {
                    openTransaction.TransactionCompleted += openTransaction_TransactionCompleted;
                    _addedPostCommitEventToTxn = true;
                }
            }
        }

        private void PerformPostCommit()
        {
            BaseEntity entity;
            while (_postCommitQueue.TryDequeue(out entity))
            {
                if (entity is BaseEntity)
                {
                    entity.RaiseAfterCommit(this);
                    if (_indexRegistry != null)
                    {
                        if (_indexRegistry.CanIndex(entity))
                        {
                            Type type = entity.GetType();
                            _indexQueue.QueueIndexWork(type, entity.ID, true, GetInterfaceType());
                        }
                    }
                }
            }
        }

        void openTransaction_TransactionCompleted(object sender, System.Transactions.TransactionEventArgs e)
        {
            PerformPostCommit();
        }

        protected abstract Type GetInterfaceType();


    }
}
