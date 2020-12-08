using BankAccountAPI;
using NUnit.Framework;
using System;

namespace BankAccountAPI.UnitTests
{
    [TestFixture]
    public class OperacoesBancariasTests
    {
        Conta _contaLucas;
        Conta _contaJenn;
        [SetUp]
        public void Setup()
        {
            _contaLucas = new Conta();
            _contaLucas.Id = 1;
            _contaLucas.Saldo = 100.12F;

            _contaJenn = new Conta();
            _contaJenn.Id = 2;
            _contaJenn.Saldo = 200.12F;
        }

        [Test]
        public void Deposita_QuandoChamado_AdicionaValorAoSaldo()
        {
            var valorDeposito= 1000;
            var informacoes = _contaLucas.Deposito(valorDeposito);
            var saldoAnterior = informacoes.saldoAnterior;
            var saldoAtual = informacoes.saldoAtual;
            var taxa = informacoes.valorTaxa;
            Assert.That(valorDeposito == (saldoAtual - saldoAnterior) + taxa);
        }

        [Test]
        public void Deposita_QuandoChamado_QuantidadeCertaDeTaxas()
        {
            var taxaPorcentagem = 1/100F;
            var valorDeposito = 1000;
            var taxaEsperada = valorDeposito * taxaPorcentagem;
            var informacoes = _contaLucas.Deposito(valorDeposito);
            var taxaResultado = informacoes.valorTaxa;
            Assert.That(taxaEsperada, Is.EqualTo(taxaResultado));
        }

        [Test]
        [TestCase(102, "SALDO INSUFICIENTE")]
        [TestCase(50, "SUCESSO")]
        public void Saque_QuandoChamado_RetornaResultadoDaTentativa(float valorTeste, string resultadoEsperado)
        {
            var informacoes = _contaLucas.Saque(valorTeste);
            var resultado = informacoes.resultado;
            Assert.That(resultado, Is.EqualTo(resultadoEsperado));
        }

        [Test]
        public void Saque_QuandoSaldoInsuficiente_NaoAlteraSaldo()
        {
            var valorSaque = 102;
            var informacoes = _contaLucas.Saque(valorSaque);
            var saldoAnterior = informacoes.saldoAnterior;
            var saldoAtual = informacoes.saldoAtual;
            Assert.That(saldoAnterior, Is.EqualTo(saldoAtual));
        }

        [Test]
        public void Saque_QuandoSaldoSuficiente_RemoveValorDoSaldo()
        {
            var valorSaque = 50;
            var informacoes = _contaLucas.Saque(valorSaque);
            var saldoAnterior = informacoes.saldoAnterior;
            var saldoAtual = informacoes.saldoAtual;
            var taxa = informacoes.valorTaxa;
            Assert.That(valorSaque == saldoAnterior - saldoAtual);
        }
    }
}