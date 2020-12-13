using BankAccountAPI.Data;
using BankAccountAPI.Models;
using Moq;
using NUnit.Framework;
using System.Data.Entity;

namespace BankAccountAPI.UnitTests
{
    [TestFixture]
    class TransacaoTests
    {
        private Mock<BancoDbContext> _bancoContext;
        private Mock<DbSet<Transacao>> _dbSetMock;

        private Transacao _testTransacao;

        [SetUp]
        public void SetUp()
        {
            _bancoContext = new Mock<BancoDbContext>();
            _dbSetMock = new Mock<DbSet<Transacao>>();

        }

        [Test]
        public void Test1()
        {
            //_bancoContext.Setup(x => x.Set<Transacao>()).Returns(_dbSetMock.Object);
            //_dbSetMock.Setup(x => x.Add(It.IsAny<Transacao>())).Returns(_testTransacao);

            //var respository = new Repository<Transacao>(_bancoContext.Object);
            //respository.Add(_testTransacao);

            //_bancoContext.Verify(x => x.Set<Transacao>());
            //_dbSetMock.Verify(x => x.Add(It.Is<Transacao>(y => y == _testTransacao)));
        }

    }
}
