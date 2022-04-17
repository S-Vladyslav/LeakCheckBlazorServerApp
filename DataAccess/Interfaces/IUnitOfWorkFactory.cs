namespace DataAccess.Interfaces
{
    public interface IUnitOfWorkFactory
    {
        void SetConnectionString(string connectionString);
        IUnitOfWork CreateUnitOfWork();
        IDBLeakCheckerContext CreateContext();
    }
}
