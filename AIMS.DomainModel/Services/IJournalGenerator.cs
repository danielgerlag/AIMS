namespace AIMS.DomainModel.Services
{
    public interface IJournalGenerator
    {
        void Run(int transactionTriggerID);
    }
}