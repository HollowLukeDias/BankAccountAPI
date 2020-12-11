using BankAccountAPI.Services;
using NUnit.Framework;
using System;

namespace BankAccountAPI.UnitTests
{
    [TestFixture]
    public class TransferenciaTests
    {
        Conta _contaLucas;
        Conta _contaJenn;
        [SetUp]
        public void Setup()
        {
            _contaLucas = new Conta();
            _contaLucas.Id = 1;
            _contaLucas.Saldo = 100.50F;

            _contaJenn = new Conta();
            _contaJenn.Id = 2;
            _contaJenn.Saldo = 200.12F;
        }

        [Test]
        [TestCase(100, "SUCESSO")]
        [TestCase(100.5F, "SUCESSO")]
        [TestCase(200, "FALHA")]
        [TestCase(1, "FALHA")]
        [TestCase(0.5F, "FALHA")]
        public void Transferencia_QuandoChamado_RetornaResultadoDaTentativa(float valor, string resultadoEsperado)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var resultado = info.Resultado;

            Assert.That(resultado, Is.EqualTo(resultadoEsperado));
        }
        
        [Test]
        [TestCase(50F, -50F)]
        [TestCase(100.5F, -100.5F)]
        [TestCase(1.5F, -1.5F)]
        public void Transferencia_QuandoValorEstaCorreto_AlteraOSaldoOrigem(float valor, float mudancaEsperada)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var saldoAtual = info.SaldoAtual;
            var saldoAnterior = info.SaldoAnterior;
            var mudancaSaldo = saldoAtual - saldoAnterior;

            Assert.That(mudancaSaldo, Is.EqualTo(mudancaEsperada));
        }

        [Test]
        [TestCase(110, 0)]
        [TestCase(1, 0)]
        [TestCase(0.5F, 0)]
        public void Transferencia_QuandoValorNaoEstaCorreto_NaoAlteraOSaldoOrigem(float valor, float mudancaEsperada)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var saldoAtual = info.SaldoAtual;
            var saldoAnterior = info.SaldoAnterior;
            var mudancaSaldo = saldoAtual - saldoAnterior;

            Assert.That(mudancaSaldo, Is.EqualTo(mudancaEsperada));
        }

        [Test]
        [TestCase(50)]
        [TestCase(100)]
        public void Transferencia_QuandoSaldoDisponivel_CobraTaxa(float valor)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var taxa = info.ValorTaxa;

            Assert.That(taxa, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        [TestCase(0.5F)]
        [TestCase(102)]
        public void Transferencia_QuandoSaldoIndiposnivel_NaoCobraTaxa(float valor)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var taxa = info.ValorTaxa;

            Assert.That(taxa, Is.EqualTo(0));
        }


        [Test]
        [TestCase(50F, 49F)]
        [TestCase(100.5F, 99.5F)]
        [TestCase(1.5F, 0.5F)]
        public void Transferencia_QuandoValorEstaCorreto_AlteraOSaldoDestino(float valor, float mudancaEsperada)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var saldoAtual = info.SaldoAtualDestino;
            var saldoAnterior = info.SaldoAnteriorDestino;
            var mudancaSaldo = saldoAtual - saldoAnterior;

            Assert.That(mudancaSaldo, Is.EqualTo(mudancaEsperada));
        }


        [Test]
        [TestCase(110, 0)]
        [TestCase(1, 0)]
        [TestCase(0.5F, 0)]
        public void Transferencia_QuandoValorNaoEstaCorreto_NaoAlteraOSaldoDestino(float valor, float mudancaEsperada)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var saldoAtual = info.SaldoAtualDestino;
            var saldoAnterior = info.SaldoAnteriorDestino;
            var mudancaSaldo = saldoAtual - saldoAnterior;

            Assert.That(mudancaSaldo, Is.EqualTo(mudancaEsperada));
        }


    }
}