using BankAccountAPI.Services;
using NUnit.Framework;

namespace BankAccountAPI.UnitTests.Operacoes
{
    [TestFixture]
    class DepositoTests
    {
        private Conta _contaLucas;

        [SetUp]
        public void Setup()
        {
            _contaLucas = new Conta
            {
                Id = 1,
                Saldo = 100.50M
            };
        }

        [TestCase(1, 0.99)]
        [TestCase(1000, 990)]
        [TestCase(876.22, 867.46)]

        public void Deposita_QuandoChamado_AdicionaValorTaxadoAoSaldoS(decimal valorDeposito, decimal valorTaxadoEsperado)
        {
            var informacoes = new Deposito(_contaLucas, valorDeposito);
            var saldoAnterior = informacoes.SaldoAnterior;
            var saldoAtual = informacoes.SaldoAtual;
            var valorTaxado = saldoAtual - saldoAnterior;

            Assert.That(valorTaxado, Is.EqualTo(valorTaxadoEsperado));
        }

        [Test]
        public void Deposita_QuandoChamado_QuantidadeCertaDeTaxas()
        {
            var taxaPorcentagem = 1 / 100F;
            var valorDeposito = 1000;

            var taxaEsperada = valorDeposito * taxaPorcentagem;
            var informacoes = new Deposito(_contaLucas, valorDeposito);
            var taxaResultado = informacoes.ValorTaxa;

            Assert.That(taxaEsperada, Is.EqualTo(taxaResultado));
        }
    }
}
