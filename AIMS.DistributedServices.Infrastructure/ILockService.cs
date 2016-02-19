using System;

namespace AIMS.DistributedServices.Infrastructure
{
    public interface ILockService: IDisposable
    {
        bool AquireLock(string uid);
        void ReleaseAllLocks();
        void ReleaseLock(string uid);
    }
}