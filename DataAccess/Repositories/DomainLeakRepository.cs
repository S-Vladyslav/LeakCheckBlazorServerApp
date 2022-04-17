using Microsoft.EntityFrameworkCore;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Context;

namespace DataAccess.Repositories
{
    public class DomainLeakRepository : IDomainLeakRepository
    {
        private DBLeakCheckerContext _dbContext;
        private DbSet<DomainLeak> _dbSet;
        
        public void SetContext(IDBLeakCheckerContext dbContext)
        {
            _dbContext = (DBLeakCheckerContext)dbContext;
            _dbSet = _dbContext.Set<DomainLeak>();
        }

        public void AddLeaks(List<DomainLeak> leakList)
        {
            _dbSet.BulkInsert(leakList);
        }

        public void AddLeaksSimple(List<DomainLeak> leakList)
        {
            foreach(var leak in leakList)
            {
                _dbSet.Add(leak);
            }
        }

        public void DeleteLeaks(string domain)
        {
            _dbSet.RemoveRange(GetLeaks(domain)
               .Select(i => i)
               .Where(i => i.Domain == domain));
        }

        public List<DomainLeak> GetLeaks(string domain)
        {
            var LeaksList = _dbSet.Select(i => i)
                .Where(i => i.Domain == domain)
                .ToList();

            return LeaksList;
        }
    }
}
