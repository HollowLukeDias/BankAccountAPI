using BankAccountAPI.Helpers;
using BankAccountAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;


namespace BankAccountAPI.UnitTests
{
    [TestFixture]
    class ContaTests
    {
        private Conta _contaLuke;
        private Mock<DbContext> _dbContext;
        private Mock<DbSet<Conta>> _dbSet;

        [SetUp]
        public void SetUp()
        {
            _dbContext = new Mock<DbContext>();
            _dbSet = new Mock<DbSet<Conta>>();
            _dbContext.Setup(x => x.Set<Conta>()).Returns(_dbSet.Object);

            _contaLuke = new Conta
            {
                Id = 1,
                NomeCliente = "Luke Dias"
            };
        }

        //Checado se falha ao mudar o método
        [Test]
        public void Create_ObjetoContaPassado_FuncaoCorreta()
        {
            _dbSet.Setup(x => x.Add(It.IsAny<Conta>()));

            var repository = new Repository<Conta>(_dbContext.Object);
            repository.Add(_contaLuke);

            _dbContext.Verify(x => x.Set<Conta>());
            _dbSet.Verify(x => x.Add(It.Is<Conta>(y => y == _contaLuke)));

        }

        [Test]
        public void Remove_ObjetoContaPassado_FuncaoCorreta()
        {
            _dbSet.Setup(x => x.Remove(It.IsAny<Conta>()));

            var repository = new Repository<Conta>(_dbContext.Object);
            repository.Remove(_contaLuke);

            _dbContext.Verify(x => x.Set<Conta>());
            _dbSet.Verify(x => x.Remove(It.Is<Conta>(y => y == _contaLuke)));
        }
    }
}
