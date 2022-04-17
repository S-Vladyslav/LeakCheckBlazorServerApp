using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using DataAccess.Context;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class EmailLeakRepository : IEmailLeakRepository
    {
        private DBLeakCheckerContext _dbContext;
        private DbSet<EmailLeak> _dbSet;

        public void SetContext(IDBLeakCheckerContext dbContext)
        {
            _dbContext = (DBLeakCheckerContext)dbContext;
            _dbSet = _dbContext.Set<EmailLeak>();
        }

        public void AddLeaks(List<EmailLeak> leakList)
        {
            _dbSet.BulkInsert(leakList);
        }

        public void DeleteLeaks(string email)
        {
            _dbSet.RemoveRange(GetLeaks(email)
               .Select(i => i)
               .Where(i => i.Domain == email));
        }

        public List<EmailLeak> GetLeaks(string email)
        {
            var LeaksList = _dbSet.Select(i => i)
                .Where(i => i.EmailAddress == email)
                .ToList();

            return LeaksList;
        }
    }
}
