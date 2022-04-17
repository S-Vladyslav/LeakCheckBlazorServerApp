using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.Interfaces;
using DataAccess.Services;
using DataAccess.Repositories;
using DataAccess;
using Xunit;
using Moq;

namespace DataAccessUnitTests
{
    public class LeakServiceUnitTests
    {
        private const string _CONNECTION_STRING = "Server=DESKTOP-CLR4CHN;Database=DBLeakChecker;User Id=Vladyslav;TrustServerCertificate=True;MultipleActiveResultSets=true;Integrated Security=true";

        private IDomainLeakService _domainLeakService;
        private IEmailLeakService _emailLeakService;

        private Mock<IDomainLeakRepository> _mockDomainLeakRepository = new Mock<IDomainLeakRepository>();
        private Mock<IEmailLeakRepository> _mockEmailLeakRepository = new Mock<IEmailLeakRepository>();

        private Mock<IUnitOfWorkFactory> _mockUnitOfWorkFactory = new Mock<IUnitOfWorkFactory>();

        private Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();

        public LeakServiceUnitTests()
        {
            IUnitOfWorkFactory uOF = new UnitOfWorkFactory();
            uOF.SetConnectionString(_CONNECTION_STRING);

            _domainLeakService = new DomainLeakService();
            _domainLeakService.SetUnitOfWorkFactory(uOF);
        }

        [Fact]
        public void DomainLeakService_GetDomainLeaksByDomain_Test()
        {
            //arrange
            var domain = "abc.com";
            var expectedDomainLeak = new List<DomainLeak>() { new DomainLeak() { Domain = domain,  } };

            _mockDomainLeakRepository.Setup(x => x.GetLeaks(domain)).Returns(new List<DomainLeak>() { new DomainLeak() { Domain = domain } });
            _mockUnitOfWork.Setup(x => x.GetRepository<DomainLeakRepository, IDomainLeakRepository>()).Returns(_mockDomainLeakRepository.Object);
            _mockUnitOfWorkFactory.Setup(x => x.CreateUnitOfWork()).Returns(_mockUnitOfWork.Object);

            _domainLeakService = new DomainLeakService();
            _domainLeakService.SetUnitOfWorkFactory(_mockUnitOfWorkFactory.Object);

            //act
            var returnedDomainLeak = _domainLeakService.GetDomainLeaksByDomain(domain);

            //assert
            Assert.Equal(expectedDomainLeak[0].Domain, returnedDomainLeak[0].Domain);
        }

        [Fact]
        public void EmailLeakService_GetEmailLeaksByDomain_Test()
        {
            //arrange
            var emailAddress = "abc@gmail.com";
            var expectedDomainLeak = new List<EmailLeak>() { new EmailLeak() { EmailAddress = emailAddress, } };

            _mockEmailLeakRepository.Setup(x => x.GetLeaks(emailAddress)).Returns(new List<EmailLeak>() { new EmailLeak() { EmailAddress = emailAddress } });
            _mockUnitOfWork.Setup(x => x.GetRepository<EmailLeakRepository, IEmailLeakRepository>()).Returns(_mockEmailLeakRepository.Object);
            _mockUnitOfWorkFactory.Setup(x => x.CreateUnitOfWork()).Returns(_mockUnitOfWork.Object);

            _emailLeakService = new EmailLeakService();
            _emailLeakService.SetUnitOfWorkFactory(_mockUnitOfWorkFactory.Object);

            //act
            var returnedDomainLeak = _emailLeakService.GetEmailLeaksByEmail(emailAddress);

            //assert
            Assert.Equal(expectedDomainLeak[0].Domain, returnedDomainLeak[0].Domain);
        }
    }
}

