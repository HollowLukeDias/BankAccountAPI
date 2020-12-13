using BankAccountAPI.Services;
using NUnit.Framework;

namespace BankAccountAPI.UnitTests.Operacoes.Transacao
{
    [TestFixture]
    class TransferenciaTransacaoTests
    {
        Conta _contaLucas;
        Conta _contaJenn;
        [SetUp]
        public void Setup()
        {
            _contaLucas = new Conta
            {
                Id = 1,
                Saldo = 100.50M
            };

            _contaJenn = new Conta
            {
                Id = 2,
                Saldo = 500.75M
            };
        }

        
        [TestCase(100, "SUCESSO")]
        [TestCase(100.5, "SUCESSO")]
        [TestCase(200, "FALHA")]
        [TestCase(1, "FALHA")]
        [TestCase(0.5, "FALHA")]
        public void Transferencia_QuandoChamado_GeraTransacaoComResultadoDaTentativaCorreto(decimal valor, string resultadoEsperado)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var transacao = info.Transacao;

            Assert.That(transacao.Resultado, Is.EqualTo(resultadoEsperado));
        }

        
        [TestCase(100, "SUCESSO")]
        [TestCase(100.5, "SUCESSO")]
        public void Transferencia_QuandoSaldoDisponivel_GeraTransacaoDestinoComResultadoDaTentativaCorreto(decimal valor, string resultadoEsperado)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var transacao = info.TransacaoDestino;

            Assert.That(transacao.Resultado, Is.EqualTo(resultadoEsperado));
        }

        
        [TestCase(200)]
        [TestCase(1)]
        [TestCase(0.5)]
        public void Transferencia_QuandoSaldoIndisponivel_GeraTransacaoDestinoComResultadoDaTentativaCorreto(decimal valor)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);

            Assert.That(info.TransacaoDestino, Is.Null);
        }

        
        [TestCase(50, -50)]
        [TestCase(100.5, -100.5)]
        [TestCase(1.5, -1.5)]
        public void Transferencia_QuandoValorEstaCorreto_GeraTransacaoMudancaCorretaNoSaldo(decimal valor, decimal mudancaEsperada)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var transacao = info.Transacao;
            var mudancaSaldo = transacao.SaldoAtual - transacao.SaldoAnterior;

            Assert.That(mudancaSaldo, Is.EqualTo(mudancaEsperada));
        }

        
        [TestCase(50)]
        [TestCase(100.5)]
        [TestCase(1.5)]
        public void Transferencia_QuandoValorEstaCorreto_GeraTransacaoMudancaCorretaNoSaldoDestino(decimal valor)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var transacao = info.TransacaoDestino;
            var mudancaSaldo = transacao.SaldoAtual - (transacao.SaldoAnterior + transacao.Taxas);

            Assert.That(mudancaSaldo, Is.EqualTo(mudancaSaldo));
        }

        
        [TestCase(110)]
        [TestCase(1)]
        [TestCase(0.5)]
        public void Transferencia_QuandoValorNaoEstaCorreto_GeraTransacaoSemMudancaNoSaldo(decimal valor)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var transacao = info.Transacao;
            var mudancaSaldo = transacao.SaldoAtual - transacao.SaldoAnterior;

            Assert.That(mudancaSaldo, Is.EqualTo(0));
        }

        
        [TestCase(50)]
        [TestCase(100)]
        public void Transferencia_QuandoSaldoDisponivel_CobraTaxa(decimal valor)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var transacao = info.Transacao;

            Assert.That(transacao.Taxas, Is.EqualTo(1));
        }


        [TestCase(1)]
        [TestCase(0.5)]
        [TestCase(102)]
        public void Transferencia_QuandoSaldoIndiposnivel_NaoCobraTaxa(decimal valor)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var transacao = info.Transacao;

            Assert.That(transacao.Taxas, Is.EqualTo(0));
        }

        [Test]
        public void Saque_QuandoChamado_GeraTransacaoComContaIdCorreto()
        {
            var informacoes = new Transferencia(_contaLucas, _contaJenn, 10);
            var transacao = informacoes.Transacao;

            Assert.That(transacao.ContaId, Is.EqualTo(1));
        }

        [Test]
        public void Saque_QuandoChamado_GeraTransacaoComContaRelacionadaIdCorreto()
        {
            var informacoes = new Transferencia(_contaLucas, _contaJenn, 10);
            var transacao = informacoes.TransacaoDestino;

            Assert.That(transacao.ContaId, Is.EqualTo(2));
        }

        [Test]
        public void Saque_QuandoChamado_GeraTransacaoComTipoDeTransacaoCorreto()
        {
            var informacoes = new Transferencia(_contaLucas, _contaJenn, 10);
            var transacao = informacoes.Transacao;

            Assert.That(transacao.TipoMovimentacao, Is.EqualTo("TRANSFERENCIA - ENVIO"));
        }

        [Test]
        public void Saque_QuandoChamado_GeraTransacaoComTipoDeTransacaoCorretoContaDestino()
        {
            var informacoes = new Transferencia(_contaLucas, _contaJenn, 10);
            var transacao = informacoes.TransacaoDestino;

            Assert.That(transacao.TipoMovimentacao, Is.EqualTo("TRANSFERENCIA - RECEBIMENTO"));
        }

    }
}
