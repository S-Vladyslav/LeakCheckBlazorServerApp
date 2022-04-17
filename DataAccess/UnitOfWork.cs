using Polly;
using DataAccess.Context;
using DataAccess.Interfaces;
using Polly.CircuitBreaker;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DBLeakCheckerContext _dbContext;

        public void SetContext(IDBLeakCheckerContext dbContext)
        {
            _dbContext = (DBLeakCheckerContext)dbContext;
        }

        public Y GetRepository<T, Y>()
            where Y : IRepository
            where T : Y, new()
        {
            Y repository = new T();
            repository.SetContext(_dbContext);
            return repository;
        }

        public void SaveChanges()
        {
            var circuitBreakerPolicy = Policy.Handle<InvalidOperationException>().CircuitBreaker(
                exceptionsAllowedBeforeBreaking: 3,
                durationOfBreak: TimeSpan.FromSeconds(10));

            var retryPolicy = Policy.Handle<InvalidOperationException>().WaitAndRetry(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(1));

            retryPolicy.Execute(() =>
            {
                try
                {
                    circuitBreakerPolicy.Execute(() =>
                    {
                        _dbContext.SaveChanges();
                    });
                }
                catch (BrokenCircuitException)
                {
                    ;
                }
            });
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
