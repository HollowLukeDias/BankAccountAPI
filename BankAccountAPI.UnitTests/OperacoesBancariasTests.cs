using BankAccountAPI.Services;
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
            _contaLucas.Saldo = 100.50F;

            _contaJenn = new Conta();
            _contaJenn.Id = 2;
            _contaJenn.Saldo = 200.12F;
        }


    }
}