using System;
using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;

namespace AIMS.DomainModel.Services
{
    public interface IContextParameterResolver
    {
        decimal Resolve(IDataContext db, DateTime effectiveDate, int contextParameterID, Policy policy);
    }
}