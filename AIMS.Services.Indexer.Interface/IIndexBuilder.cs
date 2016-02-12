﻿using System;

namespace AIMS.Services.Indexer.Interface
{
    public interface IIndexBuilder
    {
        void IndexEntity(Type entityType, int id, bool recursive, Type contextType);
    }
}