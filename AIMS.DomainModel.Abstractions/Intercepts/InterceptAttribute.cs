using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Abstractions.Intercepts
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]    
    public class InterceptAttribute : Attribute
    {
        public Type Intercept { get; set; }        
        public Stage Stage { get; set; }
        public int Order { get; set; }
    }

    public enum Stage { OnAddBeforeCommit, OnChangeBeforeCommit, OnAddAfterCommit, OnChangeAfterCommit }
}
