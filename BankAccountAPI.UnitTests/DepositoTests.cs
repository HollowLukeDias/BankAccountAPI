using BankAccountAPI.Services;
using NUnit.Framework;
using System;

namespace BankAccountAPI.UnitTests
{
    [TestFixture]
    class DepositoTests
    {
        private Conta _contaLucas;

        [SetUp]
        public void Setup()
        {
            _contaLucas = new Conta();
            _contaLucas.Id = 1;
            _contaLucas.Saldo = 100.50F;
        }

        [Test]
        public void Deposita_QuandoChamado_AdicionaValorAoSaldo()
        {
            var valorDeposito = 1000;

            var informacoes = new Deposito(_contaLucas, valorDeposito);
            var saldoAnterior = informacoes.SaldoAnterior;
            var saldoAtual = informacoes.SaldoAtual;
            var taxa = informacoes.ValorTaxa;

            Assert.That(valorDeposito == (saldoAtual - saldoAnterior) + taxa);
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
