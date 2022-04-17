using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using DataAccess;

namespace DataAccessUnitTests
{
    internal class UnitOfWorkFactoryInMemoryTest : IUnitOfWorkFactory
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
               .UseInMemoryDatabase(databaseName: ConnectionString)
               .Options;

            return new DBLeakCheckerContext(DBContextOptions);
        }
    }
}
