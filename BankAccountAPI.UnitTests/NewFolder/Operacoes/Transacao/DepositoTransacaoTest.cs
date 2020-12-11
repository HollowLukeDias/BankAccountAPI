using BankAccountAPI.Services;
using BankAccountAPI.Data;
using Moq;
using NUnit.Framework;

namespace BankAccountAPI.UnitTests.Operacoes.Transacao
{
    [TestFixture]
    class DepositoTransacaoTest
    {
        private Conta _contaLuke;

        [SetUp]
        public void SetUp()
        { 
            _contaLuke = new Conta
            {
                Id = 1,
                Saldo = 100
            };
        }

        [Test]
        [TestCase(1)]
        [TestCase(100)]
        [TestCase(1000000)]
        public void Deposito_QuandoChamado_GeraTransacaoComResultadoSUCESSO(decimal valor)
        {
            var info = new Deposito(_contaLuke, valor);
            var transacao = info.transacao;

            Assert.That(transacao.Resultado, Is.EqualTo("SUCESSO"));
        }

        [Test]
        [TestCase(2, 0.02)]
        [TestCase(1000, 10)]
        [TestCase(2131231.21, 21312.31)]
        [TestCase(10.01, 0.1)]
        public void Deposito_QuandoChamado_GeraTransacaoComTaxaCorreta(decimal valor, decimal taxaEsperada)
        {
            var info = new Deposito(_contaLuke, valor);
            var transacao = info.transacao;

            Assert.That(transacao.Taxas, Is.EqualTo(taxaEsperada));
        }



    }
}
