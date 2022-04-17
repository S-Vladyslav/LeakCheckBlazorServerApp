using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using DataAccess.Interfaces;

namespace DataAccess.Context
{
    public class DBLeakCheckerContext : DbContext, IDBLeakCheckerContext
    {
        public DBLeakCheckerContext(DbContextOptions<DBLeakCheckerContext> options) : base(options)
        {

        }

        public DbSet<DomainLeak> DomainLeaks { get; set; }
        public DbSet<EmailLeak> EmailLeaks { get; set; }
    }
}
