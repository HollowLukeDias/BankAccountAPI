using BankAccountAPI.Services;
using NUnit.Framework;
using System;

namespace BankAccountAPI.UnitTests
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
                Saldo = 100.50F
            };
        }



        [Test]
        [TestCase(102, "SALDO INSUFICIENTE")]
        [TestCase(50, "SUCESSO")]
        [TestCase(4, "VALOR IGUAL OU MAIOR QUE A TAXA")]
        [TestCase(1, "VALOR IGUAL OU MAIOR QUE A TAXA")]
        public void Saque_QuandoChamado_RetornaResultadoDaTentativa(float valorTeste, string resultadoEsperado)
        {
            var informacoes = new Saque(_contaLucas, valorTeste);
            var resultado = informacoes.Resultado;

            Assert.That(resultado, Is.EqualTo(resultadoEsperado));
        }


        [Test]
        [TestCase(1000, 0)]
        [TestCase(10, 4)]
        public void Saque_QuandoChamado_ValorDaTaxa(float valor, float taxaEsperada)
        {
            var informacoes = new Saque(_contaLucas, valor);
            var taxaRetorno = informacoes.ValorTaxa;

            Assert.That(taxaRetorno, Is.EqualTo(taxaEsperada));
        }

        [Test]
        public void Saque_QuandoSaldoInsuficiente_NaoAlteraSaldo()
        {
            var valorSaque = 102;

            var informacoes = new Saque(_contaLucas, valorSaque);
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
