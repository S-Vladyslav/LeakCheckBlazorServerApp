using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IDBLeakCheckerContext
    {
        DbSet<DomainLeak> DomainLeaks { get; set; }
        DbSet<EmailLeak> EmailLeaks { get; set; }
    }
}
