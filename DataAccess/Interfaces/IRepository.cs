namespace DataAccess.Interfaces
{
    public interface IRepository
    { 
        void SetContext(IDBLeakCheckerContext dbContext);
    }
}
