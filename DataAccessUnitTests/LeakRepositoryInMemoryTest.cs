using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.Services;
using Xunit;

namespace DataAccessUnitTests
{
    public class LeakRepositoryInMemoryTest
    {
        private string ConnectionString = "aaaaaaa";

        [Fact]
        public void AddDomainLeaksSimple_ImMemoryTest()
        {
            //arrange
            var domain = "aaa.com";
            var LeakList = new List<DomainLeak>() { new DomainLeak() { Domain = domain} };
            var LeakListForOutPut = new List<DomainLeak>() { new DomainLeak() { Domain = domain} };

            var domainLeakService = new DomainLeakService();
            var uOW = new UnitOfWorkFactoryInMemoryTest();

            uOW.SetConnectionString(ConnectionString);
            domainLeakService.SetUnitOfWorkFactory(uOW);

            //act
            domainLeakService.AddDomainLeaksSimple(LeakList);
            LeakListForOutPut = domainLeakService.GetDomainLeaksByDomain(domain);

            //assert
            Assert.Equal(LeakList[0].Domain, LeakListForOutPut[0].Domain);
        }
    }
}
