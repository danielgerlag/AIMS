using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Services
{
    public class ContextParameterResolver : IContextParameterResolver
    {

        public decimal Resolve(IDataContext db, DateTime effectiveDate, int contextParameterID, Policy policy)
        {
            return 0;
        }

    }
}
