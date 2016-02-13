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

        public event EntityCommitedEventHandler AfterCommit;

        public void RaiseAfterCommit(IDbContext dataService)
        {
            if (AfterCommit != null)
            {
                EntityCommitedEventArgs args = new EntityCommitedEventArgs() { DataService = dataService, Entity = this };
                AfterCommit(this, args);
            }
        }

        public virtual string GetLookupText()
        {
            return string.Empty;
        }


    }

    public class EntityCommitedEventArgs : EventArgs
    {
        public IDbContext DataService { get; set; }
        public BaseEntity Entity { get; set; }
    }

    public delegate void EntityCommitedEventHandler(object sender, EntityCommitedEventArgs e);
}
