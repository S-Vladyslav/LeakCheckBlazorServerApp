using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IEmailLeakService
    {
        void SetUnitOfWorkFactory(IUnitOfWorkFactory unitOfWorkFactory);
        void AddEmailLeaks(List<EmailLeak> Leaks);
        List<EmailLeak> GetEmailLeaksByEmail(string email);
        void DeleteEmailLeaksByEmailAddress(string email);
    }
}
