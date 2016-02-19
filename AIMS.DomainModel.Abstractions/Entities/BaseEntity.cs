using AIMS.DomainModel.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Abstractions.Entities
{
    public abstract class BaseEntity : IDomainEntity
    {
        [Key]
        public int ID { get; set; }

        [Timestamp]
        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }

        public DateTime DateCreatedUTC { get; set; }

        public DateTime DateModifiedUTC { get; set; }

        public event EntityEventHandler OnAdd;
        public event EntityEventHandler OnChange;
        public event EntityEventHandler OnAddAfterCommit;
        public event EntityEventHandler OnChangeAfterCommit;

        public void RaiseOnAdd(IDbContext dataService)
        {
            if (OnAdd != null)
            {
                EntityEventArgs args = new EntityEventArgs() { DataService = dataService, Entity = this };
                OnAdd(this, args);
            }
        }
        public void RaiseOnAddAfterCommit(IDbContext dataService)
        {
            if (OnAddAfterCommit != null)
            {
                EntityEventArgs args = new EntityEventArgs() { DataService = dataService, Entity = this };
                OnAddAfterCommit(this, args);
            }
        }
        public void RaiseOnAddAfterCommit(IDbContext dataService)
        {
            if (OnAddAfterCommit != null)
            {
                EntityEventArgs args = new EntityEventArgs() { DataService = dataService, Entity = this };
                OnAddAfterCommit(this, args);
            }
        }
        public void RaiseOnAddAfterCommit(IDbContext dataService)
        {
            if (OnAddAfterCommit != null)
            {
                EntityEventArgs args = new EntityEventArgs() { DataService = dataService, Entity = this };
                OnAddAfterCommit(this, args);
            }
        }

        public virtual string GetLookupText()
        {
            return string.Empty;
        }


    }

    public class EntityEventArgs : EventArgs
    {
        public IDbContext DataService { get; set; }
        public BaseEntity Entity { get; set; }
    }

    public delegate void EntityEventHandler(object sender, EntityEventArgs e);
}
