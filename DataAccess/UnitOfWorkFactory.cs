using DataAccess.Interfaces;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private string ConnectionString { get; set; }

        public void SetConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            var dbContext = CreateContext();
            var unitOfWork = new UnitOfWork();
            unitOfWork.SetContext(dbContext);

            return unitOfWork;
        }

        public IDBLeakCheckerContext CreateContext()
        { 
            var DBContextOptions = new DbContextOptionsBuilder<DBLeakCheckerContext>()
               .UseSqlServer(ConnectionString)
               .Options;
           
            return new DBLeakCheckerContext(DBContextOptions);
        }
    }
}
