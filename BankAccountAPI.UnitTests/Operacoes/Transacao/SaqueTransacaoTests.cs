using BankAccountAPI.Services;
using NUnit.Framework;

namespace BankAccountAPI.UnitTests.Operacoes.Transacao
{
    [TestFixture]
    class SaqueTransacaoTests
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
        public void Saque_QuandoChamado_GeraTransacaoResultadoDaTentativa(decimal valorTeste, string resultadoEsperado)
        {
            var informacoes = new Saque(_contaLucas, valorTeste);
            var transacao = informacoes.Transacao;

            Assert.That(transacao.Resultado, Is.EqualTo(resultadoEsperado));
        }


        [TestCase(100, 4)]
        [TestCase(5, 4)]
        [TestCase(1000, 0)]
        [TestCase(4, 0)]
        [TestCase(2, 0)]
        public void Saque_DependendoValorValido_GeraTransacaoComTaxaCorreta(decimal valor, decimal taxaEsperada)
        {
            var informacoes = new Saque(_contaLucas, valor);
            var transacao = informacoes.Transacao;

            Assert.That(transacao.Taxas, Is.EqualTo(taxaEsperada));
        }

        [TestCase(101.5)]
        [TestCase(1000)]
        [TestCase(4)]
        [TestCase(1)]
        public void Saque_QuandoSaldoInvalido_GeraTransacaoComSaldoAnteriorEAtualIguais(decimal valor)
        {
            var informacoes = new Saque(_contaLucas, valor);
            var transacao = informacoes.Transacao;

            Assert.That(transacao.SaldoAnterior, Is.EqualTo(transacao.SaldoAtual));
        }


        [TestCase(101.5)]
        [TestCase(50)]
        [TestCase(1000)]
        [TestCase(4)]
        [TestCase(1)]
        public void Saque_QuandoChamado_GeraTransacaoComSaldoAtualCorreto(decimal valorSaque)
        {
            var informacoes = new Saque(_contaLucas, valorSaque);
            var transacao = informacoes.Transacao;

            Assert.That(transacao.SaldoAtual, Is.EqualTo(transacao.SaldoAnterior - transacao.ValorPagoTotal));
        }

        [Test]
        public void Saque_QuandoChamado_GeraTransacaoComContaIdCorreto()
        {
            var informacoes = new Saque(_contaLucas, 10);
            var transacao = informacoes.Transacao;

            Assert.That(transacao.ContaId, Is.EqualTo(1));
        }

        [Test]
        public void Saque_QuandoChamado_GeraTransacaoComTipoDeTransacaoCorreto()
        {
            var informacoes = new Saque(_contaLucas, 10);
            var transacao = informacoes.Transacao;

            Assert.That(transacao.TipoMovimentacao, Is.EqualTo("SAQUE"));
        }
    }
}
