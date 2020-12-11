using BankAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    public class Deposito
    {
        public Deposito(Conta conta, float valorTentativa)
        {
            TentarDepositar(conta, valorTentativa);
            GerarTransacao(conta.Id, valorTentativa);
        }

        public float SaldoAnterior  { get; set; }
        public float SaldoAtual     { get; set; }
        public string Resultado     { get; set; }
        public float ValorTaxa      { get; set; }
        public Transacao transacao  { get; set; }

        private void TentarDepositar(Conta conta, float valor)
        {
            SaldoAnterior = conta.Saldo;
            ValorTaxa = valor * Conta.TaxaPorcentagemDeposito;
            var valorTaxado = valor - ValorTaxa;

            conta.AlterarSaldo(valorTaxado);
            SaldoAtual = conta.Saldo;
            Resultado = "SUCESSO";
        }

        private void GerarTransacao(int contaId, float valorTentativa)
        {
            transacao = new Transacao();
            var valorTotal = valorTentativa - ValorTaxa;
            transacao.SetTransacao("DEPOSITO", Resultado, valorTentativa, valorTentativa, ValorTaxa, valorTotal, SaldoAnterior, SaldoAtual, contaId, null);
        }
    }
}
