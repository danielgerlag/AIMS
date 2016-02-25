using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Models
{
    public class LedgerTxnBalance
    {
        public DateTime TxnDate { get; set; }

        public string Description { get; set; }

        public string Reference { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }
    }
}
