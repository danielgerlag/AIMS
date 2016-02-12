﻿using AIMS.DomainModel.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AIMS.Services.Indexer.Interface
{
    public interface IIndexRegister
    {        
        void RegisterEntityTypes(Assembly assembly);
        Type LookupEntityType(string name);
        IEntityIndexer GetIndexer(Type entityType);
        bool CanIndex(IDomainEntity entity);
    }
}
