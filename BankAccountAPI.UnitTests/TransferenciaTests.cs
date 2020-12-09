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
        [TestCase(200, "SALDO INSUFICIENTE")]
        public void Transferencia_QuandoChamado_RetornaResultadoDaTentativa(float valor, string resultadoEsperado)
        {
            var info = new Transferencia(_contaLucas, _contaJenn, valor);
            var resultado = info.Resultado;

            Assert.That(resultado, Is.EqualTo(resultadoEsperado));
        }

    }
}