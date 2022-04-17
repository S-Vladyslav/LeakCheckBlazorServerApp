using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IEmailLeakRepository : IRepository
    {
        void AddLeaks(List<EmailLeak> leakList);
        void DeleteLeaks(string targetString);
        List<EmailLeak> GetLeaks(string targetString);
    }
}
