using BankAccountAPI.Services;
using NUnit.Framework;

namespace BankAccountAPI.UnitTests.Operacoes.Transacao
{
    [TestFixture]
    class DepositoTransacaoTests
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

        [TestCase(2)]
        [TestCase(1000)]
        [TestCase(2131231.21)]
        [TestCase(10.01)]
        public void Deposito_QuandoChamado_GeraTransacaoComResultadoSUCESSO(decimal valor)
        {
            var info = new Deposito(_contaLuke, valor);
            var transacao = info.Transacao;

            Assert.That(transacao.Resultado, Is.EqualTo("SUCESSO"));
        }

        [TestCase(2, 0.02)]
        [TestCase(1000, 10)]
        [TestCase(2131231.21, 21312.31)]
        [TestCase(10.01, 0.1)]
        public void Deposito_QuandoChamado_GeraTransacaoComTaxaCorreta(decimal valor, decimal taxaEsperada)
        {
            var info = new Deposito(_contaLuke, valor);
            var transacao = info.Transacao;

            Assert.That(transacao.Taxas, Is.EqualTo(taxaEsperada));
        }

        [TestCase(2)]
        [TestCase(1000)]
        [TestCase(2131231.21)]
        [TestCase(10.01)]
        public void Deposito_QuandoCahamdo_GeraTransacaoComSaldoAtualCorreto(decimal valor)
        {
            var saldoAnterior = _contaLuke.Saldo;
            var info = new Deposito(_contaLuke, valor);
            var transacao = info.Transacao;

            var saldoAtual = saldoAnterior + (valor - info.ValorTaxa);

            Assert.That(transacao.SaldoAtual, Is.EqualTo(saldoAtual));
        }

        [TestCase(2)]
        [TestCase(1000)]
        [TestCase(2131231.21)]
        [TestCase(10.01)]
        public void Deposito_QuandoCahamdo_GeraTransacaoComSaldoAnteriorCorreto(decimal valor)
        {
            var saldoAnterior = _contaLuke.Saldo;
            var info = new Deposito(_contaLuke, valor);
            var transacao = info.Transacao;

            Assert.That(transacao.SaldoAnterior, Is.EqualTo(saldoAnterior));
        }

        [Test]
        public void Deposito_QuandoChamado_GeraTransacaoComContaIdCorreto()
        {
            var saldoAnterior = _contaLuke.Saldo;
            var info = new Deposito(_contaLuke, 100);
            var transacao = info.Transacao;

            Assert.That(transacao.ContaId, Is.EqualTo(1));
        }

        [Test]
        public void Deposito_QuandoChamado_GeraTransacaoComTipoDeTransacaoCorreta()
        {
            var saldoAnterior = _contaLuke.Saldo;
            var info = new Deposito(_contaLuke, 100);
            var transacao = info.Transacao;

            Assert.That(transacao.TipoMovimentacao, Is.EqualTo("DEPOSITO"));
        }

    }
}
