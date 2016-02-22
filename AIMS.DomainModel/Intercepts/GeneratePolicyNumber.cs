using AIMS.DistributedServices.Infrastructure;
using AIMS.DomainModel.Abstractions.Intercepts;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Interface;
using AIMS.DomainModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Intercepts
{
    public class GeneratePolicyNumber : EntityIntercept<Policy>
    {

        ISequenceNumberGeneator _sequenceNumberGeneator;
        public GeneratePolicyNumber(ISequenceNumberGeneator sequenceNumberGeneator)
        {
            _sequenceNumberGeneator = sequenceNumberGeneator;
        }

        public override void Run(Policy entity, IDbContext dataContext)
        {
            entity.PolicyNumber = _sequenceNumberGeneator.GetNextNumber(dataContext, entity.PolicySubType.SequenceNumberID);
        }

    }
}