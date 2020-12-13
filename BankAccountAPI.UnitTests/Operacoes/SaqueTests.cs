using BankAccountAPI.Services;
using NUnit.Framework;

namespace BankAccountAPI.UnitTests.Operacoes
{
    [TestFixture]
    class SaqueTests
    {
        Conta _contaLucas;
        [SetUp]
        public void Setup()
        {
            _contaLucas = new Conta
            {
                Id = 1,
                Saldo = 100.50M
            };
        }


        [TestCase(102, "FALHA")]
        [TestCase(50, "SUCESSO")]
        [TestCase(100.5, "SUCESSO")]
        [TestCase(4, "FALHA")]
        [TestCase(1, "FALHA")]
        public void Saque_QuandoChamado_RetornaResultadoDaTentativa(decimal valorTeste, string resultadoEsperado)
        {
            var informacoes = new Saque(_contaLucas, valorTeste);
            var resultado = informacoes.Resultado;

            Assert.That(resultado, Is.EqualTo(resultadoEsperado));
        }


        [TestCase(100, 4)]
        [TestCase(5, 4)]
        [TestCase(1000, 0)]
        [TestCase(4, 0)]
        [TestCase(2, 0)]
        public void Saque_DependendoValorValido_CobraTaxa(decimal valor, decimal taxaEsperada)
        {
            var informacoes = new Saque(_contaLucas, valor);
            var taxaRetorno = informacoes.ValorTaxa;

            Assert.That(taxaRetorno, Is.EqualTo(taxaEsperada));
        }

        [TestCase(101.5)]
        [TestCase(1000)]
        [TestCase(4)]
        [TestCase(1)]
        public void Saque_QuandoSaldoInvalido_NaoAlteraSaldo(decimal valor)
        {
            var informacoes = new Saque(_contaLucas, valor);
            var saldoAnterior = informacoes.SaldoAnterior;
            var saldoAtual = informacoes.SaldoAtual;

            Assert.That(saldoAnterior, Is.EqualTo(saldoAtual));
        }

        [Test]
        public void Saque_QuandoSaldoSuficiente_RemoveValorDoSaldo()
        {
            var valorSaque = 50;

            var informacoes = new Saque(_contaLucas, valorSaque);
            var saldoAnterior = informacoes.SaldoAnterior;
            var saldoAtual = informacoes.SaldoAtual;

            Assert.That(valorSaque, Is.EqualTo(saldoAnterior - saldoAtual));
        }

        [Test]
        public void Saque_QuandoSaldoIgualAoValor_SaldoRestanteZerado()
        {
            var valorSaque = _contaLucas.Saldo;

            var info = new Saque(_contaLucas, valorSaque);
            var saldoAtual = info.SaldoAtual;

            Assert.That(saldoAtual, Is.EqualTo(0));
        }

    }
}
