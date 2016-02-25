namespace AIMS.DomainModel.Services
{
    public interface IPolicyRater
    {
        bool Rate(int policyID);
    }
}