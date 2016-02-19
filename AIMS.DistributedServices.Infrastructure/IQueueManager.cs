namespace AIMS.DistributedServices.Infrastructure
{
    public interface IQueueManager
    {
        void Enqueue(int obj);
        ReceivedMessage TryDequeue(int timeout);
    }
}