using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Abstractions.Intercepts
{
    public interface IEntityIntercept<T>
        where T : BaseEntity
    {
        void Run(T entity);
    }
}
