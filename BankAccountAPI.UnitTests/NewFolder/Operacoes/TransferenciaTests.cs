using BankAccountAPI.Services;
using NUnit.Framework;

namespace BankAccountAPI.UnitTests.Operacoes
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
            _contaLucas.Saldo = 100.50M;

            _contaJenn = new Conta();
            _contaJenn.Id = 2;
            _contaJenn.Saldo = 200.12M;
        }

        [Test]
        [TestCase(100, "SUCESSO")]
        [TestCase(100.5, "SUCESSO")]
        [TestCase(200, "FALHA")]
        [TestCase(1, "FALHA")]
        [TestCase(0.5, "FALHA")]
        public void Transferencia_QuandoChamado_RetornaResultadoDaTentativa(decimal valor, string resultadoEsperado)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var resultado = info.Resultado;

            Assert.That(resultado, Is.EqualTo(resultadoEsperado));
        }

        [Test]
        [TestCase(50, -50)]
        [TestCase(100.5, -100.5)]
        [TestCase(1.5, -1.5)]
        public void Transferencia_QuandoValorEstaCorreto_AlteraOSaldoOrigem(decimal valor, decimal mudancaEsperada)
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
        [TestCase(0.5, 0)]
        public void Transferencia_QuandoValorNaoEstaCorreto_NaoAlteraOSaldoOrigem(decimal valor, decimal mudancaEsperada)
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
        public void Transferencia_QuandoSaldoDisponivel_CobraTaxa(decimal valor)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var taxa = info.ValorTaxa;

            Assert.That(taxa, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        [TestCase(0.5)]
        [TestCase(102)]
        public void Transferencia_QuandoSaldoIndiposnivel_NaoCobraTaxa(decimal valor)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var taxa = info.ValorTaxa;

            Assert.That(taxa, Is.EqualTo(0));
        }


        [Test]
        [TestCase(50, 49)]
        [TestCase(100.5, 99.5)]
        [TestCase(1.5, 0.5)]
        public void Transferencia_QuandoValorEstaCorreto_AlteraOSaldoDestino(decimal valor, decimal mudancaEsperada)
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
        [TestCase(0.5, 0)]
        public void Transferencia_QuandoValorNaoEstaCorreto_NaoAlteraOSaldoDestino(decimal valor, decimal mudancaEsperada)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var saldoAtual = info.SaldoAtualDestino;
            var saldoAnterior = info.SaldoAnteriorDestino;
            var mudancaSaldo = saldoAtual - saldoAnterior;

            Assert.That(mudancaSaldo, Is.EqualTo(mudancaEsperada));
        }


    }
}