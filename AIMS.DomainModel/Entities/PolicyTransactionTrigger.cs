using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyTransactionTrigger : TransactionTrigger
    {

        public int PolicyID { get; set; }
        public virtual Policy Policy { get; set; }
    }
}
