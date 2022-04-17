using DataAccess.Models;
using DataAccess.Interfaces;
using DataAccess.Repositories;

namespace DataAccess.Services
{
    public class EmailLeakService : IEmailLeakService
    {
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public void SetUnitOfWorkFactory(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void AddEmailLeaks(List<EmailLeak> Leaks)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var repo = unitOfWork.GetRepository<EmailLeakRepository, IEmailLeakRepository>();

                repo.AddLeaks(Leaks);
            }
        }

        public List<EmailLeak> GetEmailLeaksByEmail(string emailAddress)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var repo = unitOfWork.GetRepository<EmailLeakRepository, IEmailLeakRepository>();

                var leakList = repo.GetLeaks(emailAddress);

                return leakList;
            }
        }

        public void DeleteEmailLeaksByEmailAddress(string emailAddress)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var repo = unitOfWork.GetRepository<EmailLeakRepository, IEmailLeakRepository>();

                repo.DeleteLeaks(emailAddress);
            }
        }
    }
}
