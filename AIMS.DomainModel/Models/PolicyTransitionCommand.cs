using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Models
{
    public class PolicyTransitionCommand
    {
        public int PolicyTypeTransitionID { get; set; }
        public string Text { get; set; }
        public bool CanExecute { get; set; }
    }
}
