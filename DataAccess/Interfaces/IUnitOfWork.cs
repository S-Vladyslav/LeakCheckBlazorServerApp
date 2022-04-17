namespace DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void SetContext(IDBLeakCheckerContext dbContext);
        public Y GetRepository<T, Y>()
            where Y : IRepository
            where T : Y, new();
        void SaveChanges();
    }
}
