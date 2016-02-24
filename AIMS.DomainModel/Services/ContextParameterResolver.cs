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
            var value = policy.ContextParameterValues.Where(x => x.ContextParameterID == contextParameterID && x.EffectiveDate <= effectiveDate)
                .OrderByDescending(x => x.EffectiveDate)
                .FirstOrDefault();

            if (value != null)
                return value.Value;

            return Resolve(db, effectiveDate, contextParameterID, policy.PolicySubType);
        }


        public decimal Resolve(IDataContext db, DateTime effectiveDate, int contextParameterID, PolicySubType policySubType)
        {
            var value = policySubType.ContextParameterValues.Where(x => x.ContextParameterID == contextParameterID && x.EffectiveDate <= effectiveDate)
                .OrderByDescending(x => x.EffectiveDate)
                .FirstOrDefault();

            if (value != null)
                return value.Value;

            return Resolve(db, effectiveDate, contextParameterID, policySubType.PolicyType, policySubType.Region);
        }

        public decimal Resolve(IDataContext db, DateTime effectiveDate, int contextParameterID, PolicyType policyType, Region region)
        {
            var value = policyType.ContextParameterValues.Where(x => x.ContextParameterID == contextParameterID && x.EffectiveDate <= effectiveDate)
                .OrderByDescending(x => x.EffectiveDate)
                .FirstOrDefault();

            if (value != null)
                return value.Value;

            return Resolve(db, effectiveDate, contextParameterID, region);
        }


        public decimal Resolve(IDataContext db, DateTime effectiveDate, int contextParameterID, Region region)
        {
            var value = region.ContextParameterValues.Where(x => x.ContextParameterID == contextParameterID && x.EffectiveDate <= effectiveDate)
                .OrderByDescending(x => x.EffectiveDate)
                .FirstOrDefault();

            if (value != null)
                return value.Value;

            return 0;
        }

    }
}
