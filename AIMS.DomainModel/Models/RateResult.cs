using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Models
{
    public class RateResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public List<string> Log { get; set; } = new List<string>();
    }
}
