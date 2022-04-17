using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IDomainLeakRepository : IRepository
    {
        void AddLeaks(List<DomainLeak> leakList);
        void AddLeaksSimple(List<DomainLeak> leakList);
        void DeleteLeaks(string targetString);
        List<DomainLeak> GetLeaks(string targetString);
    }
}
