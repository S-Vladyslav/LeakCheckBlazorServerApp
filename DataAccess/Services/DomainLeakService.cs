using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;

namespace DataAccess.Services
{
    public class DomainLeakService : IDomainLeakService
    {
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public void SetUnitOfWorkFactory(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void AddDomainLeaks(List<DomainLeak> Leaks)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var repo = unitOfWork.GetRepository<DomainLeakRepository, IDomainLeakRepository>();

                repo.AddLeaks(Leaks);
            }
        }

        public void AddDomainLeaksSimple(List<DomainLeak> Leaks)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var repo = unitOfWork.GetRepository<DomainLeakRepository, IDomainLeakRepository>();

                repo.AddLeaksSimple(Leaks);

                unitOfWork.SaveChanges();
            }
        }

        public List<DomainLeak> GetDomainLeaksByDomain(string domain)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var repo = unitOfWork.GetRepository<DomainLeakRepository, IDomainLeakRepository>();

                var leakList = repo.GetLeaks(domain);

                return leakList;
            }
        }

        public void DeleteDomainLeaksByDomain(string domain)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var repo = unitOfWork.GetRepository<DomainLeakRepository, IDomainLeakRepository>();

                repo.DeleteLeaks(domain);
            }
        }
    }
}
