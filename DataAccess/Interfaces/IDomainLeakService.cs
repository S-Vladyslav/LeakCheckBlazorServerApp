using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IDomainLeakService
    {
        void SetUnitOfWorkFactory(IUnitOfWorkFactory unitOfWorkFactory);
        void AddDomainLeaks(List<DomainLeak> Leaks);
        List<DomainLeak> GetDomainLeaksByDomain(string domain);
        void DeleteDomainLeaksByDomain(string domain);
    }
}
