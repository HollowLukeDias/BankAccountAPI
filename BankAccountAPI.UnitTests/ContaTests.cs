using BankAccountAPI.Helpers;
using Moq;
using NUnit.Framework;

namespace BankAccountAPI.UnitTests
{
    [TestFixture]
    class ContaTests
    {
        private Conta _contaLuke;
        private Mock<IContas> _contaRepository;

        [SetUp]
        public void SetUp()
        {
            _contaRepository = new Mock<IContas>();
            _contaLuke = new Conta
            {
                Id = 1,
                NomeCliente = "Luke Dias"
            };
        }

        [Test]
        public void CreateContas()
        {
            _contaRepository.Setup(c => c.CriarConta(_contaLuke));
        }
    }
}
