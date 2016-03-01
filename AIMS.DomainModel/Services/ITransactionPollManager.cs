using System;

namespace AIMS.DomainModel.Services
{
    public interface ITransactionPollManager
    {
        void Start(TimeSpan interval);
        void Stop();
    }
}