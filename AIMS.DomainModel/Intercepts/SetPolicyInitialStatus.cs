using System;
using System.Collections.Generic;
using AIMS.DomainModel.Abstractions.Intercepts;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Interface;

namespace AIMS.DomainModel.Intercepts
{
    public class SetPolicyInitialStatus : EntityIntercept<Policy>
    {
        
        public override void Run(Policy entity, IDbContext dataContext)
        {
            entity.Status = entity.PolicySubType.PolicyType.InitialStatus;
        }

    }
}